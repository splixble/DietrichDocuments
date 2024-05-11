using Microsoft.Reporting.WinForms;
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

namespace Budget
{
    public partial class Form1 : Form
    {
        // use this?
        public MainDataSet MainData { get { return _MainData; } }
        MainDataSet _MainData = new MainDataSet();


        const int groupingsGridCheckboxColumn = 0;

        public Form1()
        {
            InitializeComponent();
        }

        void RefreshDisplay()
        {
            // Get groupings to display, from checked 
            string groupings = "";
            // 
            foreach (DataGridViewRow gridRow in gridGroupings.Rows)
            {
                if (gridRow.Cells[groupingsGridCheckboxColumn].Value is bool) // if checkbox checked
                {
                    DataRowView rowView = (DataRowView)gridRow.DataBoundItem;
                    MainDataSet.ViewBudgetGroupingsInOrderRow dataRow = (MainDataSet.ViewBudgetGroupingsInOrderRow)rowView.Row;
                    if (groupings != "")
                        groupings += ",";
                    groupings += "'" + dataRow.Grouping + "'";// DIAG gotta manually do this with a SQL stmt
                }
                // DIAG need to handle if no groupings are selected
                // DIAG capture checking the grid chaeckbox
            }

            // have to work around the no-datasource problem by manuallt allocating and filling the data table:
            /* DIAG remove:
            MainDataSetTableAdapters.ViewBudgetMonthlyReportTableAdapter adap = new MainDataSetTableAdapters.ViewBudgetMonthlyReportTableAdapter();
            //adap.FillByGroupingList(MainData.ViewBudgetMonthlyReport, "(" + groupings + ")");
            // DIAG remove FillByGroupingList; it dunn work
            adap.Fill(MainData.ViewBudgetMonthlyReport);
            // DIAG can the table be just a standalone table, apart from its DataSet obj? or use MainDataSet1?
            */

            string selectStr = "SELECT * FROM ViewBudgetMonthlyReport WHERE Grouping IN (" + groupings + ")";
            MainData.ViewBudgetMonthlyReport.Clear();
            using (SqlConnection reportDataConn = new SqlConnection(Properties.Settings.Default.SongbookConnectionString10May24))
            {
                reportDataConn.Open();
                SqlCommand reportDataCmd = new SqlCommand();
                // this dont compile: CommandBehavior fillCommandBehavior = FillCommandBehavior;
                reportDataCmd.Connection = reportDataConn;
                reportDataCmd.CommandText = selectStr;
                // DIAG do i need to bind fields?

                SqlDataAdapter reportDataAdap = new SqlDataAdapter(reportDataCmd);
                reportDataAdap.Fill(MainData.ViewBudgetMonthlyReport);
            }

                /* old code:
                
            SqlConnection reportDataConn = new SqlConnection();
            reportDataConn.ConnectionString = Properties.Settings.Default.SongbookConnectionString10May24;
            reportDataConn.Open(); // prolly not needed
            SqlCommand reportDataCmd = new SqlCommand();
            // this dont compile: CommandBehavior fillCommandBehavior = FillCommandBehavior;
            reportDataCmd.Connection = reportDataConn;
            reportDataCmd.CommandText = selectStr;
            // DIAG do i need to bind fields?

            SqlDataAdapter reportDataAdap = new SqlDataAdapter(reportDataCmd);
            reportDataAdap.Fill(MainData.ViewBudgetMonthlyReport);
                */

            ReportDataSource rds = new ReportDataSource("DataSet1", MainData.ViewBudgetMonthlyReport as DataTable);

            PopulateGrid();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        void PopulateGrid()
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
            // Check, by default, the first 2 groupings (Inc. and Exp):
            gridGroupings[groupingsGridCheckboxColumn, 0].Value = true;
            gridGroupings[groupingsGridCheckboxColumn, 1].Value = true;

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
            TrTypeForm trTypeForm = new TrTypeForm();
            trTypeForm.ShowDialog();
        }
    }
}
