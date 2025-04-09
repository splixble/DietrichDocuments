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
    public partial class CashPurchasesForm : Form
    {
        public CashPurchasesForm()
        {
            InitializeComponent();

            transacCtrl.Initialize(TransacEditingGridCtrl.Usages.CashPurchases);


            transacCtrl.TransacTable.TableNewRow += TransacTable_TableNewRow;
        }

        private void TransacTable_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            // DIAG is this the right way to do this?
            MainDataSet.TransacRow transacRow = (MainDataSet.TransacRow)e.Row;
            transacRow.BalanceIsCalculated = false;
            transacRow.AcctTransfer = false;
            transacRow.Account = Program.CashAccountID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            transacCtrl.TransacAdapter.FillByAccount(transacCtrl.TransacTable, Program.CashAccountID);
        }

        private void btnSaveSharePrices_Click(object sender, EventArgs e)
        {
            transacCtrl.TransacAdapter.Update(transacCtrl.TransacTable);
        }
    }
}
