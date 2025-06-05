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

        MainDataSet.TransacDataTable _TransacTbl;

        public RefundTransacsForm()
        {
            InitializeComponent();

            WinformsLib.Utils.AllowNullFields(this.mainDataSet.RefundTransac); // for newly added rows; FK will be filled in on save

            _TransacTbl = new MainDataSet.TransacDataTable();
            transCtrlSelectable.Initialize(TransacEditingGridCtrl.Usages.RefundSelectable, _TransacTbl);
            transCtrlInRefund.Initialize(TransacEditingGridCtrl.Usages.RefundTransactions, _TransacTbl);
        }

        public DialogResult ShowDialog(int refundID)
        {
            _RefundID = refundID;
            return ShowDialog();
        }

        private void RefundTransacsForm_Load(object sender, EventArgs e)
        {
            transCtrlSelectable.TransacAdapter.Fill(_TransacTbl);


            // TODO: This line of code loads data into the 'mainDataSet.RefundTransac' table. You can move, or remove it, as needed.
            this.refundTransacTableAdapter.FillByRefund(this.mainDataSet.RefundTransac, _RefundID);

            btnSave.Initialize(this.refundTransacBindingSource, this.mainDataSet.RefundTransac);

            UpdateTransacsDisplayed();
        }

        void UpdateTransacsDisplayed()
        {
            string filterText = string.Empty;
            foreach (MainDataSet.RefundTransacRow reTraRow in this.mainDataSet.RefundTransac)
            {
                if (filterText != "")
                    filterText += " OR ";
                filterText += "ID=" + reTraRow.Transac.ToString();
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
