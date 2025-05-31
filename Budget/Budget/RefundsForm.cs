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

            btnSave.Initialize(this.refundBindingSource, this.mainDataSet.Refund);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            refundTableAdapter.Update(this.mainDataSet.Refund);
        }

        private void grid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            MainDataSet.RefundRow refundRow = (grid1.Rows[e.RowIndex].DataBoundItem as DataRowView).Row as MainDataSet.RefundRow;
            RefundTransacsForm form = new RefundTransacsForm();
            form.ShowDialog(refundRow.RefundID);
        }
    }
}
