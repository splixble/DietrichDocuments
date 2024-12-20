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
    public partial class AccountsForm : Form
    {
        public AccountsForm()
        {
            InitializeComponent();
        }

        private void AccountsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.Account' table. You can move, or remove it, as needed.
            this.budgetAccountTableAdapter.Fill(this.mainDataSet.Account);

            AccountOwnerColumn.DataSource = Program.LookupTableSet.MainDataSet.AccountOwner;
            AccountOwnerColumn.ValueMember = "OwnerID";
            AccountOwnerColumn.DisplayMember = "OwnerDescription";

            AccountTypeColumn.DataSource = Program.LookupTableSet.MainDataSet.AccountType;
            AccountTypeColumn.ValueMember = "TypeCode";
            AccountTypeColumn.DisplayMember = "TypeDescription";

            FundColumn.DataSource = Program.LookupTableSet.MainDataSet.Fund;
            FundColumn.ValueMember = "FundID";
            FundColumn.DisplayMember = "FundName";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            budgetAccountTableAdapter.Update(this.mainDataSet.Account);
        }
    }
}
