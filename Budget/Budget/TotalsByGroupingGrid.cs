using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Budget.Constants;
using static Budget.MainDataSet;

namespace Budget
{
    internal class TotalsByGroupingGrid : DataGridView
    {
        /*
        public string _AccountOwner;
        public AssetType _AssetType;
        public DateTime _FromMonth;
        public DateTime _ToMonth;
        DataColumn _AmountColumn;

        MainDataSet.ViewMonthlyReportDataTable _DataTbl;
        MainDataSet.ViewGroupingsDataTable _GroupingsTbl;
        List<string> _GroupingKeys;
        */

        TotalsByGroupingData _TotalsData = null;
        public TotalsByGroupingData TotalsData => _TotalsData;

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
            _TotalsData.LoadGroupings();
            _TotalsData.LoadTotals(fromMonth, toMonth, accountOwner, assetType, adjustForRefunds);


            /*
            foreach(string groupingKey in groupingKeys) 
                _TotalsData.AddDisplayedGrouping(groupingKey);

            _DataTbl = dataTbl;
            _GroupingsTbl = groupingsTbl;
            _GroupingKeys = groupingKeys;
            _FromMonth = fromMonth;
            _ToMonth = toMonth;
            _AmountColumn = amountColumn;

            MonthlyDataByGroupingKey reportDataByGroupingKey = new MonthlyDataByGroupingKey(); // should this be a Member?
            reportDataByGroupingKey.AddData(_GroupingKeys, _DataTbl);
            */

            // from Form1.PopulateMainGrid():
            Rows.Clear();
            Columns.Clear();

            RowHeadersWidth = 120; // DIAG should be a const, longer, and should be saved

            SortedList<DateTime, object> monthsForGridColumns = new SortedList<DateTime, object>(); // Value not used
            SortedList<string, object> groupingsForGridRows = new SortedList<string, object>(); // Key = Grouping Key, Value = Grouping Label

            for (DateTime month = _TotalsData.FromMonth; month <= _TotalsData.ToMonth; month = month.AddMonths(1))
                monthsForGridColumns[month] = null;

            foreach (string groupingKey in _TotalsData.DisplayedGroupings.Keys)
            {
                MainDataSet.ViewGroupingsRow groupingRow = _TotalsData.GroupingsTbl.FindByGroupingKey(groupingKey);
                groupingsForGridRows[groupingKey] = groupingRow.GroupingLabel;
            }

            Dictionary<DateTime, int> colIndices = new Dictionary<DateTime, int>();
            int colIndex = 0;
            foreach (DateTime trMonth in monthsForGridColumns.Keys)
            {
                colIndices[trMonth] = colIndex;
                Columns.Add("col" + colIndex.ToString(), trMonth.ToString("MMM yy"));
                DataGridViewColumn col = Columns[colIndex];
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

            RowCount = rowIndex;
            for (int ri = 0; ri < RowCount; ri++)
            {
                DataGridViewRow row = Rows[ri];
                row.HeaderCell.Value = groupingsForGridRows.Values[ri];
                row.Tag = groupingsForGridRows.Keys[ri];
            }

            foreach (string grouping in _TotalsData.DisplayedGroupings.Keys)
            {
                foreach (DataRowView rowView in _TotalsData.DisplayedGroupings[grouping])
                {
                    MainDataSet.ViewMonthlyReportRow tblRow = rowView.Row as MainDataSet.ViewMonthlyReportRow;
                    this[colIndices[tblRow.TrMonth], rowIndices[tblRow.GroupingKey]].Value = tblRow[_TotalsData.AmountColumn];
                }
            }
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DateTime cellMonth = (DateTime)Columns[e.ColumnIndex].Tag;
            string cellGrouping = (string)Rows[e.RowIndex].Tag;
            MonthGroupingForm monthGroupingForm = new MonthGroupingForm();
            monthGroupingForm.Initialize(cellMonth, cellGrouping, _TotalsData.AccountOwner, _TotalsData.AssetType);
            monthGroupingForm.ShowDialog();

        }
    }
}
