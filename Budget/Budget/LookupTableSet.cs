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
        BudgetTypeGroupingsTableAdapter _GroupingsAdapter = new BudgetTypeGroupingsTableAdapter();

        public LookupTableSet()
        {
        }

        public void LoadWithRetryOption()
        {
            bool exceptionThrown = false;
            do
            {
                try
                {
                    Load();
                    exceptionThrown = false;
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show("Database error: \n" + ex.Message, "Error loading tables", MessageBoxButtons.RetryCancel);
                    if (result == DialogResult.Cancel)
                    {
                        Application.Exit();
                    }
                    exceptionThrown = true;
                }
            }
            while (exceptionThrown);
        }

        void Load()
        {
            _BudgetSourceFileFormatAdapter.Connection = Program.DbConnection;
            _BudgetSourceFileFormatAdapter.Fill(_MainDataSet.BudgetSourceFileFormat);

            _BudgetAccountAdapter.Connection = Program.DbConnection;
            _BudgetAccountAdapter.Fill(_MainDataSet.BudgetAccount);

            _GroupingsAdapter.Connection = Program.DbConnection;
            _GroupingsAdapter.Fill(_MainDataSet.BudgetTypeGroupings);
        }
    }
}
