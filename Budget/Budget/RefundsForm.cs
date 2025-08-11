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

            transacCtrl.Initialize(TransacEditingGridCtrl.Usages.Refunds);

            RefreshData();
        }

        void RefreshData()
        {
            transacCtrl.TransacAdapter.Fill(transacCtrl.TransacTable);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            refundTableAdapter.Update(this.mainDataSet.Refund);

            // DIAG Update transac table too -- after checking for any changes! And disable button! Prompt on close! 

            transacCtrl.TransacAdapter.Update(transacCtrl.TransacTable);
        }

    }
}
