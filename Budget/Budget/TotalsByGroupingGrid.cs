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

            Dictionary<DateTime, int> colIndices = new Dictionary<DateTime, int>();
            int colIndex = 0;
            foreach (DateTime trMonth in monthsForGridColumns.Keys)
            {
                colIndices[trMonth] = colIndex;
                DataGridViewColumn col = AddAmountColumn("col" + colIndex.ToString(), trMonth.ToString("MMM yy"));
                col.Tag = new ColumnTag(ColumnTypes.Total, trMonth);
                colIndex++;

                // Insert a Quarterly Average column?
                if (QuarterlyAverages && trMonth.Month % 3 == 0)
                {
                    DataGridViewColumn qtyCol = AddAmountColumn("colQtrly" + colIndex.ToString(), "Q" + trMonth.Quarter().ToString() + trMonth.ToString(" yy"));
                    qtyCol.Tag = new ColumnTag(ColumnTypes.QuarterlyAverage, trMonth);
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
            for (int ri = 0; ri < RowCount; ri++)
            {
                DataGridViewRow row = Rows[ri];
                row.HeaderCell.Value = groupingsForGridRows.Values[ri];
                row.Tag = groupingsForGridRows.Keys[ri];
            }

            foreach (string grouping in _GroupingKeysDisplayed)
            {
                foreach (DataRowView rowView in _TotalsData.TotalViewsByGrouping[grouping])
                {
                    MainDataSet.ViewMonthlyReportRow tblRow = rowView.Row as MainDataSet.ViewMonthlyReportRow;
                    this[colIndices[tblRow.TrMonth], rowIndices[tblRow.GroupingKey]].Value = tblRow[_TotalsData.AmountColumn];
                }
            }
        }

        DataGridViewColumn AddAmountColumn(string columnName, string headerText)
        {
            int colIndex = Columns.Add("col" + columnName, headerText);
            DataGridViewColumn col = Columns[colIndex];
            col.Width = 70;
            col.ValueType = typeof(Decimal);
            col.DefaultCellStyle.Format = "$0,0.00";
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
