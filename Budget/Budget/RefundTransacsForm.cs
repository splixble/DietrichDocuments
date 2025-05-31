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
        }

        public DialogResult ShowDialog(int refundID)
        {
            _RefundID = refundID;
            return ShowDialog();
        }

        private void RefundTransacsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.RefundTransac' table. You can move, or remove it, as needed.
            this.refundTransacTableAdapter.FillByRefund(this.mainDataSet.RefundTransac, _RefundID);

            btnSave.Initialize(this.refundTransacBindingSource, this.mainDataSet.RefundTransac);
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
