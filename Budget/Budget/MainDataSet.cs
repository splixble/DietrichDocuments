using System;
using System.Data;

namespace Budget
{
    partial class MainDataSet
    {
        partial class BudgetDataTable
        {
            DataView _DuplicatesView;
  
            public override void EndInit()
            {
                _DuplicatesView = new DataView(this);
                _DuplicatesView.Sort = "TrDate ASC, Amount ASC, Descrip ASC, Account ASC";

                base.EndInit();
            }

            public BudgetRow FindDuplicate(DateTime trDate, decimal amount, string descrip, string account)
            {
                // DIAG s/b _DuplicatesView.FindRows, in case there's multiple matches... or, s/b a custom made DataView per format...or just linear search...
                DataRowView[] dupRowViews =_DuplicatesView.FindRows(new object[] { trDate, amount, descrip, account });
                if (dupRowViews.Length > 1)
                    throw (new Exception("Multiple duplicate Budget rows"));
                else if (dupRowViews.Length == 1)
                    return dupRowViews[0].Row as BudgetRow;
                else
                    return null;
            }
        }
    }
}
