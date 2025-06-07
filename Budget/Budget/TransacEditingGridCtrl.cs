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
        public enum Usages { None, BalanceCalculation, CashPurchases, GroupingAssignment, RefundSelectable, RefundTransactions, SourceFile };
        Usages _Usage = Usages.None;

        public bool CreateNewSourceFileRow = false;

        public BindingSource BindingSrc => this.transacTableBindingSource1;

        public MainDataSet.TransacDataTable TransacTable => transacTableBindingSource1.TransacTable;

        public MainDataSetTableAdapters.TransacTableAdapter TransacAdapter => transacTableAdapter;

        public DataGridView Grid => grid1;


        public TransacEditingGridCtrl()
        {
            InitializeComponent();
        }

        public void Initialize(Usages usage)
        {
            Initialize(usage, null);
        }

        // if transacTbl param is null, it creates its own table
        public void Initialize(Usages usage, MainDataSet.TransacDataTable transacTbl)
        {
            _Usage = usage;

            if (transacTbl != null)
            {
                // DIAG _TransacTbl = transacTbl;
                transacTableBindingSource1.DataSource = transacTbl; // gotta reset this
            }

            /* old code in Designer:
            this.transacBindingSource.DataMember = "Transac";
            this.transacBindingSource.DataSource = this.mainDataSet;
            this.transacBindingSource.Sort = "";
             */

            // allow adding rows?
            if (_Usage == Usages.CashPurchases)
                grid1.AllowUserToAddRows = true;

            AmountNegatedColumn.ValueType = typeof(Decimal); // can't do this in designer

            // For AmountNegated column
            grid1.CellEndEdit += Grid1_CellEndEdit;
            this.BindingSrc.CurrentChanged += BindingSrc_CurrentChanged;
        }

        // For AmountNegated column
        public void UpdateAmountNegatedDisplayedValues()
        {
            foreach (DataGridViewRow gridRow in grid1.Rows)
            {
                if (gridRow.DataBoundItem is DataRowView)
                {
                    decimal? negatedAmount = null;
                    DataRowView dataRowView = (DataRowView)gridRow.DataBoundItem;
                    MainDataSet.TransacRow transacRow = (MainDataSet.TransacRow)dataRowView.Row;
                    Debug.WriteLine("DIAG Upd... " + gridRow.Index.ToString() + " | " + transacRow.Amount.ToString());

                    if (transacRow != null && !transacRow.IsAmountNull())
                        negatedAmount = -(decimal)transacRow.Amount;
                    // e.Value = val;

                    DataGridViewCell amountNegCell = grid1[AmountNegatedColumn.Index, gridRow.Index];
                    if ((decimal?)amountNegCell.Value != negatedAmount)
                    {
                        amountNegCell.Value = negatedAmount;
                    }

                }
            }
        }

        private void BindingSrc_CurrentChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("DIAG BindingSrc_CurrentChanged");
        // NO LONGER CALLED - it only updated the "current" (selected) grid row
            // BeginInvoke(new ParameterlessDelegate(UpdateAmountNegatedCell));           
        }

        public delegate void ParameterlessDelegate();

        // NO LONGER USED - for AmountNegated column
        void UpdateAmountNegatedCell()
        {
            decimal? negatedAmount = null;
            DataRowView dataRowView = this.BindingSrc.Current as DataRowView;
            MainDataSet.TransacRow transacRow = dataRowView.Row as MainDataSet.TransacRow;
            if (transacRow != null && !transacRow.IsAmountNull())
                negatedAmount = -(decimal)transacRow.Amount;
            // e.Value = val;

            DataGridViewCell amountNegCell = grid1[AmountNegatedColumn.Index, BindingSrc.Position];
            if ((decimal?)amountNegCell.Value != negatedAmount)
            {
                amountNegCell.Value = negatedAmount;
            }

            Debug.WriteLine("DIAG UpdateAmountNegativeCell!! " + (negatedAmount==null ? " null" : negatedAmount.ToString()) + " pos " + this.BindingSrc.Position.ToString());
        }

        // For AmountNegated column
        private void Grid1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == AmountNegatedColumn.Index)
            {
                DataGridViewCell amountNegCell = grid1[e.ColumnIndex, e.RowIndex];
                DataRowView dataRowView = this.BindingSrc.Current as DataRowView;
                MainDataSet.TransacRow transacRow = dataRowView.Row as MainDataSet.TransacRow;
                if (amountNegCell.Value is decimal)
                    transacRow.Amount = -(decimal)amountNegCell.Value;
                else
                    transacRow.SetAmountNull();

                Debug.WriteLine("DIAG Cell End Edit!! " + amountNegCell.Value.ToString());
            }
        }

        private void gridBudgetItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Utils.SetImportedDataGridRowColors(grid1);
        }

        TransacTypeTableAdapter _TransacTypeAdapter = new TransacTypeTableAdapter();
        MainDataSet.TransacTypeDataTable _TransacTypeTable = new MainDataSet.TransacTypeDataTable();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // In Design mode?
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            // Filter ctrls used?
            if (_Usage == Usages.RefundTransactions)
                panelTop.Visible = false;

            // Hide unused grid columns:
            switch (_Usage)
            {
                // DIAG make this customization easier -- or at least set it up for other usages
                case Usages.CashPurchases: 
                    this.accountDataGridViewTextBoxColumn.Visible = false;
                    this.AmountColumn.Visible = false;
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

            // comboTrType has to contain a null item:
            _TransacTypeAdapter.FillWithNullRow(_TransacTypeTable);
            comboTrType.DataSource = _TransacTypeTable;
            comboTrType.ValueMember = "TrTypeID";
            comboTrType.DisplayMember = "CodeAndName";
            comboTrType.SelectionChangeCommitted += ComboTrType_SelectionChangeCommitted;

            // DIAG only do this if it uses it
            //UpdateAmountNegatedDisplayedValues();
        }

        private void ComboTrType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateTransacFilter();
        }

        void UpdateTransacFilter()
        {
            // make filter string from more fields in the future
            // DIAG also, note that changing tbFilter has no effect... either delete that text box, or make it work with others
            if ((string)comboTrType.SelectedValue == "")
                BindingSrc.Filter = "";
            else
                BindingSrc.Filter = "TrType = '"+ (string)comboTrType.SelectedValue + "'";
        }

        public void UpdateTransacFilter(string filterText)
        {
            // DIAG make ctrls like comboTrType interact correctly
            BindingSrc.Filter = filterText;
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
                BindingSrc.Filter = tbFilter.Text;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }
    }
}
