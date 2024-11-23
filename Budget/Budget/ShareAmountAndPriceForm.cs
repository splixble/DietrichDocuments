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
    public partial class ShareAmountAndPriceForm : Form
    {
        public ShareAmountAndPriceForm()
        {
            InitializeComponent();

            // Set up Account combo box to just show share-tracked accounts:
            DataView dvShareAccounts = new DataView(Program.LookupTableSet.MainDataSet.BudgetAccount);
            dvShareAccounts.RowFilter = "TrackedByShares = 1";
            comboAccount.DataSource = dvShareAccounts;
            comboAccount.DisplayMember = "AccountName";
            comboAccount.ValueMember = "AccountID";

            mainDataSet.SharePrice.TableNewRow += SharePrice_TableNewRow;
            mainDataSet.ShareQuantity.TableNewRow += ShareQuantity_TableNewRow;
        }

        private void ShareQuantity_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            ((MainDataSet.ShareQuantityRow)e.Row).SQAccount = comboAccount.SelectedValue as string;
        }

        private void SharePrice_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            ((MainDataSet.SharePriceRow)e.Row).SPAccount = comboAccount.SelectedValue as string;
        }

        private void ShareAmountAndPriceForm_Load(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        void RefreshDisplay()
        {
            if (comboAccount.SelectedValue == null)
            {
                mainDataSet.SharePrice.Clear();
                mainDataSet.ShareQuantity.Clear();
            }
            else
            {
                this.sharePriceTableAdapter.FillByAccount(this.mainDataSet.SharePrice, comboAccount.SelectedValue as string);
                this.shareQuantityTableAdapter.FillByAccount(this.mainDataSet.ShareQuantity, comboAccount.SelectedValue as string);
            }

        }

        private void comboAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // TODO save only if data changed, etc
            this.sharePriceTableAdapter.Update(this.mainDataSet.SharePrice);
            this.shareQuantityTableAdapter.Update(this.mainDataSet.ShareQuantity);

        }
    }
}
