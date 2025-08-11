using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Budget.MainDataSetTableAdapters;

namespace Budget
{
    internal class LookupTableSet
    {
        public MainDataSet MainDataSet => _MainDataSet;
        MainDataSet _MainDataSet= new MainDataSet();

        SourceFileFormatTableAdapter _SourceFileFormatAdapter = new SourceFileFormatTableAdapter();
        AccountTableAdapter _BudgetAccountAdapter = new AccountTableAdapter();
        FundTableAdapter _FundAdapter = new FundTableAdapter();
        TransacTypeTableAdapter _TransacTypeAdapter = new TransacTypeTableAdapter();
        AccountOwnerTableAdapter _AccountOwnerAdapter = new AccountOwnerTableAdapter();
        AccountTypeTableAdapter _AccountTypeAdapter = new AccountTypeTableAdapter();
        RefundTableAdapter _RefundAdapter = new RefundTableAdapter();

        public DataView FundViewBySymbol => _FundViewBySymbol;
        DataView _FundViewBySymbol;

        public LookupTableSet()
        {
            _FundViewBySymbol = new DataView(MainDataSet.Fund, null, "StockSymbol", DataViewRowState.CurrentRows);
        }

        public bool LoadWithRetryOption()
        {
            bool exceptionThrown = false;
            do
            {
                try
                {
                    Load();
                    exceptionThrown = false;
                    return true;
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show("Database error: \n" + ex.Message, "Error loading tables", MessageBoxButtons.RetryCancel);
                    if (result == DialogResult.Cancel)
                    {
                        Application.Exit();
                        return false;
                    }
                    exceptionThrown = true;
                }
            }
            while (exceptionThrown);

            return !exceptionThrown; // this will never be called - right?
        }

        void Load()
        {
            _SourceFileFormatAdapter.Connection = Program.DbConnection;
            _SourceFileFormatAdapter.Fill(_MainDataSet.SourceFileFormat);

            _BudgetAccountAdapter.Connection = Program.DbConnection; // TODO don't think we need this DbConnection any more, right?
            _BudgetAccountAdapter.Fill(_MainDataSet.Account);

            _FundAdapter.Connection = Program.DbConnection; 
            _FundAdapter.Fill(_MainDataSet.Fund);

            _TransacTypeAdapter.Connection = Program.DbConnection;
            _TransacTypeAdapter.Fill(_MainDataSet.TransacType);

            _AccountOwnerAdapter.Connection = Program.DbConnection;
            _AccountOwnerAdapter.Fill(_MainDataSet.AccountOwner);

            _AccountTypeAdapter.Connection = Program.DbConnection;
            _AccountTypeAdapter.Fill(_MainDataSet.AccountType);

            _RefundAdapter.Connection = Program.DbConnection;
            _RefundAdapter.Fill(_MainDataSet.Refund);
        }
    }
}
