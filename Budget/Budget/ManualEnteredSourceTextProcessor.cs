using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Budget.MainDataSet;

namespace Budget
{
    internal class ManualEnteredSourceTextProcessor : SourceFileProcessor
    {
        string[] _TextLines;

        public ManualEnteredSourceTextProcessor(BudgetDataTable budgetTable, string selectedAccount, string selectedFileFormat)
            :base(budgetTable, selectedAccount, selectedFileFormat)
        {
            _TextLines = new string[0];
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

        protected override void ReadInSourceFile()
        {

            StatementDatePrompt prompt = new StatementDatePrompt();
            prompt.InitializeWithFilename(SourceFileName);
            DialogResult res = prompt.ShowDialog();
            if (res == DialogResult.OK)
            {
                // Bring up PDF (or whatever format) file:
                System.Diagnostics.Process.Start(SourceFileName);

                // diag WHAT IF we dont real;ly need the statement date?
                // make new source file row:
                _NewestSourceFileRow = _SourceFileTable.NewBudgetSourceFileRow();
                _NewestSourceFileRow.FilePath = SourceFileName;
                _NewestSourceFileRow.StatementDate = prompt.StatementDate;
                _NewestSourceFileRow.Account = this.SelectedAccount;
                _NewestSourceFileRow.ManuallyEntered = true;
                _SourceFileTable.AddBudgetSourceFileRow(_NewestSourceFileRow);
            }
        }
    }
}
