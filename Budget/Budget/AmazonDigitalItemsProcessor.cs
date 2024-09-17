using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Budget.MainDataSet;

namespace Budget
{
    internal class AmazonDigitalItemsProcessor : SourceFileProcessor
    {
        DataView _ViewByDateAndAmount;

        public override bool UpdateAccountFromSourceFile => false;
        public override bool AllowAddingNewBudgetRows => false;

        public AmazonDigitalItemsProcessor(BudgetDataTable budgetTable)
            : base(budgetTable, null, null)
        {
            _ViewByDateAndAmount = new DataView(budgetTable);
            _ViewByDateAndAmount.Sort = "TrDate ASC, Amount ASC";
        }

        protected override bool ExtractFields(string[] fileFields, Dictionary<string, object> fieldsByColumnName)
        {
            // Get date of order from column 9:
            DateTime dateTimeValue;
            if (DateTime.TryParse(fileFields[9], out dateTimeValue))
                fieldsByColumnName["TrDate"] = dateTimeValue;
            else
                return false;

            // Get amount (price) from column 11:
            Decimal amountValue;
            if (Decimal.TryParse(fileFields[11], out amountValue))
                fieldsByColumnName["Amount"] = -amountValue; // negate it since it's a debit
            else
                return false;

            // Get Aamzon's product description from column 1:
            fieldsByColumnName["DescripFromVendor"] = fileFields[1];

            return true;
        }

        protected override DataRowView[] FindDuplicateRowViews(Dictionary<string, object> fieldsByColumnName)
        {
            return _ViewByDateAndAmount.FindRows(new object[] { fieldsByColumnName["TrDate"], fieldsByColumnName["Amount"] });
        }
    }
}
