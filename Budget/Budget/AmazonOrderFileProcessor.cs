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
            :base(budgetTable, null, null, false)
        {
            _ViewByDateAndAmount = new DataView(budgetTable);
            _ViewByDateAndAmount.Sort = "TrDate ASC, Amount ASC";
        }

        protected override bool ExtractFields(string[] fileFields, Dictionary<string, object> fieldsByColumnName)
        {
            // Get date/time of order from column 2 (Order Date):
            DateTime dateTimeValue;
            if (DateTime.TryParse(fileFields[2], null, System.Globalization.DateTimeStyles.AdjustToUniversal, out dateTimeValue))
                fieldsByColumnName["TrDate"] = dateTimeValue.Date; // Get only date portion, not time
            else
                return false;

            // Get amount (price) from column 9 (Total Owed):
            Decimal amountValue;
            if (Decimal.TryParse(fileFields[9], out amountValue))
                fieldsByColumnName["Amount"] = -amountValue; // negate it since it's a debit
            else
                return false;

            // Get Aamzon's product description from column 23:
            fieldsByColumnName["DescripFromVendor"] = fileFields[23];

            return true;
        }

        protected override DataRowView[] FindDuplicateRowViews(Dictionary<string, object> fieldsByColumnName)
        {
            return _ViewByDateAndAmount.FindRows(new object[] { fieldsByColumnName["TrDate"], fieldsByColumnName["Amount"] });
        }
    }
}
