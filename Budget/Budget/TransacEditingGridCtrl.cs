using Budget.MainDataSetTableAdapters;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    public partial class TransacEditingGridCtrl : UserControl
    {
        public enum Usages { BalanceCalculation, CashPurchases, GroupingAssignment, SourceFile };
        Usages _Usage;

        public bool CreateNewSourceFileRow = false;

        public MainDataSet.TransacDataTable TransacTable => mainDataSet.Transac;
        public MainDataSetTableAdapters.TransacTableAdapter TransacAdapter => transacTableAdapter;

        public BindingSource BindingSrc => transacBindingSource;

        public DataGridView Grid => grid1;

        public TransacEditingGridCtrl()
        {
            InitializeComponent();
        }

        public void Initialize(Usages usage)
        {
            _Usage = usage;

            // allow adding rows?
            if (_Usage == Usages.CashPurchases)
                grid1.AllowUserToAddRows = true;

            Debug.WriteLine("Spud!!!! DIAG");

            grid1.CellEndEdit += Grid1_CellEndEdit;
            grid1.CellFormatting += Grid1_CellFormatting;
        }

        private void Grid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == amountDataGridViewTextBoxColumn.Index)
                Debug.WriteLine("DIAG Cell Formatting!! ");
        }

        private void Grid1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == amountDataGridViewTextBoxColumn.Index)
                Debug.WriteLine("DIAG Cell End Edit!! ");
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

            // Hide unused grid columns:
            switch (_Usage)
            {
                case Usages.CashPurchases: 
                    this.accountDataGridViewTextBoxColumn.Visible = false;
                    this.Descrip2Column.Visible = false;
                    this.DescripFromVendorColumn.Visible = false;
                    this.CardTransDateColumn.Visible = false;
                    this.Balance.Visible = false;
                    this.BalanceIsCalculatedColumn.Visible = false;
                    this.IsIncomeColumn.Visible = false;    
                    this.acctTransferDataGridViewCheckBoxColumn.Visible = false;
                    this.trCodeDataGridViewTextBoxColumn.Visible = false;
                    break;
            }

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
