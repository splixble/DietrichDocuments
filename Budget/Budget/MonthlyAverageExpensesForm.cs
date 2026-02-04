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

            selector.SelectionChanged += Selector_SelectionChanged;
        }

        private void Selector_SelectionChanged(TransacListSelector sender, EventArgs args)
        {
            RefreshDisplay();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RefreshDisplay();
        }

        void RefreshDisplay()
        { 
            /* DIAG remove
            DateTime fromMonth = new DateTime(2025, 7, 1);
            DateTime toMonth = new DateTime(2025, 12, 1);
            string accountOwner = "D";
            AssetType assetType = AssetType.BankAndCash;
            bool adjustForRefunds = false;
            */

            grid1.RefreshData(selector.FromMonth, selector.ToMonth, selector.AccountOwner, selector.AssetType, selector.AdjustForRefunds);

            // Display just the non-top-level groupings that there are total rows for:



            // DIAG this not working, "total rows for"!!! when working, put the selector ctrl on main form.




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
