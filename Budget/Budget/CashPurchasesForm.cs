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

            budgetEditingGridCtrl1.Initialize(BudgetEditingGridCtrl.Usages.CashPurchases);
        }
    }
}
