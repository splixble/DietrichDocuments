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
            bool adjustForRefunds = true;


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
            List<string> groupingKeyList = new List<string>();
            groupingKeyList.Add("TGROC");
            groupingKeyList.Add("TNETFLIX");

            grid1.RefreshData(groupingKeyList, fromMonth, toMonth,  accountOwner, assetType, adjustForRefunds);

        }
    }
}
