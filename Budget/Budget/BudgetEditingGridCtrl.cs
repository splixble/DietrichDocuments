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
    public partial class BudgetEditingGridCtrl : UserControl
    {
        public MainDataSet.BudgetDataTable BudgetTable => mainDataSet.Budget;
        public MainDataSetTableAdapters.BudgetTableAdapter BudgetAdapter => budgetTableAdapter;

        public BudgetEditingGridCtrl()
        {
            InitializeComponent();
        }

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            budgetTableAdapter.Update(this.mainDataSet.Budget);
            gridBudgetItems.Refresh();
        }
    }
}
