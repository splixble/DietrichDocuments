using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            _TextLines = textLines;

            int lineNum = 0; // it's 1-relative
            foreach (string line in _TextLines) 
            {
                lineNum++;

                Dictionary<string, object> fieldsByColumnName = new Dictionary<string, object>();

                // here's the first stab at extraction... BofA Checking:
                string pattern = @"^(\d\d/\d\d/\d\d) (.+) (-?(\d|,)+.\d\d)$";

                // DIAG this works, but only when they're in same line. Gotta handle split lines, if they just have date (var DateOnlyFound). Then the checking account stmts.

                Match match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    // 1st capture is TrDate
                    DateTime dateValue;
                    if (DateTime.TryParse(match.Groups[1].Value, out dateValue))
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
            }
        }
    }
}
