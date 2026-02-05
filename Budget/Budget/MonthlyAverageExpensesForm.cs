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

            SelectorCtrl.SelectionChanged += Selector_SelectionChanged;
        }

        private void Selector_SelectionChanged(TransacListSelector sender, EventArgs args)
        {
            RefreshDisplay();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SelectorCtrl.Initialize("D", AssetType.BankAndCash, true, DateTime.Today.AddMonths(-12), DateTime.Today.AddMonths(-1));

            RefreshDisplay();
        }

        void RefreshDisplay()
        { 
            grid1.RefreshData(SelectorCtrl.FromMonth, SelectorCtrl.ToMonth, SelectorCtrl.AccountOwner, SelectorCtrl.AssetType, SelectorCtrl.AdjustForRefunds);

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
