using Budget.MainDataSetTableAdapters;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Budget.BudgetSourceFileProcessor;
using static Budget.MainDataSet;

namespace Budget
{
    internal abstract class SourceFileProcessor
    {
        public bool IsManuallyEntered => _IsManuallyEntered;
        bool _IsManuallyEntered;

        MainDataSet.BudgetSourceFileFormatDataTable SourceFileFormatTable => Program.LookupTableSet.MainDataSet.BudgetSourceFileFormat;

        protected MainDataSet.BudgetSourceFileFormatRow _SourceFileFormatRow = null;

        protected SourceFileFormats _SourceFileFormat;

        MainDataSet.BudgetSourceFileDataTable _SourceFileTable = new MainDataSet.BudgetSourceFileDataTable();
        MainDataSet.BudgetSourceFileRow _NewestSourceFileRow = null;
        MainDataSet.BudgetSourceFileItemsDataTable _SourceFileItemsTable = new MainDataSet.BudgetSourceFileItemsDataTable();

        public string SourceFilePath => _SourceFilePath;
        protected string _SourceFilePath;

        protected string[] _TextLines = null; // if manually entered

        public List<PrimaryKeyValue> MatchingPrimaryKeys => _MatchingPrimaryKeys;
        List<PrimaryKeyValue> _MatchingPrimaryKeys = new List<PrimaryKeyValue>();

        public virtual bool AllowAddingNewImportedRows => true;

        string DownloadsDirectory
        {
            get
            {
                return "C:\\Users\\Dietr\\Downloads"; //  make it a config field DIAG
            }
        }

        protected abstract DataTable ImportedTable { get; } // the table the source file items get written to

        public SourceFileProcessor(string selectedFileFormat, bool isManuallyEntered) 
        {
            _IsManuallyEntered = isManuallyEntered;

            _SourceFileFormatRow = SourceFileFormatTable.FindByFormatCode(selectedFileFormat);

            _TextLines = new string[0];


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

        protected BudgetSourceFileItemsRow AddSourceFileItemRow(int lineNum)
        {
            BudgetSourceFileItemsRow fileItemsRow = _SourceFileItemsTable.NewBudgetSourceFileItemsRow();
            fileItemsRow.SourceFile = -1; // real value filled in on save 
            fileItemsRow.SourceFileLine = lineNum; // 1-relative
            _SourceFileItemsTable.AddBudgetSourceFileItemsRow(fileItemsRow);
            return fileItemsRow;
        }

        // Extracts fields from a line of the text file, puts them in fieldsByColumnName.
        // Returns whether line was parseable.
        // Can be overridden for custom source files like Amazon order lists.
        protected virtual bool ExtractFields(string[] fileFields, ColumnValueList fieldsByColumnName)
        {
            string[] formatFields = _SourceFileFormatRow.FormatColumns.Split(',');

            for (int formatColIndex = 0; formatColIndex < formatFields.Length; formatColIndex++)
            {
                if (formatFields[formatColIndex] != "")
                {
                    if (formatColIndex >= fileFields.Length)
                        return false;

                    // which dest. column is it?
                    if (!ImportedTable.Columns.Contains(formatFields[formatColIndex]))
                        return false;
                    DataColumn destColumn = ImportedTable.Columns[formatFields[formatColIndex]];

                    // Parse the fields, depending on the column's data type:
                    if (destColumn.DataType == typeof(string))
                        fieldsByColumnName[destColumn.ColumnName] = fileFields[formatColIndex];
                    else if (destColumn.DataType == typeof(Decimal))
                    {
                        Decimal fileValue;
                        if (Decimal.TryParse(fileFields[formatColIndex], out fileValue))
                        {
                            // For Amount and Balance columns, handle the special case in which source file lists credits as negative and debits as positive:
                            if (_SourceFileFormatRow.CreditsAreNegative)
                                fileValue = -fileValue;

                            fieldsByColumnName[destColumn.ColumnName] = fileValue;
                        }
                        else
                            return false;
                    }
                    else if (destColumn.DataType == typeof(DateTime))
                    {
                        DateTime fileValue;
                        if (DateTime.TryParse(fileFields[formatColIndex], out fileValue))
                            fieldsByColumnName[destColumn.ColumnName] = fileValue;
                        else
                            return false;
                    }
                }
            }
            return true;
        }

        protected virtual string AccountSourceFilePath
        {
            get
            {
                return "D:\\Dietrich\\Business\\Budget App Input Files"; // DIAG get from Config file, which s/b read at beginning and globally available
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

        // Finds a DataRow for each imported row that's considered a duplicate, i.e. represents the same transaction or end-of-day share value or whatever
        protected abstract DataRow[] FindDuplicateRows(ColumnValueList fieldsByColumnName);

        protected DataRow[] DataRowArrayFromDataRowViewArray(DataRowView[] rowViews)
        {
            // this is a general convenience utility
            DataRow[] rows = new DataRow[rowViews.Length];
            for (int i = 0; i < rowViews.Length; i++)
                rows[i] = rowViews[i].Row;
            return rows;
        }

        protected DataRow FindDuplicateRow(ColumnValueList fieldsByColumnName)
        {
            DataRow[] dupRows = FindDuplicateRows(fieldsByColumnName);
            if (dupRows.Length == 0)
                return null;
            else if (dupRows.Length == 1)
                return dupRows[0];
            else
            {
                string dupIDList = "";
                foreach (DataRow dupRow in dupRows)
                {
                    /* DIAG remove
                    string keyValueString = "";
                    foreach (DataColumn keyColumn in ImportedTable.PrimaryKey)
                    {
                        string keyVal = dupRowView.Row[keyColumn].ToString();
                        if (keyValueString != "")
                            keyValueString += ", ";
                        keyValueString += keyVal.ToString();
                    }
                    */

                    if (dupIDList != "")
                        dupIDList += "; ";
                    dupIDList += new PrimaryKeyValue(dupRow).ToString();
                }
                MessageBox.Show("Multiple duplicate rows, with keys " + dupIDList);
                return null;
            }
        }





        protected abstract DataRow NewImportedRow();

        public virtual DataRow AddOrUpdateImportedRow(ColumnValueList fieldsByColumnName, int lineNum)
            // returns imported row added or updated, or null if there's an error
        {
            DataRow importedRow = null;

            // Check for duplicate Budget rows:
            DataRow dupRow = FindDuplicateRow(fieldsByColumnName);

            if (dupRow != null)
            {
                // Duplicate row found
                importedRow = dupRow;
                // Add row's primary key(s):
                _MatchingPrimaryKeys.Add(new PrimaryKeyValue(dupRow)); ;
            }
            else
            {
                // no duplicate
                if (AllowAddingNewImportedRows)
                {
                    // add new row to table
                    importedRow = NewImportedRow();
                }
            }

            // if no duplicate is found, and we're not allowed to add rows, then just move to the next line in src file:
            if (importedRow == null)
                return null;

            // Copy fields in:
            foreach (string columnName in fieldsByColumnName.Keys)
            {
                object newFieldValue = fieldsByColumnName[columnName];
                if (newFieldValue != null)
                    importedRow[columnName] = newFieldValue;
            }

            // if new row, we need to add it to the DataTable:
            if (dupRow == null)
                ImportedTable.Rows.Add(importedRow);

            BudgetSourceFileItemsRow fileItemsRow = AddSourceFileItemRow(lineNum);

            _ImportedBudgetItems.Add(new ImportedAndSourceItemRows(importedRow, fileItemsRow));

            return importedRow;
        }





        public void Process()
        {
            _SourceFilePath = PromptForSourceFile(false);
            if (_SourceFilePath != null)
                ReadInSourceFile();
        }

        public virtual void ProcessManualLines(string[] textLines)
        {
            // TODO this should do something other than wimping out, rite?
            MessageBox.Show("Base class cannot process text if source file format of account is " + _SourceFileFormat.ToString());
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

        protected virtual void ReadInSourceFile()
        {
            if (IsManuallyEntered)
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
                    _NewestSourceFileRow.ManuallyEntered = true;
                    _SourceFileTable.AddBudgetSourceFileRow(_NewestSourceFileRow);
                }
            }
            else
            {
                // make new source file row:
                _NewestSourceFileRow = _SourceFileTable.NewBudgetSourceFileRow();
                _NewestSourceFileRow.FilePath = _SourceFilePath;
                _NewestSourceFileRow.ManuallyEntered = false;
                _SourceFileTable.AddBudgetSourceFileRow(_NewestSourceFileRow);

                // use Microsoft.VisualBasic.FileIO objects (TextFieldParser, TextFieldType) to load source files:
                using (TextFieldParser parser = new TextFieldParser(_SourceFilePath))
                {

                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // allow tab delim?
                    int lineNum = 0; // it's 1-relative
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        string[] fileFields = parser.ReadFields();
                        lineNum++;

                        ColumnValueList fieldsByColumnName = new ColumnValueList();

                        bool lineParsable = ExtractFields(fileFields, fieldsByColumnName);

                        if (lineParsable)
                            AddOrUpdateImportedRow(fieldsByColumnName, lineNum);
                    }
                }
            }
        }

        protected abstract void SaveImportedTable();

        protected abstract void PointSourceFileItemRowToImportedRow(ImportedAndSourceItemRows rowsOb);

        public void SaveChanges()
        {
            // TODO use transactions (budgetTableAdapter.Transaction) in this
            SaveImportedTable();

            foreach (ImportedAndSourceItemRows rowsOb in _ImportedBudgetItems)
                PointSourceFileItemRowToImportedRow(rowsOb);

            MainDataSetTableAdapters.BudgetSourceFileTableAdapter sourceFileAdap = new BudgetSourceFileTableAdapter();
            _NewestSourceFileRow.ImportDateTime = DateTime.Now;
            sourceFileAdap.Update(_SourceFileTable);

            foreach (BudgetSourceFileItemsRow itemRow in _SourceFileItemsTable)
                // fill in new Items rows with updated, permanent ID field value:
                itemRow.SourceFile = _NewestSourceFileRow.FileID;

            MainDataSetTableAdapters.BudgetSourceFileItemsTableAdapter fileItemsAdap = new BudgetSourceFileItemsTableAdapter();
            fileItemsAdap.Update(_SourceFileItemsTable);
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

        List<ImportedAndSourceItemRows> _ImportedBudgetItems = new List<ImportedAndSourceItemRows>();

        // internally used class
        protected class ImportedAndSourceItemRows
        {
            public DataRow _ImportedRow;
            public BudgetSourceFileItemsRow _ItemsRow;

            public ImportedAndSourceItemRows(DataRow importedRow, BudgetSourceFileItemsRow itemsRow)
            {
                _ImportedRow = importedRow;
                _ItemsRow = itemsRow;
            }
        }
    }
}
