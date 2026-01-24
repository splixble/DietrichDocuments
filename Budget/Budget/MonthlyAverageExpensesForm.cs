using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Budget
{
    public partial class MonthlyAverageExpensesForm : Form
    {
        public MonthlyAverageExpensesForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DateTime fromMonth = new DateTime(2025, 7, 1);
            DateTime toMonth = new DateTime(2025, 12, 1);
            string accountOwner = "D";
            AssetType assetType = AssetType.BankAndCash;
            bool adjustForRefunds = false;
            // DIAG Balances are 0 when adjustForRefunds=true!!!! gotta fix!!


            // DIAG THIS needs to be merged into the ViewMonthlyReportDataTable code 
            /*
            MainDataSet.ViewMonthlyReportDataTable dataTbl = new MainDataSet.ViewMonthlyReportDataTable();

            MainDataSetTableAdapters.ViewMonthlyReportTableAdapter adap = new MainDataSetTableAdapters.ViewMonthlyReportTableAdapter();
            adap.FillByDateRange(dataTbl, fromMonth, toMonth, accountOwner, (assetType == AssetType.Investments ? (byte)1 : (byte)0));

            MainDataSet.ViewGroupingsDataTable groupingsTbl = new MainDataSet.ViewGroupingsDataTable();
            MainDataSetTableAdapters.ViewGroupingsTableAdapter groupingsAdap = new MainDataSetTableAdapters.ViewGroupingsTableAdapter();
            groupingsTbl.Clear();
            groupingsAdap.FillInSelectorOrder(groupingsTbl);

            */

            /* DIAG remove, not used
            List<string> groupingKeyList = new List<string>();
            groupingKeyList.Add("TGROC");
            groupingKeyList.Add("TNETFLIX");
            */



            grid1.RefreshData(fromMonth, toMonth, accountOwner, assetType, adjustForRefunds);

            // Display just the non-top-level groupings that there are total rows for:
            List<string> keysDisplayed = new List<string>();
            foreach (string groupingKey in grid1.TotalsData.TotalViewsByGrouping.Keys)
            {
                MainDataSet.ViewGroupingsRow groupingRow = grid1.TotalsData.GroupingsTbl.FindByGroupingKey(groupingKey);
                if (groupingRow != null && groupingRow.IsTopLevel == 0)
                    keysDisplayed.Add(groupingKey);
            }
            grid1.RefreshDisplay(keysDisplayed);
        }
    }
}
