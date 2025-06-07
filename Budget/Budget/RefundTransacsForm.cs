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
    public partial class RefundTransacsForm : Form
    {
        int _RefundID;


        public RefundTransacsForm()
        {
            InitializeComponent();

            WinformsLib.Utils.AllowNullFields(this.mainDataSet.RefundTransac); // for newly added rows; FK will be filled in on save

            transCtrlSelectable.Initialize(TransacEditingGridCtrl.Usages.RefundSelectable);
            transCtrlSelectable.TransacAdapter.Fill(transCtrlSelectable.TransacTable);

            // The 2nd TransacEditingGridCtrl shows the same data as the 1st, but we can't pass the same table to Initialize -- that will make
            // the BindingSource.Filter apply to both ctrls, since Filter, stupidly, points to the DataTable's DefaultView rather than using one of its own.
            MainDataSet.TransacDataTable transacTblCopy = transCtrlSelectable.TransacTable.Copy() as MainDataSet.TransacDataTable;
            transCtrlInRefund.Initialize(TransacEditingGridCtrl.Usages.RefundTransactions, transacTblCopy);
        }

        public DialogResult ShowDialog(int refundID)
        {
            _RefundID = refundID;
            return ShowDialog();
        }

        private void RefundTransacsForm_Load(object sender, EventArgs e)
        {
            btnSave.Initialize(this.refundTransacBindingSource, this.mainDataSet.RefundTransac);

            this.refundTransacTableAdapter.FillByRefund(this.mainDataSet.RefundTransac, _RefundID);

            UpdateTransacsDisplayed();
        }

        void UpdateTransacsDisplayed()
        {
            string filterText = string.Empty;
            if (mainDataSet.RefundTransac.Rows.Count == 0)
                filterText = "1 = 0"; // no rows get thru filter
            else
            {
                foreach (MainDataSet.RefundTransacRow reTraRow in mainDataSet.RefundTransac)
                {
                    if (filterText != "")
                        filterText += " OR ";
                    filterText += "ID=" + reTraRow.Transac.ToString();
                }
            }

            transCtrlInRefund.UpdateTransacFilter(filterText);

            // DIAG do the reverse for Selectable ctrl?
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private void UpdateDB()
        {
            refundTransacBindingSource.EndEdit();

            // Set Refund on any new RefundTransac records: 
            foreach (MainDataSet.RefundTransacRow row in mainDataSet.RefundTransac)
            {
                if (row[mainDataSet.RefundTransac.RefundColumn] == DBNull.Value)
                    row.Refund = _RefundID;
            }

            this.refundTransacTableAdapter.Update(mainDataSet.RefundTransac);
        }
    }
}
