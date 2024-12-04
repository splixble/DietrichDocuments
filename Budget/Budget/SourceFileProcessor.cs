using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Budget.MainDataSet;
using System.Windows.Forms;
using Budget.MainDataSetTableAdapters;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System.IO;
using System.Text.RegularExpressions;

namespace Budget
{
    internal class SourceFileProcessor
    {
        public BudgetDataTable BudgetTable => _BudgetTable;
        BudgetDataTable _BudgetTable;

        BudgetTableAdapter _BudgetAdapter;

        protected string SelectedAccount => _SelectedAccount;
        string _SelectedAccount = null;

        protected MainDataSet.BudgetAccountRow _AccountRowSelected = null;
        protected MainDataSet.BudgetSourceFileFormatRow _SourceFileFormatRow = null;

        protected SourceFileFormats _SourceFileFormat;       

        public virtual bool UpdateAccountFromSourceFile => true;
        public virtual bool AllowAddingNewBudgetRows => true;

        MainDataSet.BudgetSourceFileFormatDataTable SourceFileFormatTable => Program.LookupTableSet.MainDataSet.BudgetSourceFileFormat;

        public MainDataSet.BudgetSourceFileDataTable _SourceFileTable = new MainDataSet.BudgetSourceFileDataTable();
        public MainDataSet.BudgetSourceFileRow _NewestSourceFileRow = null;
        public MainDataSet.BudgetSourceFileItemsDataTable _SourceFileItemsTable = new MainDataSet.BudgetSourceFileItemsDataTable();
        public List<BudgetAndSourceItemRows> _ImportedBudgetItems = new List<BudgetAndSourceItemRows>();

        public string SourceFilePath => _SourceFilePath;
        string _SourceFilePath;

        public List<int> MatchingBudgetIDs => _MatchingBudgetIDs;
        List<int> _MatchingBudgetIDs = new List<int>();

        public bool IsManuallyEntered => _IsManuallyEntered;
        bool _IsManuallyEntered;

        string[] _TextLines = null; // if manually entered

        public SourceFileProcessor(BudgetDataTable budgetTable, string selectedAccount, string selectedFileFormat, bool isManuallyEntered)
        {
            _BudgetAdapter = new BudgetTableAdapter();
            _BudgetAdapter.Connection = Program.DbConnection;
            _BudgetTable = budgetTable;
            _SelectedAccount = selectedAccount;
            _IsManuallyEntered = isManuallyEntered;

            _TextLines = new string[0];

            if (_SelectedAccount != null)
            {
                // DIAG Is this the right thing to do? if no account is selected, it probably shoudn't even include account... it doesn't use it...
                _AccountRowSelected = Program.LookupTableSet.MainDataSet.BudgetAccount.FindByAccountID(_SelectedAccount as string);
                _SourceFileFormatRow = SourceFileFormatTable.FindByFormatCode(selectedFileFormat);

                // get source file format:
                _SourceFileFormat = SourceFileFormats.None;
                foreach (Enum enumVal in Enum.GetValues(typeof(SourceFileFormats)))
                {
                    if (enumVal.ToString() == selectedFileFormat)
                    {
                        _SourceFileFormat = (SourceFileFormats)enumVal;
                        break;
                    }
                }
            }
        }

        // Extracts fields from a line of the text file, puts them in fieldsByColumnName.
        // Returns whether line was parseable.
        // Can be overridden for custom source files like Amazon order lists.
        protected virtual bool ExtractFields(string[] fileFields, Dictionary<string, object> fieldsByColumnName)
        {
            string[] formatFields = _SourceFileFormatRow.FormatColumns.Split(',');

            bool lineParsable = true;
            for (int formatColIndex = 0; formatColIndex < formatFields.Length; formatColIndex++)
            {
                // DIAG can remove var lineParsable; just return false
                // 
                if (formatFields[formatColIndex] != "")
                {
                    if (formatColIndex >= fileFields.Length)
                    {
                        lineParsable = false;
                        break;
                    }

                    // which Budget column is it?
                    if (!_BudgetTable.Columns.Contains(formatFields[formatColIndex]))
                    {
                        lineParsable = false;
                        break;
                    }
                    DataColumn budgetColumn = _BudgetTable.Columns[formatFields[formatColIndex]];

                    // Parse the fields, depending on the column's data type:
                    if (budgetColumn.DataType == typeof(string))
                        fieldsByColumnName[budgetColumn.ColumnName] = fileFields[formatColIndex];
                    else if (budgetColumn.DataType == typeof(Decimal))
                    {
                        Decimal fileValue;
                        if (Decimal.TryParse(fileFields[formatColIndex], out fileValue))
                        {
                            // For Amount and Balance columns, handle the special case in which source file lists credits as negative and debits as positive:
                            if (_SourceFileFormatRow.CreditsAreNegative)
                                fileValue = -fileValue;

                            fieldsByColumnName[budgetColumn.ColumnName] = fileValue;
                        }
                        else
                            lineParsable = false;
                    }
                    else if (budgetColumn.DataType == typeof(DateTime))
                    {
                        DateTime fileValue;
                        if (DateTime.TryParse(fileFields[formatColIndex], out fileValue))
                            fieldsByColumnName[budgetColumn.ColumnName] = fileValue;
                        else
                            lineParsable = false;
                    }
                    if (!lineParsable)
                        break;
                }
            }
            return lineParsable;
        }

        BudgetRow FindDuplicateRow(Dictionary<string, object> fieldsByColumnName)
        {
            {
                DataRowView[] dupRowViews = FindDuplicateRowViews(fieldsByColumnName);
                if (dupRowViews.Length == 0)
                    return null;
                else if (dupRowViews.Length == 1)
                    return dupRowViews[0].Row as BudgetRow;
                else
                {
                    string dupIDList = "";
                    foreach (DataRowView dupRowView in dupRowViews)
                    {
                        if (dupIDList != "")
                            dupIDList += ", ";
                        dupIDList += ((BudgetRow)dupRowView.Row).ID.ToString();
                    }
                    MessageBox.Show("Multiple duplicate Budget rows, with IDs " + dupIDList);
                    return null;
                }
            }

        }

        protected virtual DataRowView[] FindDuplicateRowViews(Dictionary<string, object> fieldsByColumnName)
        {
            // TODO maybe a custom made DataView per format, depending on what fields it captures...or just linear search...
            return _BudgetTable.DuplicatesView.FindRows(
                new object[] {
                    (DateTime)fieldsByColumnName["TrDate"],
                    (decimal)fieldsByColumnName["Amount"],
                    (string)fieldsByColumnName["Descrip"],
                    _SelectedAccount
                });
        }

        public void Process()
        {
            _SourceFilePath = PromptForSourceFile(false);
            if (_SourceFilePath != null)
                ReadInSourceFile();
        }

        public void ProcessFromChecklist()
        {
            string downloadedFilePath = PromptForSourceFile(true);
            if (downloadedFilePath != null)
            {
                // before processing, copy source file from Downloads to account's SourceFilePath:
                string archivedFilePath = Path.Combine(AccountSourceFilePath, "S_" + DateTime.Now.ToString("yyMMdd_hhmmss_") + Path.GetFileName(downloadedFilePath));
                try
                {


                    // DIAG seems to copy OK. So download a file and try it.


                    File.Copy(downloadedFilePath, archivedFilePath);
                    _SourceFilePath = archivedFilePath; 
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Error copying source file: " + ex.Message);
                    return;
                }

                ReadInSourceFile();
            }
        }

        string AccountSourceFilePath
        { 
            get
            {
                string srcRootDir = "D:\\Dietrich\\Business\\Budget App Input Files"; // DIAG get from Config file, which s/b read at beginning and globally available
                if (_AccountRowSelected != null)
                    return Path.Combine(srcRootDir, this._AccountRowSelected.SourceFileLocation);
                else
                    return srcRootDir;
            }
        }

        string DownloadsDirectory
        {
            get 
            {
                return "C:\\Users\\Dietr\\Downloads"; //  make it a config field DIAG
            }
        }

        protected string PromptForSourceFile(bool startInDownloadsDir)
        {
            // returns file path if user OK'd; null if Cancel
            OpenFileDialog srcFileDlg = new OpenFileDialog();

            if (startInDownloadsDir)
                srcFileDlg.InitialDirectory = DownloadsDirectory;
            else
                srcFileDlg.InitialDirectory = AccountSourceFilePath;

            if (_SourceFileFormatRow != null && !_SourceFileFormatRow.IsFileExtensionNull())
                srcFileDlg.DefaultExt = _SourceFileFormatRow.FileExtension;
            srcFileDlg.Title = "Select source file:";
            DialogResult diaRes = srcFileDlg.ShowDialog();

            if (diaRes == DialogResult.OK)
                return srcFileDlg.FileName;
            else
                return null;
        }

        protected virtual void ReadInSourceFile()
        {
            if (_IsManuallyEntered)
            {
                StatementDatePrompt prompt = new StatementDatePrompt();
                prompt.InitializeWithFilename(SourceFilePath);
                DialogResult res = prompt.ShowDialog();
                if (res == DialogResult.OK)
                {
                    // Bring up PDF (or whatever format) file:
                    System.Diagnostics.Process.Start(SourceFilePath);

                    // TODO WHAT IF we dont real;ly need the statement date?
                    // make new source file row:
                    _NewestSourceFileRow = _SourceFileTable.NewBudgetSourceFileRow();
                    _NewestSourceFileRow.FilePath = SourceFilePath;
                    _NewestSourceFileRow.StatementDate = prompt.StatementDate;
                    _NewestSourceFileRow.Account = this.SelectedAccount;
                    _NewestSourceFileRow.ManuallyEntered = true;
                    _SourceFileTable.AddBudgetSourceFileRow(_NewestSourceFileRow);
                }
            }
            else
            {
                // make new source file row:
                _NewestSourceFileRow = _SourceFileTable.NewBudgetSourceFileRow();
                _NewestSourceFileRow.FilePath = _SourceFilePath;
                _NewestSourceFileRow.Account = _SelectedAccount;
                _NewestSourceFileRow.ManuallyEntered = false;
                _SourceFileTable.AddBudgetSourceFileRow(_NewestSourceFileRow);

                // Read in the full Budget table, to check for duplicates:
                _BudgetAdapter.Fill(_BudgetTable);

                // use Microsoft.VisualBasic.FileIO objects (TextFieldParser, TextFieldType) to load source files:
                using (TextFieldParser parser = new TextFieldParser(_SourceFilePath))
                {
                    _MatchingBudgetIDs.Clear();

                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // allow tab delim?
                    int lineNum = 0; // it's 1-relative
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        string[] fileFields = parser.ReadFields();
                        lineNum++;

                        Dictionary<string, object> fieldsByColumnName = new Dictionary<string, object>();

                        bool lineParsable = ExtractFields(fileFields, fieldsByColumnName);

                        if (lineParsable)
                            AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                    }
                }
            }
        }

        public bool AddOrUpdateBudgetRow(Dictionary<string, object> fieldsByColumnName, int lineNum)
        {
            BudgetRow importedRow = null;

            // Check for duplicate Budget rows:
            BudgetRow dupRow = FindDuplicateRow(fieldsByColumnName);

            if (dupRow != null)
            {
                // Duplicate row found
                importedRow = dupRow;
                // Add row ID:
                _MatchingBudgetIDs.Add(dupRow.ID);
            }
            else
            {
                // no duplicate
                if (AllowAddingNewBudgetRows)
                {
                    // add new row to table
                    importedRow = _BudgetTable.NewBudgetRow();
                    // REMOVED importedRow.IsIncome = false; // necessary initialization
                    importedRow.Ignore = false; // necessary initialization
                    importedRow.BalanceIsCalculated = false; // necessary initialization
                }
            }

            // if no duplicate is found, and we're not allowed to add rows, then just move to the next line in src file:
            if (importedRow == null)
                return false;

            // Copy fields in:
            foreach (string columnName in fieldsByColumnName.Keys)
            {
                object newFieldValue = fieldsByColumnName[columnName];
                if (newFieldValue != null)
                    importedRow[columnName] = newFieldValue;
            }

            if (UpdateAccountFromSourceFile)
                importedRow.Account = _SelectedAccount;

            BudgetSourceFileItemsRow fileItemsRow = _SourceFileItemsTable.NewBudgetSourceFileItemsRow();
            fileItemsRow.SourceFile = -1; // real value filled in on save 
            fileItemsRow.SourceFileLine = lineNum; // 1-relative
            _SourceFileItemsTable.AddBudgetSourceFileItemsRow(fileItemsRow);

            _ImportedBudgetItems.Add(new BudgetAndSourceItemRows(importedRow, fileItemsRow));

            // if new row, we need to add it to the DataTable:
            if (dupRow == null)
                _BudgetTable.AddBudgetRow(importedRow);

            return true;
        }

        public virtual void SaveChanges()
        {
            // TODO use transactions (budgetTableAdapter.Transaction) in this
            MainDataSetTableAdapters.BudgetSourceFileTableAdapter sourceFileAdap = new BudgetSourceFileTableAdapter();
            _NewestSourceFileRow.ImportDateTime = DateTime.Now;
            sourceFileAdap.Update(_SourceFileTable);

             _BudgetAdapter.Update(_BudgetTable);

            foreach (BudgetAndSourceItemRows budgetObj in _ImportedBudgetItems)
            {
                // fill in new Items rows with updated, permanent ID field values:
                budgetObj._ItemsRow.BudgetItem = budgetObj._BudgetRow.ID; 
                budgetObj._ItemsRow.SourceFile = _NewestSourceFileRow.FileID;
            }

            MainDataSetTableAdapters.BudgetSourceFileItemsTableAdapter fileItemsAdap = new BudgetSourceFileItemsTableAdapter();
            fileItemsAdap.Update(_SourceFileItemsTable);
        }

        bool CaptureDateFieldInBofAWideStatementLine(string line, Dictionary<string, object> fieldsByColumnName, out string restOfLine)
        {
            restOfLine = ""; // initialize

            string pattern = null;
            // Capture date, ignore space if exists, and rest of line:
            if (_SourceFileFormat == SourceFileFormats.BofAWidePDFWithYear)
            {
                pattern = @"^(\d\d/\d\d/\d\d)[ ]?(.*)$";
                Match match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    // 2nd capture is rest of line
                    restOfLine = match.Groups[2].Value;

                    // 1st capture is TrDate
                    DateTime dateValue;
                    string dateString = match.Groups[1].Value;
                    if (DateTime.TryParse(dateString, out dateValue))
                        fieldsByColumnName["TrDate"] = dateValue;
                    else
                        return false;

                    return true;
                }
                else
                    return false;
            }
            else if (_SourceFileFormat == SourceFileFormats.BofAWidePDF)
            {
                pattern = @"^(\d\d/\d\d) (\d\d/\d\d)[ ]?(.+)$";  // no year in date. Captures Card Trans Date, Posting Date
                Match match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    // 3rd capture is rest of line
                    restOfLine = match.Groups[3].Value;

                    DateTime dateValue;

                    // 1st capture is Card Trans Date
                    string dateString = AddYearToYearlessDateString(match.Groups[1].Value);
                    if (DateTime.TryParse(dateString, out dateValue))
                        fieldsByColumnName["CardTransDate"] = dateValue;
                    else
                        return false;

                    // 2nd capture is TrDate
                    dateString = AddYearToYearlessDateString(match.Groups[2].Value);
                    if (DateTime.TryParse(dateString, out dateValue))
                        fieldsByColumnName["TrDate"] = dateValue;
                    else
                        return false;

                    return true;

                }
                else
                    return false;
            }
            else
                return false;
        }

        public string AddYearToYearlessDateString(string dateString)
        {
            // BofA credit card statements don't list year, so we have to add year to string before parsing.
            // Get year from Statement Date:
            // TODO drop out if the file isnt loaded
            int statementDateYear = _NewestSourceFileRow.StatementDate.Year;
            int statementDateMonth = _NewestSourceFileRow.StatementDate.Month;

            // The transaction might be in the year BEFORE the statement year, though, if it's in December (or a month >= 10),
            // and it's a January statement (this check was added 25Jun24):
            int tYear = statementDateYear;
            if (dateString[0] == '1' // if month = 10, 11, or 12
                && statementDateMonth == 1)
                tYear--;
            return dateString + "/" + tYear.ToString();
        }

        public void ProcessManualLines(string[] textLines)
        {
            switch (_SourceFileFormat)
            {
                case SourceFileFormats.BofAWidePDF:
                case SourceFileFormats.BofAWidePDFWithYear:
                    ProcessBofAWidePDFLines(textLines);
                    break;
                case SourceFileFormats.BofANarrowPDF:
                    ProcessBofANarrowPDFLines(textLines);
                    break;
                case SourceFileFormats.AmexStmt:
                    ProcessAmexPDFLines(textLines);
                    break;
                default:
                    MessageBox.Show("Cannot process text if source file format of account is " + _SourceFileFormat.ToString());
                    break;
            }
        }


        public void ProcessAmexPDFLines(string[] textLines)
        {
            // For BofA Statement PDFs that, when text is copied and pasted to text box, shows one field per line rather than one transaction per line.
            _TextLines = textLines;

            Dictionary<string, object> fieldsByColumnName = null;

            PDFField _LastFieldRead = PDFField.None;

            int lineNum = 0; // it's 1-relative
            foreach (string line in _TextLines)
            {
                lineNum++;

                // reused vars in switch stmt:
                Match match;

                switch (_LastFieldRead)
                {
                    case PDFField.None:
                        // start of budget item, so initialize fieldsByColumnName:
                        fieldsByColumnName = new Dictionary<string, object>();

                        // does line start with a date (mm/dd/yy)?
                        match = Regex.Match(line, @"^(\d\d/\d\d/\d\d)(.*)$");
                        if (match.Success)
                        {
                            DateTime dateValue;
                            if (DateTime.TryParse(match.Groups[1].Value, out dateValue))
                            {
                                fieldsByColumnName["TrDate"] = dateValue;

                                // is there's anything to the right of the date?
                                if (match.Groups[2].Value != "")
                                {
                                    string leftText = match.Groups[2].Value.Trim();

                                    // Could be a description and amount, or just a description:
                                    if (CaptureAmountField(leftText, fieldsByColumnName, false, true))
                                    {
                                        AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                                        _LastFieldRead = PDFField.None;
                                    }
                                    else
                                    {
                                        AddToDescripField(fieldsByColumnName, leftText);
                                        _LastFieldRead = PDFField.Descrip;
                                    }
                                }
                                else
                                    _LastFieldRead = PDFField.PostDate;
                            }
                        }
                        else
                            _LastFieldRead = PDFField.None;
                        break;
                    case PDFField.Descrip:
                        // Could be a description and amount, or just a description:
                        if (CaptureAmountField(line, fieldsByColumnName, false, true))
                        {
                            AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                            _LastFieldRead = PDFField.None;
                        }
                        else
                        {
                            AddToDescripField(fieldsByColumnName, line);
                            _LastFieldRead = PDFField.Descrip;
                        }
                        break;
                }
            }
        }

        public void ProcessBofANarrowPDFLines(string[] textLines)
        {
            // For BofA Statement PDFs that, when text is copied and pasted to text box, shows one field per line rather than one transaction per line.
            _TextLines = textLines;

            Dictionary<string, object> fieldsByColumnName = null;

            PDFField _LastFieldRead = PDFField.None;

            int lineNum = 0; // it's 1-relative
            foreach (string line in _TextLines)
            {
                lineNum++;

                // reused vars in switch stmt:
                DateTime dateValue;
                Match match;

                switch (_LastFieldRead)
                {
                    case PDFField.None:
                        // start of budget item, so initialize fieldsByColumnName:
                        // DIAG make fieldsByColumnName its own class. Indexed by an enum insted of a string
                        fieldsByColumnName = new Dictionary<string, object>();

                        // is field a date (mm/dd)? That's the Transaction Date
                        match = Regex.Match(line, @"^(\d\d/\d\d)$");
                        if (match.Success)
                        {
                            if (DateTime.TryParse(AddYearToYearlessDateString(match.Groups[1].Value), out dateValue))
                            {
                                fieldsByColumnName["CardTransDate"] = dateValue;
                                _LastFieldRead = PDFField.TransDate;
                            }
                        }
                        else
                            _LastFieldRead = PDFField.None;
                        break;
                    case PDFField.TransDate:
                        // is field a date (mm/dd)? That's the Post Date (TrDate)
                        match = Regex.Match(line, @"^(\d\d/\d\d)$");
                        if (match.Success)
                        {
                            if (DateTime.TryParse(AddYearToYearlessDateString(match.Groups[1].Value), out dateValue))
                            {
                                fieldsByColumnName["TrDate"] = dateValue;
                                _LastFieldRead = PDFField.PostDate;
                            }
                        }
                        else
                            _LastFieldRead = PDFField.None;
                        break;
                    case PDFField.PostDate:
                        // next field is beginning of Descrip
                        fieldsByColumnName["Descrip"] = line;
                        _LastFieldRead = PDFField.Descrip;
                        break;
                    case PDFField.Descrip:
                        // next field is Reference Number, appended to Descrip field:
                        fieldsByColumnName["Descrip"] += " " + line;
                        _LastFieldRead = PDFField.RefNum;
                        break;
                    case PDFField.RefNum:
                        // next field is Acct Number, appended to Descrip field:
                        fieldsByColumnName["Descrip"] += " " + line;
                        _LastFieldRead = PDFField.AcctNum;
                        break;
                    case PDFField.AcctNum:
                        // next field is Amount:

                        if (CaptureAmountField(line, fieldsByColumnName, true, false))
                            AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);

                        // whether success or failure, go back to BofaPDFField.None:
                        _LastFieldRead = PDFField.None;
                        break;
                }
            }
        }

        public void ProcessBofAWidePDFLines(string[] textLines)
        {
            _TextLines = textLines;

            bool foundLineWithOnlyDate = false; // line with only date (at beginning) indicates an incomplete item which will hopefully be completed by a dollar amount later

            Dictionary<string, object> fieldsByColumnName = null;

            int lineNum = 0; // it's 1-relative
            foreach (string line in _TextLines)
            {
                lineNum++;

                if (foundLineWithOnlyDate)
                {
                    // Check if line is dollar amount:
                    if (CaptureAmountField(line, fieldsByColumnName, false, false))
                    {
                        AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                        foundLineWithOnlyDate = false;
                    }
                    else
                        AddToDescripField(fieldsByColumnName, line); // whole line is capture is continuation of descrip
                }
                else
                {
                    // New budget item, not a continuation of old one. So, reset field dictionary:
                    fieldsByColumnName = new Dictionary<string, object>();

                    // Check for match of date at beginning:
                    string restOfLine;
                    bool lineBeginsWithDate = CaptureDateFieldInBofAWideStatementLine(line, fieldsByColumnName, out restOfLine);
                    if (lineBeginsWithDate)
                    {
                        if (CaptureAmountField(restOfLine, fieldsByColumnName, false, false))
                        {
                            AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                        }
                        else
                        {
                            foundLineWithOnlyDate = true;

                            // rest of line is capture is continuation of descrip
                            AddToDescripField(fieldsByColumnName, restOfLine);
                        }
                    }
                }
            }
        }

        bool CaptureAmountField(string lineText, Dictionary<string, object> fieldsByColumnName, bool fromWholeLine, bool hasDollarSign)
        {
            // DIAG use this in all the areas
            // Check for match of dollar amount
            // if fromWholeLine, amount must take up whole line. Otherwise, it checks for amount at right end, and captures any text to the left (and adds to Descrip field)
            // if hasDollarSign, the amount figure is preceded by "$" (after negative sign if any)
            string amountPattern; // pattern of just amount
            if (hasDollarSign)
                amountPattern = @"-?\$(\d|,)+\.\d\d";
            else
                amountPattern = @"-?(\d|,)+\.\d\d";
            string pattern;
            if (fromWholeLine)
                pattern = @"^(" + amountPattern + ")$";
            else
                pattern = @"^(.* )?(" + amountPattern + ")$";

            Match match = Regex.Match(lineText, pattern);
            if (match.Success)
            {
                // Parse captured amount:
                string amountString = match.Groups[fromWholeLine ? 1 : 2].Value;
                if (hasDollarSign)
                    amountString = amountString.Replace("$", ""); // remove dollar sign from string so it'll parse

                Decimal amountValue;
                if (Decimal.TryParse(amountString, out amountValue))
                {
                    // Handle the special case in which source file lists credits as negative and debits as positive:
                    if (_SourceFileFormatRow.CreditsAreNegative)
                        amountValue = -amountValue;
                    fieldsByColumnName["Amount"] = amountValue;
                }

                if (!fromWholeLine)
                {
                    // 1st capture is descrip
                    AddToDescripField(fieldsByColumnName, match.Groups[1].Value);
                }
                return true;
            }
            else
                return false;
        }

        void AddToDescripField(Dictionary<string, object> fieldsByColumnName, string textValue)
        {
            if (!fieldsByColumnName.ContainsKey("Descrip"))
                fieldsByColumnName["Descrip"] = "";
            else
                fieldsByColumnName["Descrip"] += " ";
            fieldsByColumnName["Descrip"] += textValue;

        }
        // internally used classes
        public class BudgetAndSourceItemRows
        {
            public BudgetRow _BudgetRow;
            public BudgetSourceFileItemsRow _ItemsRow;

            public BudgetAndSourceItemRows(BudgetRow budgetRow, BudgetSourceFileItemsRow itemsRow)
            {
                _BudgetRow = budgetRow;
                _ItemsRow = itemsRow;
            }
        }
    }
}
