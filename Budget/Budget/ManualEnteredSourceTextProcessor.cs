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

        bool MatchDateInBofAStatementLine(string line, Dictionary<string, object> fieldsByColumnName, out string restOfLine)
        {
            restOfLine = ""; // initialize

            // Get year from Statement Date:
            // DIAG drop out if the file isnt loaded
            int statementDateYear = _NewestSourceFileRow.StatementDate.Year;

            string pattern = null;
            // Capture date, ignore space if exists, and rest of line:
            if (_SourceFileFormat == SourceFileFormats.AccountBofA)
                pattern = @"^(\d\d/\d\d/\d\d)[ ]?(.*)$";
            else if (_SourceFileFormat == SourceFileFormats.CreditCardBofA)
                pattern = @"^\d\d/\d\d (\d\d/\d\d)[ ]?(.+)$";  // no year in date. Also, throw away first date (Transaction Date) since we're using 2nd, Posting Date

            Match match = Regex.Match(line, pattern);
            if (match.Success)
            {
                // 2nd capture is rest of line
                restOfLine = match.Groups[2].Value;

                // 1st capture is TrDate
                DateTime dateValue;
                string dateString = match.Groups[1].Value;
                if (_SourceFileFormat == SourceFileFormats.CreditCardBofA)
                {
                    // BofA credid card statements don't list year, so we have to add year to string before parsing.
                    // The transaction might be the year BEFORE the statement year, though, if it's in December (or a month >= 10).
                    int tYear = statementDateYear;
                    if (dateString[0] == '1')
                        tYear--;
                    dateString += "/" + tYear.ToString();
                }
                if (DateTime.TryParse(dateString, out dateValue))
                {
                    fieldsByColumnName["TrDate"] = dateValue;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public void ProcessManualLines(string[] textLines)
        {
            if (_SourceFileFormat != SourceFileFormats.AccountBofA && _SourceFileFormat != SourceFileFormats.CreditCardBofA)
            {
                MessageBox.Show("Cannot process text if source file format of account is " + _SourceFileFormat.ToString());
                return;
            }

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
                    bool lineBeginsWithDate = MatchDateInBofAStatementLine(line, fieldsByColumnName, out restOfLine);
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

        /* REMOVE
        public override void SaveChanges()
        {
            // DIAG dont prompt!

            ManuallyEnteredSourceFileSavePrompt prompt = new ManuallyEnteredSourceFileSavePrompt();
            DialogResult res = prompt.ShowDialog();
            if (res == DialogResult.OK)
            {
                // DIAG Hey, we need to know StatementDate BEFORE we save, so that we can add year to dates. How bout we just open PDF's? And read in its filename?
                // should work: System.Diagnostics.Process.Start(@"file path");

                // make new source file row:
                _NewestSourceFileRow = _SourceFileTable.NewBudgetSourceFileRow();
                _NewestSourceFileRow.FilePath = prompt.FileName;
                _NewestSourceFileRow.StatementDate = prompt.StatementDate;
                _NewestSourceFileRow.Account = this.SelectedAccount;
                _NewestSourceFileRow.ManuallyEntered = true;
                _SourceFileTable.AddBudgetSourceFileRow(_NewestSourceFileRow);

                base.SaveChanges();
            }
        }
        */

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
