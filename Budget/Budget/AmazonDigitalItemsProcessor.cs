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
    internal class AmazonDigitalItemsProcessor : BudgetSourceFileProcessor
    {
        DataView _ViewByDateAndAmount;

        public override bool UpdateAccountFromSourceFile => false;
        public override bool AllowAddingNewImportedRows => false;

        public AmazonDigitalItemsProcessor(TransacDataTable budgetTable)
            : base(budgetTable, null, null, false)
        {
            _ViewByDateAndAmount = new DataView(budgetTable);
            _ViewByDateAndAmount.Sort = "TrDate ASC, Amount ASC";
        }

        protected override bool ExtractFields(string[] fileFields, ColumnValueList fieldsByColumnName)
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

            // Get Amazon's product description from column 1:
            fieldsByColumnName["DescripFromVendor"] = fileFields[1];

            return true;
        }

        protected override DataRow[] FindDuplicateRows(ColumnValueList fieldsByColumnName)
        {
            return DataRowArrayFromDataRowViewArray(
                _ViewByDateAndAmount.FindRows(new object[] { fieldsByColumnName["TrDate"], fieldsByColumnName["Amount"] }) );
        }
    }
}
