using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Budget.MainDataSet;

namespace Budget
{
    internal class AmazonOrderFileProcessor : SourceFileProcessor
    {
        DataView _ViewByDateAndAmount;

        public override bool UpdateAccountFromSourceFile => false;
        public override bool AllowAddingNewBudgetRows => false;

        public AmazonOrderFileProcessor(BudgetDataTable budgetTable)
            :base(budgetTable, null)
        {
            _ViewByDateAndAmount = new DataView(budgetTable);
            _ViewByDateAndAmount.Sort = "TrDate ASC, Amount ASC";
        }

        protected override bool ExtractFields(string[] fileFields, Dictionary<string, object> fieldsByColumnName)
        {
            // Get date/time of order from column 2:
            DateTime dateTimeValue;
            if (DateTime.TryParse(fileFields[2], out dateTimeValue))
                fieldsByColumnName["TrDate"] = dateTimeValue.Date; // Get only date portion, not time
            else
                return false;

            // Get amount (price) from column 10:
            Decimal amountValue;
            if (Decimal.TryParse(fileFields[10], out amountValue))
                fieldsByColumnName["Amount"] = -amountValue; // negate it since it's a debit
            else
                return false;

            // Get Aamzon's product description from column 23:
            fieldsByColumnName["DescripFromVendor"] = fileFields[23];

            return true;
        }

        protected override MainDataSet.BudgetRow FindDuplicateRow(Dictionary<string, object> fieldsByColumnName)
        {
            DataRowView[] dupRowViews = _ViewByDateAndAmount.FindRows(new object[] { fieldsByColumnName["TrDate"], fieldsByColumnName["Amount"] });
            // DIAG maybe check Description for "Amazon" as well?
            if (dupRowViews.Length > 1)
                throw (new Exception("Multiple duplicate Budget rows"));
            else if (dupRowViews.Length == 1)
                return dupRowViews[0].Row as BudgetRow;
            else
                return null;

        }
    }
}
