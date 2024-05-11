using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Budget
{
    public partial class GroupingAssignmentForm : Form
    {
        public GroupingAssignmentForm()
        {
            InitializeComponent();
        }

        private void TrTypeForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet1.BudgetTypePattern' table. You can move, or remove it, as needed.
            this.budgetTypePatternTableAdapter.Fill(this.mainDataSet1.BudgetTypePattern);
            LoadTables();
        }

        void LoadTables()
        {
            if (chBoxShowUntypedOnly.Checked)
                this.budgetTableAdapter.FillUntypedUnignored(this.mainDataSet.Budget);
            else
                this.budgetTableAdapter.Fill(this.mainDataSet.Budget);

            MainDataSetTableAdapters.BudgetTypePatternTableAdapter typePatternAdap = new MainDataSetTableAdapters.BudgetTypePatternTableAdapter();
            typePatternAdap.Fill(mainDataSet.BudgetTypePattern);
        }


        private void btnApplyTypes_Click(object sender, EventArgs e)
        {
            foreach (MainDataSet.BudgetRow budgetRow in mainDataSet.Budget)
            {
                foreach (MainDataSet.BudgetTypePatternRow patternRow in mainDataSet.BudgetTypePattern)
                {
                    // Does the descrioption contain a regex match for the pattern?
                    if (Regex.IsMatch(budgetRow.Descrip, patternRow.Pattern))
                    {
                        if (patternRow.ForIgnore)
                        {
                            if (!budgetRow.Ignore)
                                budgetRow.Ignore = true;
                        }
                        else
                        {
                            if (!patternRow.IsTrTypeNull() && (budgetRow.IsTrTypeNull() || budgetRow.TrType != patternRow.TrType))
                                budgetRow.TrType = patternRow.TrType;
                        }
                    }
                }
            }

            // Now, highlight all grid rows that have modified data:
            foreach (DataGridViewRow gridRow in gridBudgetItems.Rows) 
            {
                DataRow dataRow = ((DataRowView)gridRow.DataBoundItem).Row;
                if (dataRow.RowState == DataRowState.Modified) 
                {
                    gridRow.Selected = true;
                    btnSave.Enabled = true; // there is data to save
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            budgetTableAdapter.Update(this.mainDataSet.Budget);
            LoadTables();
            gridBudgetItems.Refresh();
            btnSave.Enabled = false;
    }
    }
}
