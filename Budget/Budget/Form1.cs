using Microsoft.Reporting.WinForms;
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
    public partial class Form1 : Form
    {
        // use this?
        public MainDataSet MainData { get { return _MainData; } }
        MainDataSet _MainData = new MainDataSet();

        public Form1()
        {
            InitializeComponent();

            // have to work around the no-datasource problem by manuallt allocating and filling the data table:
            MainDataSetTableAdapters.ViewBudgetMonthlyReportTableAdapter adap = new MainDataSetTableAdapters.ViewBudgetMonthlyReportTableAdapter();
            adap.Fill(MainData.ViewBudgetMonthlyReport); // DIAG can the table be just a standalone table, apart from its DataSet obj? or use MainDataSet1?
            adap.Fill(MainDataSet1.ViewBudgetMonthlyReport);
            ReportDataSource rds = new ReportDataSource("DataSet1", MainData.ViewBudgetMonthlyReport as DataTable);

            PopulateGrid();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

        }

        void PopulateGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.RowHeadersWidth = 120;


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
                dataGridView1.Columns.Add("col" + colIndex.ToString(), trMonth.ToString("MMM yy"));
                DataGridViewColumn col = dataGridView1.Columns[colIndex];
                col.Width = 70;
                col.ValueType = typeof(Decimal);
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

            dataGridView1.RowCount = rowIndex;
            for (int ri = 0; ri < dataGridView1.RowCount; ri++)
                dataGridView1.Rows[ri].HeaderCell.Value = rows.Keys[ri];

            foreach (MainDataSet.ViewBudgetMonthlyReportRow tblRow in MainData.ViewBudgetMonthlyReport)
            {
                if (!tblRow.IsGroupingNull())
                {
                    dataGridView1[colIndices[tblRow.TrMonth], rowIndices[tblRow.Grouping]].Value = tblRow.AmountNormalized;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'MainDataSet1.ViewBudgetMonthlyReport' table. You can move, or remove it, as needed.
            this.viewBudgetMonthlyReportTableAdapter.Fill(this.MainDataSet1.ViewBudgetMonthlyReport);

            this.reportViewer1.RefreshReport();
        }
    }
}
