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
            adap.Fill(MainData.ViewBudgetMonthlyReport); // can the table be justa  standalone table, apart from its DataSet obj?
            ReportDataSource rds = new ReportDataSource("DataSet1", MainData.ViewBudgetMonthlyReport as DataTable);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
