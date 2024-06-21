using System;
using System.Collections.Generic;
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

        public ManualEnteredSourceTextProcessor(BudgetDataTable budgetTable, string selectedAccount)
            :base(budgetTable, selectedAccount)
        {
            _TextLines = new string[0];
        }

        bool MatchDateInBofAWideStatementLine(string line, Dictionary<string, object> fieldsByColumnName, out string restOfLine)
        {
            restOfLine = ""; // initialize

            string pattern = null;
            // Capture date, ignore space if exists, and rest of line:
            if (_SourceFileFormat == SourceFileFormats.AccountBofA)
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
            else if (_SourceFileFormat == SourceFileFormats.CreditCardBofA)
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
            // DIAG drop out if the file isnt loaded
            int statementDateYear = _NewestSourceFileRow.StatementDate.Year;

            // The transaction might be the year BEFORE the statement year, though, if it's in December (or a month >= 10).
            int tYear = statementDateYear;
            if (dateString[0] == '1')
                tYear--;
            return dateString + "/" + tYear.ToString();
        }

        public void ProcessManualLines(string[] textLines)
        {
            switch (_SourceFileFormat)
            {
                case SourceFileFormats.AccountBofA:
                case SourceFileFormats.CreditCardBofA:
                    ProcessBofAWidePDFLines(textLines);
                    break;
                case SourceFileFormats.CreditCardBofANarrowPDF:
                    ProcessBofANarrowPDFLines(textLines);
                    break;
                default:
                    MessageBox.Show("Cannot process text if source file format of account is " + _SourceFileFormat.ToString());
                    break;
            }
        }

        enum BofaPDFField { None, TransDate, PostDate, Descrip, RefNum, AcctNum, Amount }; // used inProcessBofANarrowPDFLines

        public void ProcessBofANarrowPDFLines(string[] textLines)
        {
            // For BofA Statement PDFs that, when text is copied and pasted to text box, shows one field per line rather than one transaction per line.
            _TextLines = textLines;

            Dictionary<string, object> fieldsByColumnName = null;

            BofaPDFField _LastFieldRead = BofaPDFField.None;

            int lineNum = 0; // it's 1-relative
            foreach (string line in _TextLines)
            {
                lineNum++;

                // reused vars in switch stmt:
                DateTime dateValue;
                Decimal amountValue;
                Match match;

                switch (_LastFieldRead)
                {
                    case BofaPDFField.None:
                        // start of budget item, so initialize fieldsByColumnName:
                        fieldsByColumnName = new Dictionary<string, object>();

                        // is field a date (mm/dd)? That's the Transaction Date
                        match = Regex.Match(line, @"^(\d\d/\d\d)$");
                        if (match.Success)
                        {
                            if (DateTime.TryParse(AddYearToYearlessDateString(match.Groups[1].Value), out dateValue))
                            {
                                fieldsByColumnName["CardTransDate"] = dateValue;
                                _LastFieldRead = BofaPDFField.TransDate;
                            }
                        }
                        else
                            _LastFieldRead = BofaPDFField.None;
                        break;
                    case BofaPDFField.TransDate:
                        // is field a date (mm/dd)? That's the Post Date (TrDate)
                        match = Regex.Match(line, @"^(\d\d/\d\d)$");
                        if (match.Success)
                        {
                            if (DateTime.TryParse(AddYearToYearlessDateString(match.Groups[1].Value), out dateValue))
                            {
                                fieldsByColumnName["TrDate"] = dateValue;
                                _LastFieldRead = BofaPDFField.PostDate;
                            }
                        }
                        else
                            _LastFieldRead = BofaPDFField.None;
                        break;
                    case BofaPDFField.PostDate:
                        // next field is beginning of Descrip
                        fieldsByColumnName["Descrip"] = line;
                        _LastFieldRead = BofaPDFField.Descrip;
                        break;
                    case BofaPDFField.Descrip:
                        // next field is Reference Number, appended to Descrip field:
                        fieldsByColumnName["Descrip"] += " " + line;
                        _LastFieldRead = BofaPDFField.RefNum;
                        break;
                    case BofaPDFField.RefNum:
                        // next field is Acct Number, appended to Descrip field:
                        fieldsByColumnName["Descrip"] += " " + line;
                        _LastFieldRead = BofaPDFField.AcctNum;
                        break;
                    case BofaPDFField.AcctNum:
                        // next field is Amount:
                        match = Regex.Match(line, @"^(-?(\d|,)+\.\d\d)$");
                        if (match.Success)
                        {
                            if (Decimal.TryParse(match.Groups[1].Value, out amountValue))
                            {
                                // Handle the special case in which source file lists credits as negative and debits as positive:
                                if (_SourceFileFormatRow.CreditsAreNegative)
                                    amountValue = -amountValue;
                                fieldsByColumnName["Amount"] = amountValue;
                                AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                            }
                        }
                        // whether success or failure, go back to BofaPDFField.None:
                        _LastFieldRead = BofaPDFField.None;
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
                    if (MatchAmountInBofAStatementLine(line, fieldsByColumnName))
                    { 
                        AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                        foundLineWithOnlyDate = false;
                    }
                    else
                    {
                        // whole line is capture is continuation of descrip
                        if (!fieldsByColumnName.ContainsKey("Descrip"))
                            fieldsByColumnName["Descrip"] = "";
                        else
                            fieldsByColumnName["Descrip"] += " ";
                        fieldsByColumnName["Descrip"] += line;
                    }
                }
                else
                {
                    // New budget item, not a continuation of old one. So, reset field dictionary:
                    fieldsByColumnName = new Dictionary<string, object>();

                    // Check for match of date at beginning:
                    string restOfLine;
                    bool lineBeginsWithDate = MatchDateInBofAWideStatementLine(line, fieldsByColumnName, out restOfLine);
                    if (lineBeginsWithDate)
                    {
                        if (MatchAmountInBofAStatementLine(restOfLine, fieldsByColumnName))
                        {
                            AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                        }
                        else
                        {
                            foundLineWithOnlyDate = true;

                            // rest of line is capture is continuation of descrip
                            if (!fieldsByColumnName.ContainsKey("Descrip"))
                                fieldsByColumnName["Descrip"] = "";
                            else
                                fieldsByColumnName["Descrip"] += " ";
                            fieldsByColumnName["Descrip"] += restOfLine;
                        }
                    }
                }
            }
        }

        bool MatchAmountInBofAStatementLine(string restOfLine, Dictionary<string, object> fieldsByColumnName)
        { 
            // Check for match of dollar amount at end, and capture any text before it (to add to Descrip field)
            string pattern = @"^(.* )?(-?(\d|,)+\.\d\d)$"; 
            Match match = Regex.Match(restOfLine, pattern);
            if (match.Success)
            {
                // 2nd capture is Amount 
                Decimal amountValue;
                if (Decimal.TryParse(match.Groups[2].Value, out amountValue))
                {
                    // Handle the special case in which source file lists credits as negative and debits as positive:
                    if (_SourceFileFormatRow.CreditsAreNegative)
                        amountValue = -amountValue;
                    fieldsByColumnName["Amount"] = amountValue;
                }

                // 1st capture is descrip
                if (!fieldsByColumnName.ContainsKey("Descrip"))
                    fieldsByColumnName["Descrip"] = "";
                else
                    fieldsByColumnName["Descrip"] += " ";
                fieldsByColumnName["Descrip"] += match.Groups[1].Value;

                return true;
            }
            else
                return false;
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
