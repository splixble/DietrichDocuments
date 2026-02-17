using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TypeLib.DateTimeExtensions;
using static Budget.Constants;
using static Budget.MainDataSet;
using System.Drawing;

namespace Budget
{
    internal class TotalsByGroupingGrid : DataGridView
    {
        TotalsByGroupingData _TotalsData = null;
        public TotalsByGroupingData TotalsData => _TotalsData;

        List<string> _GroupingKeysDisplayed;

        public bool QuarterlyAverages = false;

        public TotalsByGroupingGrid()
        {
            ReadOnly = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            _TotalsData = new TotalsByGroupingData();
        }

        public void RefreshData(DateTime fromMonth, DateTime toMonth, string accountOwner, AssetType assetType, bool adjustForRefunds)
        {
            _TotalsData.LoadData(fromMonth, toMonth, accountOwner, assetType, adjustForRefunds);
        }

        public void RefreshDisplay(List<string> groupingKeysDisplayed)
        {
            _GroupingKeysDisplayed = groupingKeysDisplayed;

            // from Form1.PopulateMainGrid():
            Rows.Clear();
            Columns.Clear();

            RowHeadersWidth = 250; // DIAG should be a const, and should be saved

            SortedList<DateTime, object> monthsForGridColumns = new SortedList<DateTime, object>(); // Value not used
            SortedList<string, object> groupingsForGridRows = new SortedList<string, object>(); // Key = Grouping Key, Value = Grouping Label

            for (DateTime month = _TotalsData.FromMonth; month <= _TotalsData.ToMonth; month = month.AddMonths(1))
                monthsForGridColumns[month] = null;

            foreach (string groupingKey in _GroupingKeysDisplayed)
            {
                MainDataSet.ViewGroupingsRow groupingRow = _TotalsData.GroupingsTbl.FindByGroupingKey(groupingKey);
                groupingsForGridRows[groupingKey] = groupingRow.GroupingLabel;
            }

            Dictionary<DateTime, int> colIndicesOfTotals = new Dictionary<DateTime, int>();
            Dictionary<DateTime, int> colIndicesOfQtrlyAvgs = new Dictionary<DateTime, int>();
            int colIndex = 0;
            foreach (DateTime trMonth in monthsForGridColumns.Keys)
            {
                colIndicesOfTotals[trMonth] = colIndex;
                DataGridViewColumn col = AddAmountColumn("col" + colIndex.ToString(), trMonth.ToString("MMM yy"));
                col.Tag = new ColumnTag(ColumnTypes.Total, trMonth);
                colIndex++;

                // Insert a Quarterly Average column?
                if (QuarterlyAverages && trMonth.Month % 3 == 0)
                {
                    colIndicesOfQtrlyAvgs[trMonth.FirstOfQuarter()] = colIndex;
                    DataGridViewColumn qtrCol = AddAmountColumn("colQtrly" + colIndex.ToString(), "Q" + trMonth.Quarter().ToString() + trMonth.ToString(" yy"));
                    qtrCol.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    qtrCol.Tag = new ColumnTag(ColumnTypes.QuarterlyAverage, trMonth);
                    colIndex++;
                }
            }

            Dictionary<string, int> rowIndices = new Dictionary<string, int>();
            int rowIndex = 0;
            foreach (string groupingWithParent in groupingsForGridRows.Keys)
            {
                rowIndices[groupingWithParent] = rowIndex;
                //dataGridView1.Columns.Add("row" + rowIndex.ToString(), trMonth.ToString("MMM yy"));
                rowIndex++;
            }

            RowCount = rowIndex;

            bool colorGridRow = false;
            MainDataSet.ViewGroupingsRow prevGroupingRow = null;
            for (int ri = 0; ri < RowCount; ri++)
            {
                DataGridViewRow row = Rows[ri];
                row.HeaderCell.Value = groupingsForGridRows.Values[ri];
                row.Tag = groupingsForGridRows.Keys[ri];

                // Stripe the rows, based on grouping key or parent:
                if (colorGridRow)
                    row.DefaultCellStyle.BackColor = Color.LightGreen;

                // toggle row color?
                MainDataSet.ViewGroupingsRow groupingRow = _TotalsData.GroupingsTbl.FindByGroupingKey(groupingsForGridRows.Keys[ri]);
                if (prevGroupingRow != null &&
                    (groupingRow.IsParentKeyNull() || prevGroupingRow.IsParentKeyNull() || // either grouping is parent key
                    groupingRow.ParentKey != prevGroupingRow.ParentKey))  // parent keys are different
                    colorGridRow = !colorGridRow;
                prevGroupingRow = groupingRow;
            }

            Dictionary<string, Dictionary<DateTime, decimal>> qtrTotalsByGrouping = new  Dictionary<string, Dictionary<DateTime, decimal>>();
            // - used only if QuarterlyAverages is set

            foreach (string grouping in _GroupingKeysDisplayed)
            {
                foreach (DataRowView rowView in _TotalsData.TotalViewsByGrouping[grouping])
                {
                    // Fill in monthly totals in grid, and calculate quarterly totals for quarterly averages:
                    MainDataSet.ViewMonthlyReportRow tblRow = rowView.Row as MainDataSet.ViewMonthlyReportRow;
                    decimal monthlyTotal = (decimal)tblRow[_TotalsData.AmountColumn];

                    if (monthlyTotal != 0) // NOTE: CHANGED ON 2/16/26 TO SHOW BLANK FOR 0
                        this[colIndicesOfTotals[tblRow.TrMonth], rowIndices[tblRow.GroupingKey]].Value = monthlyTotal;

                    if (QuarterlyAverages)
                    {
                        if (!qtrTotalsByGrouping.ContainsKey(tblRow.GroupingKey))
                            qtrTotalsByGrouping[tblRow.GroupingKey] = new Dictionary<DateTime, decimal>();
                        Dictionary<DateTime, decimal> subdictByQtr = qtrTotalsByGrouping[tblRow.GroupingKey];

                        if (!subdictByQtr.ContainsKey(tblRow.TrMonth.FirstOfQuarter()))
                            subdictByQtr[tblRow.TrMonth.FirstOfQuarter()] = monthlyTotal;
                        else
                            subdictByQtr[tblRow.TrMonth.FirstOfQuarter()] += monthlyTotal;
                    }
                }
            }

            if (QuarterlyAverages)
            {
                // Fill in quarterly averages in grid:
                foreach (string groupingKey in qtrTotalsByGrouping.Keys)
                {
                    Dictionary<DateTime, decimal> subdictByQtr = qtrTotalsByGrouping[groupingKey];
                    foreach (DateTime quarterDate in subdictByQtr.Keys)
                    {
                        decimal qtrlyAvg = subdictByQtr[quarterDate] / 3;
                        this[colIndicesOfQtrlyAvgs[quarterDate], rowIndices[groupingKey]].Value = qtrlyAvg;
                    }
                }
            }
        }

        DataGridViewColumn AddAmountColumn(string columnName, string headerText)
        {
            int colIndex = Columns.Add("col" + columnName, headerText);
            DataGridViewColumn col = Columns[colIndex];
            col.Width = 70;
            col.ValueType = typeof(Decimal);
            col.DefaultCellStyle.Format = "C"; // or "$#,0.00";
            return col;
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            ColumnTag colTag = (ColumnTag)Columns[e.ColumnIndex].Tag;
            if (colTag._ColumnType == ColumnTypes.Total)
            {
                string cellGrouping = (string)Rows[e.RowIndex].Tag;
                MonthGroupingForm monthGroupingForm = new MonthGroupingForm();
                monthGroupingForm.Initialize(colTag._Month, cellGrouping, _TotalsData.AccountOwner, _TotalsData.AssetType);
                monthGroupingForm.ShowDialog();
            }
        }

        // internally used types:

        enum ColumnTypes { Total, QuarterlyAverage };

        struct ColumnTag
        {
            public ColumnTypes _ColumnType;
            public DateTime _Month; // 1st day of month of total, or 1st day of quarter

            public ColumnTag(ColumnTypes columnType, DateTime month)
            {
                _ColumnType = columnType;
                _Month = month;
            }
        }
    }
}
