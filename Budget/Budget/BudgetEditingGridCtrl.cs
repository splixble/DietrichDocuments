using Budget.MainDataSetTableAdapters;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    public partial class BudgetEditingGridCtrl : UserControl
    {
        public bool CreateNewSourceFileRow = false;

        public MainDataSet.BudgetDataTable BudgetTable => mainDataSet.Budget;
        public MainDataSetTableAdapters.BudgetTableAdapter BudgetAdapter => budgetTableAdapter;

        public BindingSource BindingSrc => budgetBindingSource;

        public DataGridView Grid => grid1;

        public BudgetEditingGridCtrl()
        {
            InitializeComponent();
        }

        private void gridBudgetItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Utils.SetImportedDataGridRowColors(grid1);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // In Design mode?
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            budgetTableAdapter.Connection = Program.DbConnection;

            TrTypeComboColumn.DataSource = Program.LookupTableSet.MainDataSet.BudgetTypeGroupings;
            TrTypeComboColumn.ValueMember = "TrTypeID";
            TrTypeComboColumn.DisplayMember = "CodeAndName";
        }

        private void tbFilter_Validated(object sender, EventArgs e)
        {
            try
            {
                budgetBindingSource.Filter = tbFilter.Text;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }
    }
}
