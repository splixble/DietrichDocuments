﻿using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient; // NOTE: Must be Microsoft.Data.SqlClient, NOT System.Data.SqlClient
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace Budget
{
    public partial class Form1 : Form
    {
        // use this?
        public MainDataSet MainData { get { return _MainData; } }
        MainDataSet _MainData = new MainDataSet();

        // DIAG put a splitter window in! And show Comments in budget items,
        const int groupingsGridCheckboxColumn = 0;

        public Form1()
        {
            InitializeComponent();
        }

        void AddToGroupingListIfChecked(TreeNode node, ref string groupingsList)
        {
            if (node.Checked)
            {
                if (groupingsList != "")
                    groupingsList += ",";
                groupingsList += "'" + node.Text + "'";

                // recursively call this for child nodes:
                foreach (TreeNode childNode in node.Nodes)
                    AddToGroupingListIfChecked(childNode, ref groupingsList);
            }
        }

        void RefreshDisplay()
        {
            // Get groupings to display, from checked 
            string groupingsList = "";
            // 


            foreach (TreeNode node in tvGroupings.Nodes)
            {
                AddToGroupingListIfChecked(node, ref groupingsList);
            }
            

            /* REMO
            foreach (DataGridViewRow gridRow in gridGroupings.Rows)
            {
                object groupingIsChecked = gridRow.Cells[groupingsGridCheckboxColumn].Value;
                if (groupingIsChecked is bool && (bool)groupingIsChecked) // if checkbox checked
                {
                    DataRowView rowView = (DataRowView)gridRow.DataBoundItem;
                    MainDataSet.ViewBudgetGroupingsInOrderRow dataRow = (MainDataSet.ViewBudgetGroupingsInOrderRow)rowView.Row;
                    if (groupingsList != "")
                        groupingsList += ",";
                    groupingsList += "'" + dataRow.Grouping + "'";// DIAG gotta manually do this with a SQL stmt
                }
            }
            */

            MainData.ViewBudgetMonthlyReport.Clear();
            if (groupingsList != "") // if no groupings checked, just leave it cleared
            {
                string selectStr = "SELECT * FROM ViewBudgetMonthlyReport WHERE Grouping IN (" + groupingsList + ")";
                using (SqlConnection reportDataConn = new SqlConnection(Properties.Settings.Default.SongbookConnectionString10May24))
                {
                    // reportDataConn.Open();
                    SqlCommand reportDataCmd = new SqlCommand();
                    // this dont compile: CommandBehavior fillCommandBehavior = FillCommandBehavior;
                    reportDataCmd.Connection = reportDataConn;
                    reportDataCmd.CommandText = selectStr;

                    SqlDataAdapter reportDataAdap = new SqlDataAdapter(reportDataCmd);
                    reportDataAdap.Fill(MainData.ViewBudgetMonthlyReport);
                }
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", MainData.ViewBudgetMonthlyReport as DataTable);

            PopulateMainGrid();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        void PopulateMainGrid()
        {
            gridMain.Rows.Clear();
            gridMain.Columns.Clear();

            gridMain.RowHeadersWidth = 120;


            SortedList<DateTime, object> cols = new SortedList<DateTime, object>();
            SortedList<string, object> rows = new SortedList<string, object>();
            foreach (MainDataSet.ViewBudgetMonthlyReportRow tblRow in MainData.ViewBudgetMonthlyReport)
            {
                if (!tblRow.IsGroupingNull())
                    rows[tblRow.Grouping] = null;

                cols[tblRow.TrMonth] = null;
            }

            Dictionary<DateTime, int> colIndices = new Dictionary<DateTime, int>();
            int colIndex = 0;
            foreach (DateTime trMonth in cols.Keys)
            {
                colIndices[trMonth] = colIndex;
                gridMain.Columns.Add("col" + colIndex.ToString(), trMonth.ToString("MMM yy"));
                DataGridViewColumn col = gridMain.Columns[colIndex];
                col.Width = 70;
                col.ValueType = typeof(Decimal);
                col.Tag = trMonth;
                col.DefaultCellStyle.Format = "$0,0.00";
                colIndex++;
            }

            Dictionary<string, int> rowIndices = new Dictionary<string, int>();
            int rowIndex = 0;
            foreach (string grouping in rows.Keys)
            {
                rowIndices[grouping] = rowIndex;
                //dataGridView1.Columns.Add("row" + rowIndex.ToString(), trMonth.ToString("MMM yy"));
                rowIndex++;
            }

            gridMain.RowCount = rowIndex;
            for (int ri = 0; ri < gridMain.RowCount; ri++)
            {
                DataGridViewRow row = gridMain.Rows[ri]; 
                row.HeaderCell.Value = rows.Keys[ri];
                row.Tag = rows.Keys[ri];
            }

            foreach (MainDataSet.ViewBudgetMonthlyReportRow tblRow in MainData.ViewBudgetMonthlyReport)
            {
                if (!tblRow.IsGroupingNull())
                {
                    gridMain[colIndices[tblRow.TrMonth], rowIndices[tblRow.Grouping]].Value = tblRow.AmountNormalized;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.ViewBudgetGroupingsInOrder' table. You can move, or remove it, as needed.
            this.viewBudgetGroupingsInOrderTableAdapter.Fill(this.mainDataSet.ViewBudgetGroupingsInOrder);

            // Populate Groupings tree:
            // add parent nodes:
            foreach (MainDataSet.ViewBudgetGroupingsInOrderRow groupingRow in mainDataSet.ViewBudgetGroupingsInOrder)
            {
                if (groupingRow.IsParentGroupingLabelNull())
                {
                    TreeNode node = tvGroupings.Nodes.Add(groupingRow.Grouping, groupingRow.Grouping);
                    // Check, by default, the first 2 groupings (Inc. and Exp):   // DIAG strings s/b constants
                    if (groupingRow.Grouping == "Income" || groupingRow.Grouping == "Expenditures") 
                        node.Checked = true;
                }
            }
            // now, add child nodes:
            foreach (MainDataSet.ViewBudgetGroupingsInOrderRow groupingRow in mainDataSet.ViewBudgetGroupingsInOrder)
            {
                if (!groupingRow.IsParentGroupingLabelNull())
                {
                    TreeNode[] parentNodeArray = tvGroupings.Nodes.Find(groupingRow.ParentGroupingLabel, false); // search only top level
                    if (parentNodeArray.Length > 0) // should never be 0, or >1
                        parentNodeArray[0].Nodes.Add(groupingRow.Grouping);
                }
            }

            RefreshDisplay();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DateTime cellMonth = (DateTime)gridMain.Columns[e.ColumnIndex].Tag;
            string cellGrouping = (string)gridMain.Rows[e.RowIndex].Tag;
            MonthGroupingForm monthGroupingForm = new MonthGroupingForm();
            monthGroupingForm.Initialize(cellMonth, cellGrouping);
            monthGroupingForm.ShowDialog();

            //MessageBox.Show("Waa! " + cellMonth.ToString() + " + " + cellGrouping);
        }

        private void applyTransactionTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupingAssignmentForm trTypeForm = new GroupingAssignmentForm();
            trTypeForm.ShowDialog();
        }

        private void editGroupingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupingForm form = new GroupingForm();
            form.ShowDialog();
        }

        private void loadSourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SourceFileForm form = new SourceFileForm();
            form.ShowDialog();
        }

        private void tvGroupings_AfterCheck(object sender, TreeViewEventArgs e)
        {
            RefreshDisplay();
        }

        private void calculateAccountBalancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceCalculationForm balanceCalculationForm = new BalanceCalculationForm();
            balanceCalculationForm.ShowDialog();
        }
    }
}