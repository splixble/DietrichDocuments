using System;
using System.Collections.Generic;
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

        BudgetSourceFileFormatTableAdapter _BudgetSourceFileFormatAdapter = new BudgetSourceFileFormatTableAdapter();
        BudgetAccountTableAdapter _BudgetAccountAdapter = new BudgetAccountTableAdapter();
        FundTableAdapter _FundAdapter = new FundTableAdapter();
        BudgetTypeGroupingsTableAdapter _GroupingsAdapter = new BudgetTypeGroupingsTableAdapter();
        AccountOwnerTableAdapter _AccountOwnerAdapter = new AccountOwnerTableAdapter();
        AccountTypeTableAdapter _AccountTypeAdapter = new AccountTypeTableAdapter();
        ViewAccountTypesWithAllOptionTableAdapter _ViewAccountTypesWithAllOptionAdapter = new ViewAccountTypesWithAllOptionTableAdapter();

        public LookupTableSet()
        {
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
            _BudgetSourceFileFormatAdapter.Connection = Program.DbConnection;
            _BudgetSourceFileFormatAdapter.Fill(_MainDataSet.BudgetSourceFileFormat);

            _BudgetAccountAdapter.Connection = Program.DbConnection; // TODO don't think we need this DbConnection any more, right?
            _BudgetAccountAdapter.Fill(_MainDataSet.BudgetAccount);

            _FundAdapter.Connection = Program.DbConnection; 
            _FundAdapter.Fill(_MainDataSet.Fund);

            _GroupingsAdapter.Connection = Program.DbConnection;
            _GroupingsAdapter.Fill(_MainDataSet.BudgetTypeGroupings);

            _AccountOwnerAdapter.Connection = Program.DbConnection;
            _AccountOwnerAdapter.Fill(_MainDataSet.AccountOwner);

            _AccountTypeAdapter.Connection = Program.DbConnection;
            _AccountTypeAdapter.Fill(_MainDataSet.AccountType);

            _ViewAccountTypesWithAllOptionAdapter.Connection = Program.DbConnection;
            _ViewAccountTypesWithAllOptionAdapter.Fill(_MainDataSet.ViewAccountTypesWithAllOption);           
        }
    }
}
