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
using System.IO;
using System.Xml.Linq;
using System.Security.Cryptography.Xml;
using PrintLib;
using ChartLib;
using TypeLib;
using static TypeLib.DBUtils;
using static TypeLib.DateTimeExtensions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Budget.Constants;
using System.Windows.Forms.DataVisualization.Charting;
using static Budget.MainDataSet;

namespace Budget
{
    public partial class Form1 : Form
    {
        const int groupingsGridCheckboxColumn = 0;

        TotalsByGroupingData TotalsData => totalsGrid.TotalsData; // used by the groupings tree ctrl and chart as well

        TreeNode _BalanceTotalNode = null;
        TreeNode _IncomeNode = null;
        TreeNode _ExpensesNode = null;

        public string AccountOwner => SelectorCtrl.AccountOwner;

        DataColumn AmountColumn
        {
            get
            {
                if (SelectorCtrl.AdjustForRefunds)
                    return TotalsData.TotalsTbl.AmountRefundAdjustedNormalizedColumn;
                else
                    return TotalsData.TotalsTbl.AmountNormalizedColumn;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        void AddToGroupingListIfChecked(TreeNode node, ref List<string> groupingsList)
        {
            if (node.Checked)
                groupingsList.Add(node.Name);

            // recursively call this for child nodes:
            foreach (TreeNode childNode in node.Nodes)
                AddToGroupingListIfChecked(childNode, ref groupingsList);
        }

        void BuildGroupingsTree(DataView dataByGroupingKey)
        {
            // Remove all nodes, of they exist:
            tvGroupings.Nodes.Clear();

            // DIAG check that there's at least one row in the ViewBudgetWithMonthly table with a Grouping value, before adding it to tree ctrl.
            // And ordering s/b done in this code, at the tree node construction code.

            // Requery the table: 
            TotalsData.LoadGroupings(); // DIAG do we need to??

            // Populate Groupings tree, and save certain nodes for future reference:

            // First, add parent nodes:
            foreach (MainDataSet.ViewGroupingsRow groupingRow in TotalsData.GroupingsTbl)
            {
                if (dataByGroupingKey.Find(groupingRow.GroupingKey) < 0)
                    continue;
                if (groupingRow.IsParentKeyNull())
                // DIAG was: if (groupingRow.IsParentGroupingLabelNull() && groupingRow.GroupingType != Constants.GroupingType.BalanceOfAccount)
                {
                    TreeNode node = tvGroupings.Nodes.Add(groupingRow.GroupingKey, groupingRow.GroupingLabel);

                    if (groupingRow.GroupingKey == Constants.GroupingKey.BalanceTotal)
                        _BalanceTotalNode = node;
                    else if (groupingRow.GroupingKey == Constants.GroupingKey.Income)
                        _IncomeNode = node;
                    else if (groupingRow.GroupingKey == Constants.GroupingKey.Expenses)
                        _ExpensesNode = node;
                }
            }

            // Now, add child nodes:
            foreach (MainDataSet.ViewGroupingsRow groupingRow in TotalsData.GroupingsTbl)
            {
                if (dataByGroupingKey.Find(groupingRow.GroupingKey) < 0)
                    continue;

                TreeNode parentNode = null;
                if (!groupingRow.IsParentKeyNull())
                {
                    TreeNode[] parentNodeArray = tvGroupings.Nodes.Find(groupingRow.ParentKey, false); // search only top level
                    if (parentNodeArray.Length > 0) // should never be 0, or >1
                        parentNode = parentNodeArray[0];
                }

                if (parentNode != null)
                    parentNode.Nodes.Add(groupingRow.GroupingKey, groupingRow.GroupingLabel);
            }

            // Check initial default groupings:
            switch (SelectorCtrl.AssetType)
            {
                case AssetType.BankAndCash:
                    _ExpensesNode.Checked = true;
                    _IncomeNode.Checked = true;
                    break;
                case AssetType.Investments:
                case AssetType.Both:
                    _BalanceTotalNode.Checked = true;
                    break;
            }
        }

        void RefreshData()
        {
            TotalsData.LoadData(SelectorCtrl.FromMonth, SelectorCtrl.ToMonth, SelectorCtrl.AccountOwner, SelectorCtrl.AssetType, SelectorCtrl.AdjustForRefunds);
            BuildGroupingsTree(TotalsData.TotalsByGroupingView);

            RefreshDisplay();
        }

        List<string> _GroupingKeysList; 

        void RefreshDisplay()
        {
            // Get groupings to display, from checked 
            _GroupingKeysList = new List<string>();
            foreach (TreeNode node in tvGroupings.Nodes)
                AddToGroupingListIfChecked(node, ref _GroupingKeysList);

            totalsGrid.RefreshDisplay(_GroupingKeysList);

            DrawChart();
        }

        void DrawChart()
        {
            // Clear previous things:
            ClearChart();

            Axis axisX = chart1.ChartAreas[0].AxisX;
            Axis axisY = chart1.ChartAreas[0].AxisY;

            // Y axis (dollar amount) chart settings:
            decimal minAmount = Decimal.MaxValue;
            decimal maxAmount = Decimal.MinValue;
            foreach (string grouping in _GroupingKeysList)
            {
                foreach (DataRowView rowView in TotalsData.TotalViewsByGrouping[grouping])
                {
                    MainDataSet.ViewMonthlyReportRow tblRow = rowView.Row as MainDataSet.ViewMonthlyReportRow;
                    decimal amount = (decimal)tblRow[AmountColumn];
                    if (amount > maxAmount)
                        maxAmount = amount;
                    if (amount < minAmount)
                        minAmount = amount;
                }
            }

            double yMax = Convert.ToDouble(maxAmount);
            double yMin = Convert.ToDouble(minAmount);
            double yInterval;
            DateTimeIntervalType yIntervalType;
            ChartUtils.GetDefaultIntervalInfo(12, ref yMin, ref yMax, out yInterval, out yIntervalType);
            axisY.Minimum = yMin;
            axisY.Maximum = yMax;

            // this apparently affects what numbers are shown on left side:
            axisY.Interval = yInterval;
            axisY.IntervalType = yIntervalType;
            axisY.LabelStyle.Format = "{0:C}"; // show as currency
            axisY.MajorGrid.LineColor = Color.LightGray;

            // X axis (month) chart settings:
            DateTime minDate = SelectorCtrl.FromMonth;
            DateTime maxDate = SelectorCtrl.ToMonth;
            double xInterval;
            DateTimeIntervalType xIntervalType;
            DateGraphInterval dateInterval;
            ChartUtils.GetDefaultIntervalInfo(12, ref minDate, ref maxDate, out xInterval, out xIntervalType, out dateInterval);
            axisX.Minimum = minDate.ToOADate();
            axisX.Maximum = maxDate.ToOADate();
            axisX.Interval = xInterval;
            axisX.IntervalType = xIntervalType;
            axisX.LabelStyle.Format = "MMM yy";
            axisX.MajorGrid.LineColor = Color.LightGray;

            ChartLineColorGenerator colorGenerator = new ChartLineColorGenerator();

            foreach (string groupingKey in _GroupingKeysList)
            {
                MainDataSet.ViewGroupingsRow groupingRow = TotalsData.GroupingsTbl.FindByGroupingKey(groupingKey);
                DataView view = TotalsData.TotalViewsByGrouping[groupingKey];
                Series series = new Series(groupingRow.GroupingLabel);
                series.ChartType = SeriesChartType.Line;
                series.Color = GetChartLineColor(groupingKey, colorGenerator);
                series.BorderWidth = 1; // line width
                                        // series.BorderDashStyle = ChartDashStyle.Dot; // if not solid line
                                        // (Color)Enum.Parse(typeof(Color), ); // 

                //
                series.Points.DataBindXY(view, "TrMonth", view, AmountColumn.ColumnName);
                chart1.Series.Add(series);
            }
        }

        Color GetChartLineColor(string groupingKey, ChartLineColorGenerator colorGenerator)
        {
            // return Color.FromName("ForestGreen");  // Color.ForestGreen; // line color. DIAG set from DB

            if (groupingKey == Constants.GroupingKey.Income)
                return Color.Black;
            else if (groupingKey == Constants.GroupingKey.Expenses)
                return Color.Red;
            else if (groupingKey == Constants.GroupingKey.BalanceTotal)
                return Color.Blue;
            else
                return colorGenerator.GetNextColor();
        }

        protected virtual void ClearChart()
        {
            // Clear previous things:
            chart1.Series.Clear();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Program.LookupTableSet.LoadWithRetryOption()) // load tables before form is loaded
                return;

            base.OnLoad(e);

            SelectorCtrl.Initialize("D", AssetType.BankAndCash, true, DateTime.Today.AddMonths(-15), DateTime.Today.AddMonths(-1), false);

            // Make totalsGrid printable by taking a Printable tag onto it:
            totalsGrid.Tag = new PrintableGridTag(totalsGrid);

            RefreshData();
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

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DNPrintDocument printDoc = new DNPrintDocument();
            printDoc.PrintPreview(splitConInner.Panel1); // contains gridMain. Or can I just pass gridMain in?
        }

        private void investmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvestmentBalancesForm form = new InvestmentBalancesForm();
            form.ShowDialog();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountsForm form = new AccountsForm();
            form.ShowDialog();
        }

        private void cashPurchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashPurchasesForm form = new CashPurchasesForm();
            form.ShowDialog();
        }

        private void refundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefundsForm form = new RefundsForm();
            form.ShowDialog();
        }

        private void monthlyAverageExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlyAverageExpensesForm form = new MonthlyAverageExpensesForm();
            form.ShowDialog();
        }

        private void SelectorCtrl_SelectionChanged(TransacListSelector sender, EventArgs args)
        {
            RefreshData();
        }
    }
}
