using Budget.MainDataSetTableAdapters;
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
        public bool CreateNewSourceFileRow = false;

        public MainDataSet.BudgetDataTable BudgetTable => mainDataSet.Budget;
        public MainDataSetTableAdapters.BudgetTableAdapter BudgetAdapter => budgetTableAdapter;

        public DataGridView Grid => grid1;

        public BudgetEditingGridCtrl()
        {
            InitializeComponent();
        }

        private void gridBudgetItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int rowNum = 0; rowNum < grid1.Rows.Count; rowNum++) 
            {
                grid1.Rows[rowNum].HeaderCell.Value = (rowNum + 1).ToString();
            }
        }
    }
}
