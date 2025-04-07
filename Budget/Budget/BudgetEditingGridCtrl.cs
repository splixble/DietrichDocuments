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
    // DIAG rename Transac...
    public partial class BudgetEditingGridCtrl : UserControl
    {
        public enum Usages { BalanceCalculation, CashPurchases, GroupingAssignment, SourceFile };
        Usages _Usage;

        public bool CreateNewSourceFileRow = false;

        public MainDataSet.TransacDataTable TransacTable => mainDataSet.Transac;
        public MainDataSetTableAdapters.TransacTableAdapter TransacAdapter => transacTableAdapter;

        public BindingSource BindingSrc => transacBindingSource;

        public DataGridView Grid => grid1;

        public BudgetEditingGridCtrl()
        {
            InitializeComponent();
        }

        public void Initialize(Usages usage)
        {
            _Usage = usage;
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

            transacTableAdapter.Connection = Program.DbConnection;

            TrTypeComboColumn.DataSource = Program.LookupTableSet.MainDataSet.TransacType;
            TrTypeComboColumn.ValueMember = "TrTypeID";
            TrTypeComboColumn.DisplayMember = "CodeAndName";
        }

        public void UpdateForImportedItems(SourceFileProcessor processor)
        {
            BindingSrc.Filter = processor.GetBindingSrcFilterText();
            lblStatus.Text = processor.GetImportResultStatus();
            lblStatus.Visible = true;
        }

        private void tbFilter_Validated(object sender, EventArgs e)
        {
            try
            {
                transacBindingSource.Filter = tbFilter.Text;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }
    }
}
