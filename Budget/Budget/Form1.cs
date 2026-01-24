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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Budget.Constants;
using System.Windows.Forms.DataVisualization.Charting;
using static Budget.MainDataSet;

namespace Budget
{
    public partial class Form1 : Form
    {
        /* replaced by TotalsData
        // use this?
        public MainDataSet MainData { get { return _MainData; } }
        MainDataSet _MainData = new MainDataSet();

        MainDataSet.ViewGroupingsDataTable _GroupingsTbl = new MainDataSet.ViewGroupingsDataTable();
        MainDataSetTableAdapters.ViewGroupingsTableAdapter _GroupingsAdap = new MainDataSetTableAdapters.ViewGroupingsTableAdapter();
        */

        const int groupingsGridCheckboxColumn = 0;

        TotalsByGroupingData TotalsData => totalsGrid.TotalsData; // used by the groupings tree ctrl and chart as well

        public string AccountOwner => comboAccountOwner.SelectedValue as string;

        public AssetType AssetType => (AssetType)comboAccountType.SelectedValue;

        public DateTime FromMonth => (DateTime)comboFromMonth.SelectedMonth;

        public DateTime ToMonth => (DateTime)comboToMonth.SelectedMonth;

        TreeNode _BalanceTotalNode = null;
        TreeNode _IncomeNode = null;
        TreeNode _ExpensesNode = null;

        DataColumn AmountColumn
        {
            get
            {
                if (chBoxRefunds.Checked)
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




            // DIAG redo this to use new TotalsByGroupData class






            // Remove all nodes, of they exist:
            tvGroupings.Nodes.Clear();

            // DIAG check that there's at least one row in the ViewBudgetWithMonthly table with a Grouping value, before adding it to tree ctrl.
            // And ordering s/b done in this code, at the tree node construction code.

            // Requery the table: 
            TotalsData.LoadGroupings(); // DIAG do we need to??
            /*
            _GroupingsTbl.Clear();
            _GroupingsAdap.FillInSelectorOrder(_GroupingsTbl);
            */

            // DIAG filter by AccountOwner, AccountType
            /* with:
                    " AND AccountOwner = '" + AccountOwner + "'";
                    // "WHERE Grouping IN (" + groupingsListString + ")"
                if (AccountType != Constants.AccountType.BothValue)
                    selectStr += " AND AccountType = '" + AccountType + "'";
             */

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
                /* REMOVE -- s/n need to make this s special case
                else if (groupingRow.GroupingType == Constants.GroupingType.BalanceOfAccount)
                    parentNode = _BalanceTotalNode;
                */

                if (parentNode != null)
                    parentNode.Nodes.Add(groupingRow.GroupingKey, groupingRow.GroupingLabel);
            }

            // Check initial default groupings:
            switch (AssetType)
            {
                case AssetType.BankAndCash:
                    _ExpensesNode.Checked = true;
                    _IncomeNode.Checked = true;
                    break;
                case AssetType.Investments:
                    _BalanceTotalNode.Checked = true;
                    break;
            }
        }

        void RefreshData()
        {
            /* ############# DIAG MOVED TO TotalsByGroupingData ##############
            if (AssetType == AssetType.Both)
            {

                // in this case we need to do a special query to combine total balances of both account types:
                string selectStr = "SELECT SUM(AmountNormalized) AS AmountNormalized, " +
                    "SUM(AmountRefundAdjustedNormalized) AS AmountRefundAdjustedNormalized, " +
                    "MAX(GroupingKey) AS GroupingKey, " +
                    "MAX(TrMonth) AS TrMonth, " +
                    "MAX(AccountOwner) AS AccountOwner, " +
                    "'' AS AccountType " +
                    "FROM ViewMonthlyReport " +
                    "WHERE AccountOwner = '" + AccountOwner + "'" +
                    "AND TrMonth >= " + FromMonth.SQLDateLiteral() +
                    "AND TrMonth <= " + ToMonth.SQLDateLiteral() +
                    "GROUP BY GroupingKey, TrMonth, AccountOwner";

                using (SqlConnection reportDataConn = new SqlConnection(Properties.Settings.Default.BudgetConnectionString))
                {
                    // reportDataConn.Open();
                    SqlCommand reportDataCmd = new SqlCommand();
                    // this dont compile: CommandBehavior fillCommandBehavior = FillCommandBehavior;
                    reportDataCmd.Connection = Program.DbConnection;
                    reportDataCmd.CommandText = selectStr;

                    SqlDataAdapter reportDataAdap = new SqlDataAdapter(reportDataCmd);
                    MainData.ViewMonthlyReport.Clear();
                    reportDataAdap.Fill(MainData.ViewMonthlyReport);

                }
            }
            else
            {
                MainDataSetTableAdapters.ViewMonthlyReportTableAdapter adap = new MainDataSetTableAdapters.ViewMonthlyReportTableAdapter();
                adap.FillByDateRange(MainData.ViewMonthlyReport, FromMonth, ToMonth, AccountOwner, (AssetType == AssetType.Investments ? (byte)1 : (byte)0));
            }
       



            // Fill in missing months gaps in data for each Grouping, AccountOwner, and IsInv with 0-amount rows,
            // to avoid discontinuous lines on the chart:
            // (outer Key is array of Grouping, AccountOwner, and AccountType; inner Value is not used)
            SortedList<GroupingAccOwnerIsInvestment, SortedList<DateTime, object>> rowsByKeysAndMonth = new SortedList<GroupingAccOwnerIsInvestment, SortedList<DateTime, object>>();
            foreach (ViewMonthlyReportRow reportRow in MainData.ViewMonthlyReport)
            {
                GroupingAccOwnerIsInvestment outerKey = new GroupingAccOwnerIsInvestment(reportRow.GroupingKey, reportRow.AccountOwner, reportRow.IsInvestment);
                if (!rowsByKeysAndMonth.ContainsKey(outerKey))
                    rowsByKeysAndMonth.Add(outerKey, new SortedList<DateTime, object>());
                rowsByKeysAndMonth[outerKey][reportRow.TrMonth] = null;
            }
            foreach (GroupingAccOwnerIsInvestment keyList in rowsByKeysAndMonth.Keys)
            {
                // go through each month in specified range, and if there's not a row for it, add one with 0 amount:
                SortedList<DateTime, object> subList = rowsByKeysAndMonth[keyList];
                for (DateTime month = FromMonth; month <= ToMonth; month = month.AddMonths(1))
                {
                    if (!subList.ContainsKey(month))
                    {
                        MainDataSet.ViewMonthlyReportRow newRow = MainData.ViewMonthlyReport.NewViewMonthlyReportRow();
                        newRow.TrMonth = month;
                        newRow.GroupingKey = keyList._GroupingKey;
                        newRow.AccountOwner = keyList._AccountOwner;
                        newRow.IsInvestment = keyList._IsInvestment;
                        newRow[AmountColumn] = 0;
                        MainData.ViewMonthlyReport.AddViewMonthlyReportRow(newRow);
                        // subList.Add(month, null); // DIAG dont need to add it, rite?
                    }
                }
            }

            // ############## END OF DIAG MOVED TO TotalsByGroupingData ##############


            // DIAG WILL NOT BE NEEDED, with TotalsByGroupingData's new _DataByGroupingKey -- right?
            DataView dataByGroupingKey = new DataView(TotalsData.TotalsTbl, null, "GroupingKey", DataViewRowState.Unchanged);
            */

            TotalsData.LoadData(FromMonth, ToMonth, AccountOwner, AssetType, chBoxRefunds.Checked);
            BuildGroupingsTree(TotalsData.TotalsByGroupingView);

            RefreshDisplay();
        }

        List<string> _GroupingKeysList; 

        void RefreshDisplay()
        {
            /* Report Viewer is no longer used
            ReportDataSource rds = new ReportDataSource("DataSet1", MainData.ViewBudgetMonthlyReport as DataTable);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
            */

            // Get groupings to display, from checked 
            _GroupingKeysList = new List<string>();
            foreach (TreeNode node in tvGroupings.Nodes)
                AddToGroupingListIfChecked(node, ref _GroupingKeysList);

            // DIAG USE NEW MonthlyDataByGroupingKey class with .AddData()!!
            /*

            SortedList<string, DataView> reportDataByGroupingKey = new SortedList<string, DataView>();
            foreach (string groupingKey in _GroupingKeysList)
            {
                DataView view = new DataView(MainData.ViewMonthlyReport);
                view.Sort = "TrMonth ASC";
                view.RowFilter = "GroupingKey = '" + groupingKey + "'";
                reportDataByGroupingKey.Add(groupingKey, view);
            }

            // gridMain replaced by totalsGrid  --  PopulateMainGrid(reportDataByGroupingKey);
            */

            // DIAG new main grid!
            totalsGrid.RefreshDisplay(_GroupingKeysList);

            DrawChart(TotalsData.TotalViewsByGrouping);
        }

        void DrawChart(SortedList<string, DataView> reportDataByGroupingKey)
        {
            // Clear previous things:
            ClearChart();

            Axis axisX = chart1.ChartAreas[0].AxisX;
            Axis axisY = chart1.ChartAreas[0].AxisY;

            // Y axis (dollar amount) chart settings:
            decimal minAmount = Decimal.MaxValue;
            decimal maxAmount = Decimal.MinValue;
            foreach (string grouping in reportDataByGroupingKey.Keys)
            {
                foreach (DataRowView rowView in reportDataByGroupingKey[grouping])
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
            DateTime minDate = FromMonth;
            DateTime maxDate = ToMonth;
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

            foreach (string groupingKey in reportDataByGroupingKey.Keys)
            {
                MainDataSet.ViewGroupingsRow groupingRow = TotalsData.GroupingsTbl.FindByGroupingKey(groupingKey);
                DataView view = reportDataByGroupingKey[groupingKey];
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

        /* gridMain replaced by totalsGrid
        void PopulateMainGrid(SortedList<string, DataView> reportDataByGroupingKey)
        {
            gridMain.Rows.Clear();
            gridMain.Columns.Clear();

            gridMain.RowHeadersWidth = 120;

            SortedList<DateTime, object> monthsForGridColumns = new SortedList<DateTime, object>(); // Value not used
            SortedList<string, object> groupingsForGridRows = new SortedList<string, object>(); // Key = Grouping Key, Value = Grouping Label

            for (DateTime month = FromMonth; month <= ToMonth; month = month.AddMonths(1))
                monthsForGridColumns[month] = null;

            foreach (string groupingKey in reportDataByGroupingKey.Keys)
            {
                MainDataSet.ViewGroupingsRow groupingRow = _GroupingsTbl.FindByGroupingKey(groupingKey);
                groupingsForGridRows[groupingKey] = groupingRow.GroupingLabel;
            }

            Dictionary<DateTime, int> colIndices = new Dictionary<DateTime, int>();
            int colIndex = 0;
            foreach (DateTime trMonth in monthsForGridColumns.Keys)
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
            foreach (string groupingWithParent in groupingsForGridRows.Keys)
            {
                rowIndices[groupingWithParent] = rowIndex;
                //dataGridView1.Columns.Add("row" + rowIndex.ToString(), trMonth.ToString("MMM yy"));
                rowIndex++;
            }

            gridMain.RowCount = rowIndex;
            for (int ri = 0; ri < gridMain.RowCount; ri++)
            {
                DataGridViewRow row = gridMain.Rows[ri];
                row.HeaderCell.Value = groupingsForGridRows.Values[ri];
                row.Tag = groupingsForGridRows.Keys[ri];
            }

            foreach (string grouping in reportDataByGroupingKey.Keys)
            {
                foreach (DataRowView rowView in reportDataByGroupingKey[grouping])
                {
                    MainDataSet.ViewMonthlyReportRow tblRow = rowView.Row as MainDataSet.ViewMonthlyReportRow;
                    gridMain[colIndices[tblRow.TrMonth], rowIndices[tblRow.GroupingKey]].Value = tblRow[AmountColumn];
                }
            }
        }
        */

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Program.LookupTableSet.LoadWithRetryOption())
                return;

            comboAccountOwner.DataSource = Program.LookupTableSet.MainDataSet.AccountOwner;
            comboAccountOwner.ValueMember = "OwnerID";
            comboAccountOwner.DisplayMember = "OwnerDescription";
            comboAccountOwner.SelectedValue = "D";// initialize it

            comboAccountType.DataSource = AssetTypeClass.List;
            comboAccountType.ValueMember = "AssetType";
            comboAccountType.DisplayMember = "Label";
            comboAccountType.SelectedValue = AssetType.BankAndCash; // initialize it to Bank/Cash          

            DateTime minMonth = new DateTime(2022, 1, 1); // DIAG get from config!
            DateTime maxMonth = DateTime.Today.AddMonths(2);
            DateTime fromMonth = DateTime.Today.AddMonths(-14);
            DateTime toMonth = DateTime.Today;

            comboFromMonth.Populate(minMonth, maxMonth, fromMonth);
            comboToMonth.Populate(minMonth, maxMonth, toMonth);

            // Make totalsGrid printable by taking a Printable tag onto it:
            totalsGrid.Tag = new PrintableGridTag(totalsGrid);

            RefreshData();
        }

        /* gridMain replaced by totalsGrid
        private void gridMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DateTime cellMonth = (DateTime)gridMain.Columns[e.ColumnIndex].Tag;
            string cellGrouping = (string)gridMain.Rows[e.RowIndex].Tag;
            MonthGroupingForm monthGroupingForm = new MonthGroupingForm();
            monthGroupingForm.Initialize(cellMonth, cellGrouping, AccountOwner, AssetType);
            monthGroupingForm.ShowDialog();
        }
        */ 

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

        private void comboAccountOwner_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboAccountType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshData();
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

        private void comboFromMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboToMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshData();
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

        private void chBoxRefunds_CheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void monthlyAverageExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlyAverageExpensesForm form = new MonthlyAverageExpensesForm();
            form.ShowDialog();
        }
    }
}
