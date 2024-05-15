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
            LoadBudgetTable();
            LoadGroupingPatternTable();
        }

        void LoadBudgetTable()
        {
            if (chBoxShowUntypedOnly.Checked)
                budgetEditingGridCtrl1.BudgetAdapter.FillUntypedUnignored(budgetEditingGridCtrl1.BudgetTable);
            else
                budgetEditingGridCtrl1.BudgetAdapter.Fill(budgetEditingGridCtrl1.BudgetTable);
        }

        void LoadGroupingPatternTable()
        {
            // DIAG does not fill grid! Why not???
            budgetTypePatternTableAdapter.Fill(mainDataSet1.BudgetTypePattern);
        }


        private void btnApplyTypes_Click(object sender, EventArgs e)
        {
            foreach (MainDataSet.BudgetTypePatternRow patternRow in mainDataSet1.BudgetTypePattern)
                ApplyPatternGrouping(patternRow);
            // Now, highlight all grid rows that have modified data:
            HighlightModifiedGridRows(budgetEditingGridCtrl1.Grid);
        }

        void HighlightModifiedGridRows(DataGridView grid)
        // Highlight all grid rows that have modified data
        {
            foreach (DataGridViewRow gridRow in grid.Rows)
            {
                DataRow dataRow = ((DataRowView)gridRow.DataBoundItem).Row;
                if (dataRow.RowState == DataRowState.Modified)
                {
                    gridRow.Selected = true;
                    btnSaveBudgetItems.Enabled = true; // there is data to save
                }
            }
        }

        void ApplyPatternGrouping(MainDataSet.BudgetTypePatternRow patternRow)
        {
            foreach (MainDataSet.BudgetRow budgetRow in budgetEditingGridCtrl1.BudgetTable)
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

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            budgetEditingGridCtrl1.BudgetAdapter.Update(budgetEditingGridCtrl1.BudgetTable);
            LoadBudgetTable();
            budgetEditingGridCtrl1.Grid.Refresh();
            btnSaveBudgetItems.Enabled = false; // DIAG s/b enabled any time data is edited, right?
    }

        private void gridGroupingPatterns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == ColumnApply.Index)
                {
                    // Apply the pattern grouping:
                    MainDataSet.BudgetTypePatternRow patternRow = (gridGroupingPatterns.Rows[e.RowIndex].DataBoundItem as DataRowView).Row as MainDataSet.BudgetTypePatternRow;
                    ApplyPatternGrouping(patternRow);
                    HighlightModifiedGridRows(budgetEditingGridCtrl1.Grid);
                }
            }
        }

        private void btnSaveGroupingPatterns_Click(object sender, EventArgs e)
        {
            // DIAG DOES NOT Save! Why not?? Need 
            budgetTypePatternTableAdapter.Update(mainDataSet1.BudgetTypePattern);
            //mainDataSet1.BudgetTypePattern.AcceptChanges(); // DIAG work now?
            LoadGroupingPatternTable();
            gridGroupingPatterns.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
