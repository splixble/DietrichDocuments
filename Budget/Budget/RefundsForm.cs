using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GridLib;

namespace Budget
{
    public partial class RefundsForm : Form
    {
        public RefundsForm()
        {
            InitializeComponent();
        }

        private void RefundsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.Refund' table. You can move, or remove it, as needed.
            this.refundTableAdapter.Fill(this.mainDataSet.Refund);
            transacCtrl.Initialize(TransacEditingGridCtrl.Usages.Refunds);

            btnSaveRefundSets.Initialize(this.refundBindingSource, this.mainDataSet.Refund);
            btnSaveTransactions.Initialize(transacCtrl.BindingSrc, transacCtrl.TransacTable);

            RefreshData();
        }

        void RefreshData()
        {
            transacCtrl.TransacAdapter.Fill(transacCtrl.TransacTable);
        }

        private void btnSaveTransactions_Click(object sender, EventArgs e)
        {
            transacCtrl.TransacAdapter.Update(transacCtrl.TransacTable);
        }

        private void saveRefundSets_Click(object sender, EventArgs e)
        {
            refundTableAdapter.Update(this.mainDataSet.Refund);

            // reread table at app level:
            Program.LookupTableSet.LoadRefundTable();

            // DIAG Prompt on close - does it? 
        }

        private void grid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == ColApplyToSelected.Index)
            {
                DataRowView refundRowView = refundBindingSource[e.RowIndex] as DataRowView;
                int refundID = ((MainDataSet.RefundRow)refundRowView.Row).RefundID;

                SortedList<int, object> gridRowDict = transacCtrl.Grid.RowsContainingSelectedData();
                if (gridRowDict == null)
                    return;

                // Gather Transac table rows: 
                List<MainDataSet.TransacRow> transacRows = new List<MainDataSet.TransacRow>();
                foreach (int rowIndex in gridRowDict.Keys)
                {
                    DataGridViewRow gridRow = transacCtrl.Grid.Rows[rowIndex];
                    transacRows.Add((gridRow.DataBoundItem as DataRowView).Row as MainDataSet.TransacRow);
                }

                // Set RefundID in each Transac table record:
                foreach (MainDataSet.TransacRow transacRow in transacRows)
                {
                    if (transacRow.IsRefundIDNull() || transacRow.RefundID != refundID)
                        transacRow.RefundID = refundID;
                }

                // Now, highlight all grid rows that have modified data:
                Utils.SetImportedDataGridRowColors(transacCtrl.Grid);
            }
        }
    }
}
