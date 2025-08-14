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
using GridLib;

namespace Budget
{
    /* DIAG do: make views put in 0 values for intervening months with no charges. By a generated SQL function
     */
    public partial class GroupingAssignmentForm : Form
    {
        public GroupingAssignmentForm()
        {
            InitializeComponent();
            transacCtrl.Initialize(TransacEditingGridCtrl.Usages.GroupingAssignment);
        }

        private void TrTypeForm_Load(object sender, EventArgs e)
        {
            // In Design mode?
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            TrTypeComboColumn.DataSource = Program.LookupTableSet.MainDataSet.TransacType;
            TrTypeComboColumn.ValueMember = "TrTypeID";
            TrTypeComboColumn.DisplayMember = "CodeAndName";

            comboTrType.DataSource = Program.LookupTableSet.MainDataSet.TransacType;
            comboTrType.ValueMember = "TrTypeID";
            comboTrType.DisplayMember = "CodeAndName";           

            LoadTransacTable();
            LoadGroupingPatternTable();
        }

        void LoadTransacTable()
        {
            if (chBoxShowUntypedOnly.Checked)
                transacCtrl.TransacAdapter.FillUntypedNotAcctTransfer(transacCtrl.TransacTable);
            else
                transacCtrl.TransacAdapter.Fill(transacCtrl.TransacTable);
            // TODO just use the binding ctrl's filter, rather than rereading table, right?
        }

        void LoadGroupingPatternTable()
        {
            transacTypePatternTableAdapter.Fill(mainDataSet1.TransacTypePattern);
        }


        private void btnApplyTypes_Click(object sender, EventArgs e)
        {
            foreach (MainDataSet.TransacTypePatternRow patternRow in mainDataSet1.TransacTypePattern)
                ApplyPatternGrouping(patternRow);
        }

        void ApplyPatternGrouping(MainDataSet.TransacTypePatternRow patternRow)
        {
            ApplyPatternGrouping(patternRow.Pattern, patternRow.IsTrTypeNull() ? null : patternRow.TrType, patternRow.ForAcctTransfer);
        }

        void ApplyPatternGrouping(string pattern, string trType, bool forAcctTransfer)
        {
            foreach (MainDataSet.TransacRow budgetRow in transacCtrl.TransacTable)
            {
                // Does the descrioption contain a regex match for the pattern?
                if (Regex.IsMatch(budgetRow.Descrip, pattern))
                {
                    if (forAcctTransfer)
                    {
                        if (!budgetRow.AcctTransfer)
                            budgetRow.AcctTransfer = true;
                    }
                    else
                    {
                        if (trType != null && (budgetRow.IsTrTypeNull() || budgetRow.TrType != trType))
                            budgetRow.TrType = trType;
                    }
                }
            }
            // Now, highlight all grid rows that have modified data:
            Utils.SetImportedDataGridRowColors(transacCtrl.Grid);
        }

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            transacCtrl.TransacAdapter.Update(transacCtrl.TransacTable);
            LoadTransacTable();
            transacCtrl.Grid.Refresh();
    }

        private void gridGroupingPatterns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == ColumnApply.Index)
                {
                    // Apply the pattern grouping:
                    MainDataSet.TransacTypePatternRow patternRow = (gridGroupingPatterns.Rows[e.RowIndex].DataBoundItem as DataRowView).Row as MainDataSet.TransacTypePatternRow;
                    ApplyPatternGrouping(patternRow);
                }
            }
        }

        private void btnSaveGroupingPatterns_Click(object sender, EventArgs e)
        {
            transacTypePatternTableAdapter.Update(mainDataSet1.TransacTypePattern);
            LoadGroupingPatternTable();
            gridGroupingPatterns.Refresh();
        }

        private void chBoxShowUntypedOnly_CheckStateChanged(object sender, EventArgs e)
        {
            LoadTransacTable();
        }

        private void btnApplyOneTime_Click(object sender, EventArgs e)
        {
            ApplyPatternGrouping(tbPattern.Text, comboTrType.SelectedValue as string, chBoxAcctTransfer.Checked);
        }

        private void btnApplyToSelected_Click(object sender, EventArgs e)
        {
            string trType = comboTrType.SelectedValue as string;
            if (trType == null)
                return;

            SortedList<int, object> gridRowDict = transacCtrl.Grid.RowsContainingSelectedData();
            if (gridRowDict == null)
                return;

            // Gather Transac table rows: 
            List<MainDataSet.TransacRow> transacRows = new List<MainDataSet.TransacRow>();
            foreach (int rowIndex in gridRowDict.Keys)
            {
                DataGridViewRow gridRow = transacCtrl.Grid.Rows[rowIndex];
                transacRows.Add((gridRow.DataBoundItem as DataRowView).Row as MainDataSet.TransacRow);
            }

            // Set TrType in each Transac table record:
            foreach (MainDataSet.TransacRow transacRow in transacRows)
            { 
                if (transacRow.IsTrTypeNull() || transacRow.TrType != trType)
                    transacRow.TrType = trType;
            }

            // Now, highlight all grid rows that have modified data:
            Utils.SetImportedDataGridRowColors(transacCtrl.Grid);
        }
    }
}
