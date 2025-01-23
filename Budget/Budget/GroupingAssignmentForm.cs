using GridLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Budget
{
    /* DIAG do: make views put in 0 values for intervening months with no charges. By a generated SQL function
     */
    public partial class GroupingAssignmentForm : Form
    {
        public GroupingAssignmentForm()
        {
            InitializeComponent();
        }

        private void TrTypeForm_Load(object sender, EventArgs e)
        {
            // In Design mode?
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            TrTypeComboColumn.DataSource = Program.LookupTableSet.MainDataSet.BudgetTypeGroupings;
            TrTypeComboColumn.ValueMember = "TrTypeID";
            TrTypeComboColumn.DisplayMember = "CodeAndName";

            comboTrType.DataSource = Program.LookupTableSet.MainDataSet.BudgetTypeGroupings;
            comboTrType.ValueMember = "TrTypeID";
            comboTrType.DisplayMember = "CodeAndName";           

            LoadBudgetTable();
            LoadGroupingPatternTable();
        }

        void LoadBudgetTable()
        {
            if (chBoxShowUntypedOnly.Checked)
                budgetEditingGridCtrl1.BudgetAdapter.FillUntypedUnignored(budgetEditingGridCtrl1.BudgetTable);
            else
                budgetEditingGridCtrl1.BudgetAdapter.Fill(budgetEditingGridCtrl1.BudgetTable);
            // TODO just use the binding ctrl's filter, rather than rereading table, right?
        }

        void LoadGroupingPatternTable()
        {
            budgetTypePatternTableAdapter.Fill(mainDataSet1.BudgetTypePattern);
        }


        private void btnApplyTypes_Click(object sender, EventArgs e)
        {
            foreach (MainDataSet.BudgetTypePatternRow patternRow in mainDataSet1.BudgetTypePattern)
                ApplyPatternGrouping(patternRow);
        }

        void ApplyPatternGrouping(MainDataSet.BudgetTypePatternRow patternRow)
        {
            ApplyPatternGrouping(patternRow.Pattern, patternRow.IsTrTypeNull() ? null : patternRow.TrType, patternRow.ForIncome, patternRow.ForIgnore);
        }

        void ApplyPatternGrouping(string pattern, string trType, bool forIncome, bool forIgnore)
        {
            foreach (MainDataSet.TransacRow budgetRow in budgetEditingGridCtrl1.BudgetTable)
            {
                // Does the descrioption contain a regex match for the pattern?
                if (Regex.IsMatch(budgetRow.Descrip, pattern))
                {
                    if (forIgnore)
                    {
                        if (!budgetRow.Ignore)
                            budgetRow.Ignore = true;
                    }
                    else
                    {
                        if (trType != null && (budgetRow.IsTrTypeNull() || budgetRow.TrType != trType))
                            budgetRow.TrType = trType;
                    }
                }
            }
            // Now, highlight all grid rows that have modified data:
            Utils.SetImportedDataGridRowColors(budgetEditingGridCtrl1.Grid);
        }

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            budgetEditingGridCtrl1.BudgetAdapter.Update(budgetEditingGridCtrl1.BudgetTable);
            LoadBudgetTable();
            budgetEditingGridCtrl1.Grid.Refresh();
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
                }
            }
        }

        private void btnSaveGroupingPatterns_Click(object sender, EventArgs e)
        {
            budgetTypePatternTableAdapter.Update(mainDataSet1.BudgetTypePattern);
            LoadGroupingPatternTable();
            gridGroupingPatterns.Refresh();
        }

        private void chBoxShowUntypedOnly_CheckStateChanged(object sender, EventArgs e)
        {
            LoadBudgetTable();
        }

        private void btnApplyOneTime_Click(object sender, EventArgs e)
        {
            ApplyPatternGrouping(tbPattern.Text, comboTrType.SelectedValue as string, chBoxIncome.Checked, chBoxIgnore.Checked);
        }

        private void btnApplyToSelected_Click(object sender, EventArgs e)
        {
            SortedList<int, object> gridRowDict = budgetEditingGridCtrl1.Grid.RowsContainingSelectedData();
            if (gridRowDict == null)
                return;

            string trType = comboTrType.SelectedValue as string;
            if (trType == null)
                return;

            // Gather Budget table rows: 
            List<MainDataSet.TransacRow> budgetRows = new List<MainDataSet.TransacRow>();
            foreach (int rowIndex in gridRowDict.Keys)
            {
                DataGridViewRow gridRow = budgetEditingGridCtrl1.Grid.Rows[rowIndex];
                budgetRows.Add((gridRow.DataBoundItem as DataRowView).Row as MainDataSet.TransacRow);
            }

            // Set TrType in each Budget table record:
            foreach (MainDataSet.TransacRow budgetRow in budgetRows)
            { 
                if (budgetRow.IsTrTypeNull() || budgetRow.TrType != trType)
                    budgetRow.TrType = trType;
            }

            // Now, highlight all grid rows that have modified data:
            Utils.SetImportedDataGridRowColors(budgetEditingGridCtrl1.Grid);
        }
    }
}
