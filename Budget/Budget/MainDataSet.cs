using System;
using System.Data;

namespace Budget
{
    partial class MainDataSet
    {
        partial class AccountDataTable
        {
            //ViewCashAccounts
            public DataView CashAccountsView => _CashAccountsView;
            DataView _CashAccountsView;

            public override void EndInit()
            {
                _CashAccountsView = new DataView(this);
                _CashAccountsView.RowFilter = "AccountType = 'C'";
                _CashAccountsView.Sort = "AccountOwner ASC";

                base.EndInit();
            }
        }

        partial class TransacDataTable
        {
            public DataView DuplicatesView => _DuplicatesView;
            DataView _DuplicatesView;

            public override void EndInit()
            {
                _DuplicatesView = new DataView(this);
                _DuplicatesView.Sort = "TrDate ASC, Amount ASC, Descrip ASC, TrCode ASC, Account ASC";

                base.EndInit();
            }
        }
    }
}
