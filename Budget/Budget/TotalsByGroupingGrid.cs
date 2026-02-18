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

            EnableHeadersVisualStyles = false;        //   to recolor row headers

            _TotalsData = new TotalsByGroupingData();
        }

        public void RefreshData(DateTime fromMonth, DateTime toMonth, string accountOwner, AssetType assetType, bool adjustForRefunds)
        {
            _TotalsData.LoadData(fromMonth, toMonth, accountOwner, assetType, adjustForRefunds);
        }

        bool[] _StripedGroupingRows;

        public void RefreshDisplay(List<string> groupingKeysDisplayed)
        {
            // DIAG no need to copy to member -- _GroupingKeysDisplayed is only used in this func
            _GroupingKeysDisplayed = groupingKeysDisplayed;

            // from Form1.PopulateMainGrid():
            Rows.Clear();
            Columns.Clear();

            RowHeadersWidth = 250; // DIAG should be a const, and should be saved

            SortedList<DateTime, object> monthsForGridColumns = new SortedList<DateTime, object>(); // Value not used
            SortedList<string, object> groupingsForGridRows = new SortedList<string, object>(); // Key = Grouping Key, value not used

            for (DateTime month = _TotalsData.FromMonth; month <= _TotalsData.ToMonth; month = month.AddMonths(1))
                monthsForGridColumns[month] = null;

            foreach (string groupingKey in _GroupingKeysDisplayed)
            {
                MainDataSet.ViewGroupingsRow groupingRow = _TotalsData.GroupingsTbl.FindByGroupingKey(groupingKey);
                groupingsForGridRows.Add(groupingKey, null);
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
                    qtrCol.Tag = new ColumnTag(ColumnTypes.QuarterlyAverage, trMonth);
                    colIndex++;
                }
            }

            // Make list of ViewGrouping rows in same order that they were queried in:
            List<MainDataSet.ViewGroupingsRow> groupingRowsInGridOrder = new List<MainDataSet.ViewGroupingsRow>();
            foreach (MainDataSet.ViewGroupingsRow groupingRow in _TotalsData.GroupingsTbl)
                if (groupingsForGridRows.ContainsKey(groupingRow.GroupingKey))
                    groupingRowsInGridOrder.Add(groupingRow);

            _StripedGroupingRows = new bool[groupingRowsInGridOrder.Count];
            // - parallels groupingRowsInGridOrder
            bool colorGridRow = false;
            MainDataSet.ViewGroupingsRow prevGroupingRow = null;
            for (int grx = 0; grx < groupingRowsInGridOrder.Count; grx++)
            {
                MainDataSet.ViewGroupingsRow groupingRow = groupingRowsInGridOrder[grx];
                if (prevGroupingRow != null &&
                    (groupingRow.IsParentKeyNull() || prevGroupingRow.IsParentKeyNull() || // either grouping is parent key
                    groupingRow.ParentKey != prevGroupingRow.ParentKey))  // parent keys are different
                    colorGridRow = !colorGridRow;
                prevGroupingRow = groupingRow;

                // Stripe the rows, based on grouping key or parent:
                _StripedGroupingRows[grx] = colorGridRow;
            }

            RowCount = groupingsForGridRows.Count;

            // color the striped row headers (can't do that with a CelFormatting event handler apparently)
            for (int r = 0; r < RowCount; r++)
            {
                if (_StripedGroupingRows[r])
                    Rows[r].HeaderCell.Style.BackColor = Color.DarkSeaGreen; // TODO color s/b const
            }

            Dictionary<string, int> rowIndices = new Dictionary<string, int>(); // Key = Grouping Key, obj = grid row index
            int rowIndex = 0;

            // DIAG use groupingRowsInGridOrder, dont duplicate filtering:
            foreach (MainDataSet.ViewGroupingsRow groupingRow in _TotalsData.GroupingsTbl)
            {
                if (groupingsForGridRows.ContainsKey(groupingRow.GroupingKey))
                {
                    rowIndices[groupingRow.GroupingKey] = rowIndex;
                    Rows[rowIndex].HeaderCell.Value = groupingRow.GroupingLabel;
                    Rows[rowIndex].Tag = new RowTag(groupingRow.GroupingKey);
                    rowIndex++;
                }
            }
            /* Replaces this on 2/17/26:
            foreach (string groupingWithParent in groupingsForGridRows.Keys)
            {
                rowIndices[groupingWithParent] = rowIndex;
                //dataGridView1.Columns.Add("row" + rowIndex.ToString(), trMonth.ToString("MMM yy"));
                rowIndex++;
            }
            */

            /* REMOVE
            bool colorGridRow = false;
            MainDataSet.ViewGroupingsRow prevGroupingRow = null;
            for (int ri = 0; ri < RowCount; ri++)
            {
                DataGridViewRow row = Rows[ri];
                RowTag rowTag = (RowTag)Rows[ri].Tag;

                /* REMOVE              
                
                // NOPE == gotta get row index from ...

                row.HeaderCell.Value = groupingsForGridRows.Values[ri];
                row.Tag = groupingsForGridRows.Keys[ri];
                * /

                // toggle row color?
                MainDataSet.ViewGroupingsRow groupingRow = _TotalsData.GroupingsTbl.FindByGroupingKey(rowTag._GroupingKey);
                if (prevGroupingRow != null &&
                    (groupingRow.IsParentKeyNull() || prevGroupingRow.IsParentKeyNull() || // either grouping is parent key
                    groupingRow.ParentKey != prevGroupingRow.ParentKey))  // parent keys are different
                    colorGridRow = !colorGridRow;
                prevGroupingRow = groupingRow;

                // Stripe the rows, based on grouping key or parent:
                if (colorGridRow)
                    rowTag._StripeTheGridRow = true;
            }
            */

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

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);

            bool quarterlyAverageColumn = Columns[e.ColumnIndex].Tag is ColumnTag && ((ColumnTag)Columns[e.ColumnIndex].Tag)._ColumnType == ColumnTypes.QuarterlyAverage;
            // REMO bool stripedRow = ((RowTag)Rows[e.RowIndex].Tag)._StripeTheGridRow;
            bool stripedRow = _StripedGroupingRows[e.RowIndex];

            if (quarterlyAverageColumn)
            {
                // TODO the colors should really be consts
                if (stripedRow)
                    e.CellStyle.BackColor = Color.LightSeaGreen;
                else
                    e.CellStyle.BackColor = Color.LightSkyBlue;
            }
            else if (stripedRow)
                e.CellStyle.BackColor = Color.PaleGreen;
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
                string cellGrouping = ((RowTag)Rows[e.RowIndex].Tag)._GroupingKey;
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

        struct RowTag
        {
            public string _GroupingKey;
            // REMOVE public bool _StripeTheGridRow;

            public RowTag(string  groupingKey)
            {
                _GroupingKey = groupingKey;
                // REMOVE _StripeTheGridRow = false;
            }
        }
    }
}
