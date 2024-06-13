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

        public void ProcessManualLines(string[] textLines)
        {
            SourceFileFormats fileFormat = SourceFileFormat;
            if (fileFormat != SourceFileFormats.AccountBofA && fileFormat != SourceFileFormats.CreditCardBofA)
            {
                MessageBox.Show("Cannot process text if source file format of account is " + fileFormat.ToString());
                return;
            }
            string dateRegexPattern = "";
            if (fileFormat == SourceFileFormats.AccountBofA)
                dateRegexPattern = @"\d\d/\d\d/\d\d";
            else if (fileFormat == SourceFileFormats.CreditCardBofA)
                dateRegexPattern = @"\d\d/\d\d"; // no year

            // DIAG gotta get the year!!
            string placeholderYear = "2017";

            _TextLines = textLines;

            bool foundLineWithOnlyDate = false; // line with only date (at beginning) indicates an incomplete item which will hopefully be completed by a dollar amount later

            Dictionary<string, object> fieldsByColumnName = null;

            /*
             * DIAG remove the 2nd date in credit card lines!
             * DIAG gotta include this code -- in the SINGLE processing of Amount:
                            // For Amount and Balance columns, handle the special case in which source file lists credits as negative and debits as positive:
                            if (formatRow.CreditsAreNegative)
                                fileValue = -fileValue;
             */

            int lineNum = 0; // it's 1-relative
            foreach (string line in _TextLines) 
            {
                lineNum++;

                if (foundLineWithOnlyDate)
                {
                    // Check if line is dollar amount:
                    Match match = Regex.Match(line, @"^(.? )?(-?(\d|,)+\.\d\d)$");
                    if (match.Success)
                    {
                        // Capture 1, if it contains anything, is continuation of descrip:
                        if (match.Groups[1].Value != "") 
                            fieldsByColumnName["Descrip"] += " " + line;

                        // Capture 2 is Amount 
                        Decimal amountValue;
                        if (Decimal.TryParse(match.Groups[2].Value, out amountValue))
                        {
                            // Budget item is completed, so add/update it as a Budget row:
                            fieldsByColumnName["Amount"] = amountValue;
                            AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                            foundLineWithOnlyDate = false;
                        }
                    }
                    else
                    {
                        // whole line is capture is continuation of descrip
                        fieldsByColumnName["Descrip"] += " " + line;
                    }
                }
                else
                {
                    // reset field dictionary:
                    fieldsByColumnName = new Dictionary<string, object>();

                    // Check for match of date at beginning, dollar amount at end, separated by just spaces (BofA checking/saving statements)
                    // DIAG Change this to do the date extraction & parse only once!!
                    string pattern = @"^(" + dateRegexPattern + @") (.+) (-?(\d|,)+\.\d\d)$"; // DIAG alter to use it always
                    Match match = Regex.Match(line, @"^("+ dateRegexPattern + @") (.+) (-?(\d|,)+\.\d\d)$");
                    if (match.Success)
                    {
                        // 1st capture is TrDate
                        DateTime dateValue;
                        string dateString = match.Groups[1].Value;
                        if (fileFormat == SourceFileFormats.CreditCardBofA)
                            dateString += "/" + placeholderYear; // DIAG replace placeholder, and of course increment year if december
                        if (DateTime.TryParse(dateString, out dateValue))
                            fieldsByColumnName["TrDate"] = dateValue;
                        else
                            continue;

                        // 2nd capture is descrip
                        fieldsByColumnName["Descrip"] = match.Groups[2].Value;

                        // 3rd capture is Amount 
                        Decimal amountValue;
                        if (Decimal.TryParse(match.Groups[3].Value, out amountValue))
                            fieldsByColumnName["Amount"] = amountValue;
                        else
                            continue;

                        // if it gets to here, add/update it as a Budget row: 
                        AddOrUpdateBudgetRow(fieldsByColumnName, lineNum);
                    }
                    else
                    {
                        // Check for just match of date at beginning (but capturing rest of line for desrip). This indicates an incomplete DateOnly line:
                        Match dateMatch = Regex.Match(line, @"^(""+ dateRegexPattern + @"") (.+)$");
                        if (dateMatch.Success)
                        {
                            // 1st capture is TrDate
                            DateTime dateValue;
                            string dateString = match.Groups[1].Value;
                            if (fileFormat == SourceFileFormats.CreditCardBofA)
                                dateString += "/" + placeholderYear; // DIAG replace placeholder, and of course increment year if december
                            if (DateTime.TryParse(dateString, out dateValue))
                            {
                                fieldsByColumnName["TrDate"] = dateValue;
                                foundLineWithOnlyDate = true;
                            }
                            else
                                continue;

                            // 2nd capture is descrip -- which will be added to in later lines:
                            fieldsByColumnName["Descrip"] = dateMatch.Groups[2].Value;
                        }
                    }
                }
            }
        }
    }
}
