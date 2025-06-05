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

            Debug.WriteLine("DIAG CASH form queried!");
            transacCtrl.UpdateAmountNegatedDisplayedValues();         // For AmountNegated column. S/n hafta call it from here, but... no better way found
        }

        private void btnSaveSharePrices_Click(object sender, EventArgs e)
        {
            transacCtrl.TransacAdapter.Update(transacCtrl.TransacTable);
        }
    }
}
