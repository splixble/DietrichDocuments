using System;
using System.Data;

namespace Budget
{
    partial class MainDataSet
    {
        partial class BudgetDataTable
        {
            public DataView DuplicatesView => _DuplicatesView;
            DataView _DuplicatesView;

            public override void EndInit()
            {
                _DuplicatesView = new DataView(this);
                _DuplicatesView.Sort = "TrDate ASC, Amount ASC, Descrip ASC, Account ASC";

                base.EndInit();
            }
        }
    }
}
