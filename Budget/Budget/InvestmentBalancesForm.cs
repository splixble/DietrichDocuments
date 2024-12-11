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
    public partial class InvestmentBalancesForm : Form
    {
        public InvestmentBalancesForm()
        {
            InitializeComponent();

            // Set up Account combo boxes:
            DataView dvAccountsByShare = new DataView(Program.LookupTableSet.MainDataSet.BudgetAccount);
            dvAccountsByShare.RowFilter = "TrackedByShares = 1 AND AccountType='" + Constants.AccountType.Investment + "'";
            comboAccountByShares.DataSource = dvAccountsByShare;
            comboAccountByShares.DisplayMember = "AccountName";
            comboAccountByShares.ValueMember = "AccountID";

            DataView dvAccountsByDollar = new DataView(Program.LookupTableSet.MainDataSet.BudgetAccount);
            dvAccountsByDollar.RowFilter = "TrackedByShares = 0 AND AccountType='" + Constants.AccountType.Investment + "'";
            comboAccountByDollars.DataSource = dvAccountsByDollar;
            comboAccountByDollars.DisplayMember = "AccountName";
            comboAccountByDollars.ValueMember = "AccountID";

            // TODO also, currentlyUsed?

            mainDataSet.SharePrice.TableNewRow += SharePrice_TableNewRow;
            mainDataSet.ShareQuantity.TableNewRow += ShareQuantity_TableNewRow;
            mainDataSet.Budget.TableNewRow += Budget_TableNewRow;
        }

        string SelectedShareAccount => comboAccountByShares.SelectedValue as string;
        string SelectedDollarAccount => comboAccountByDollars.SelectedValue as string;

        string SelectedShareFund
        {
            get
            {
                if (SelectedShareAccount != null)
                    return Program.LookupTableSet.MainDataSet.BudgetAccount.FindByAccountID(SelectedShareAccount).Fund;
                else
                    return null;
            }
        }

        private void ShareQuantity_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            ((MainDataSet.ShareQuantityRow)e.Row).SQAccount = SelectedShareAccount;
        }

        private void SharePrice_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            ((MainDataSet.SharePriceRow)e.Row).Fund = SelectedShareFund;
        }

        private void Budget_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            MainDataSet.BudgetRow budgetRow = (MainDataSet.BudgetRow)e.Row;
            budgetRow.Account = SelectedDollarAccount;
            budgetRow.TrType = Constants.GroupingName.FundBalanceChange;
            budgetRow.Ignore = false;
            budgetRow.BalanceIsCalculated = false;
        }

        private void ShareAmountAndPriceForm_Load(object sender, EventArgs e)
        {
            RefreshDisplayByShares();
            RefreshDisplayByDollars();
        }

        void RefreshDisplayByShares()
        {
            if (SelectedShareAccount == null)
            {
                mainDataSet.SharePrice.Clear();
                mainDataSet.ShareQuantity.Clear();
            }
            else
            {
                this.sharePriceTableAdapter.FillByFund(this.mainDataSet.SharePrice, SelectedShareFund);
                this.shareQuantityTableAdapter.FillByAccount(this.mainDataSet.ShareQuantity, SelectedShareAccount);
            }
        }

        void RefreshDisplayByDollars()
        {
            if (SelectedDollarAccount == null)
                mainDataSet.Budget.Clear();
            else
                this.budgetTableAdapter.FillByAccount(this.mainDataSet.Budget, SelectedDollarAccount);
        }

        private void comboAccountByShares_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshDisplayByShares();
        }

        private void btnSaveSharePrices_Click(object sender, EventArgs e)
        {
            // TODO save only if data changed, etc
            this.sharePriceTableAdapter.Update(this.mainDataSet.SharePrice);

        }

        private void btnSaveShareAmounts_Click(object sender, EventArgs e)
        {
            this.shareQuantityTableAdapter.Update(this.mainDataSet.ShareQuantity);
        }

        private void comboAccountByDollars_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshDisplayByDollars();
        }

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            // TODO save only if data changed, etc
            budgetTableAdapter.Update(this.mainDataSet.Budget);
        }
    }
}
