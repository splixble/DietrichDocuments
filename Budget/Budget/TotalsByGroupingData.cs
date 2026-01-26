using Microsoft.Data.SqlClient; // NOTE: Must be Microsoft.Data.SqlClient, NOT System.Data.SqlClient
using PrintLib;
using System;
using System.Collections.Generic;
using System.Data;
using TypeLib;
using static Budget.MainDataSet;
//using static TypeLib.DBUtils;

namespace Budget
{
    internal class TotalsByGroupingData
    {
        string _AccountOwner;
        public string AccountOwner => _AccountOwner;

        AssetType _AssetType;
        public AssetType AssetType => _AssetType;

        DateTime _FromMonth;
        public DateTime FromMonth => _FromMonth;

        DateTime _ToMonth;
        public DateTime ToMonth => _ToMonth;   

        bool _AdjustForRefunds; 
        public bool AdjustForRefunds => _AdjustForRefunds;

        MainDataSet.ViewMonthlyReportDataTable _TotalsTbl;
        public MainDataSet.ViewMonthlyReportDataTable TotalsTbl => _TotalsTbl;

        MainDataSet.ViewGroupingsDataTable _GroupingsTbl;
        public MainDataSet.ViewGroupingsDataTable GroupingsTbl => _GroupingsTbl;

        // View to check whether there are any rows in _TotalsTbl with a particular grouping:
        DataView _TotalsByGroupingView;
        public DataView TotalsByGroupingView => _TotalsByGroupingView;

        public DataColumn AmountColumn
        {
            get
            {
                if (_AdjustForRefunds)
                    return _TotalsTbl.AmountRefundAdjustedNormalizedColumn;
                else
                    return _TotalsTbl.AmountNormalizedColumn;
            }
        }

        /* DIAG REMOVE -- this complictes things unnecessarily
        // events:
        public delegate void DisplayingGroupingHandler(TotalsByGroupingData sender, DisplayingGroupingEventArgs e);
        public event DisplayingGroupingHandler DisplayingGrouping;

        public class DisplayingGroupingEventArgs
        {
            public ViewGroupingsRow _GroupingRow;
            public bool Cancel = false;

            public DisplayingGroupingEventArgs(ViewGroupingsRow groupingRow)
            {
                _GroupingRow = groupingRow;
            }
        }
        */ 

        public TotalsByGroupingData()
        {
            _TotalsTbl = new MainDataSet.ViewMonthlyReportDataTable();
            _GroupingsTbl = new MainDataSet.ViewGroupingsDataTable();
            _TotalsByGroupingView = new DataView(_TotalsTbl, null, "GroupingKey", DataViewRowState.Unchanged);
        }

        public void LoadData(DateTime fromMonth, DateTime toMonth, string accountOwner, AssetType assetType, bool adjustForRefunds)
        {
            LoadGroupings();
            LoadTotals(fromMonth, toMonth, accountOwner, assetType, adjustForRefunds);
        }

        public void LoadGroupings()
        {
            MainDataSetTableAdapters.ViewGroupingsTableAdapter groupingsAdap = new MainDataSetTableAdapters.ViewGroupingsTableAdapter();
            _GroupingsTbl.Clear();
            groupingsAdap.FillInSelectorOrder(_GroupingsTbl);
        }

        public void LoadTotals(DateTime fromMonth, DateTime toMonth, string accountOwner, AssetType assetType, bool adjustForRefunds)
        { 
            _FromMonth = fromMonth;
            _ToMonth = toMonth;
            _AccountOwner = accountOwner;
            _AssetType = assetType;
            _AdjustForRefunds = adjustForRefunds;

            if (_AssetType == AssetType.Both)
            {

                // in this case we need to do a special query to combine total balances of both account types:
                string selectStr = "SELECT SUM(AmountNormalized) AS AmountNormalized, " +
                    "SUM(AmountRefundAdjustedNormalized) AS AmountRefundAdjustedNormalized, " +
                    "MAX(GroupingKey) AS GroupingKey, " +
                    "MAX(TrMonth) AS TrMonth, " +
                    "MAX(AccountOwner) AS AccountOwner, " +
                    "'' AS AccountType, " + // just because we need a dummy value
                    "0 AS IsInvestment " + // just because we need a dummy value
                    "FROM ViewMonthlyReport " +
                    "WHERE AccountOwner = '" + _AccountOwner + "'" +
                    "AND TrMonth >= " + _FromMonth.SQLDateLiteral() +
                    "AND TrMonth <= " + _ToMonth.SQLDateLiteral() +
                    "GROUP BY GroupingKey, TrMonth, AccountOwner";

                using (SqlConnection reportDataConn = new SqlConnection(Properties.Settings.Default.BudgetConnectionString))
                {
                    // reportDataConn.Open();
                    SqlCommand reportDataCmd = new SqlCommand();
                    // this dont compile: CommandBehavior fillCommandBehavior = FillCommandBehavior;
                    reportDataCmd.Connection = Program.DbConnection;
                    reportDataCmd.CommandText = selectStr;

                    SqlDataAdapter reportDataAdap = new SqlDataAdapter(reportDataCmd);
                    _TotalsTbl.Clear();
                    reportDataAdap.Fill(_TotalsTbl);

                }
            }
            else
            {
                MainDataSetTableAdapters.ViewMonthlyReportTableAdapter adap = new MainDataSetTableAdapters.ViewMonthlyReportTableAdapter();
                adap.FillByDateRange(_TotalsTbl, _FromMonth, _ToMonth, _AccountOwner, (_AssetType == AssetType.Investments ? (byte)1 : (byte)0));
            }

            // Fill in missing months gaps in data for each Grouping, AccountOwner, and IsInv with 0-amount rows,
            // to avoid discontinuous lines on the chart:
            // (outer Key is array of Grouping, AccountOwner, and AccountType; inner Value is not used)
            SortedList<GroupingAccOwnerIsInvestment, SortedList<DateTime, object>> rowsByKeysAndMonth = new SortedList<GroupingAccOwnerIsInvestment, SortedList<DateTime, object>>();
            foreach (ViewMonthlyReportRow reportRow in _TotalsTbl)
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
                for (DateTime month = _FromMonth; month <= _ToMonth; month = month.AddMonths(1))
                {
                    if (!subList.ContainsKey(month))
                    {
                        MainDataSet.ViewMonthlyReportRow newRow = _TotalsTbl.NewViewMonthlyReportRow();
                        newRow.TrMonth = month;
                        newRow.GroupingKey = keyList._GroupingKey;
                        newRow.AccountOwner = keyList._AccountOwner;
                        newRow.IsInvestment = keyList._IsInvestment;
                        newRow[AmountColumn] = 0; // DIAG or we could just set both cols to 0
                        _TotalsTbl.AddViewMonthlyReportRow(newRow);
                    }
                }
            }

            foreach (MainDataSet.ViewGroupingsRow groupingRow in _GroupingsTbl)
            {
                if (_TotalsByGroupingView.Find(groupingRow.GroupingKey) >= 0)
                    AddTotalViewByGrouping(groupingRow);
            }
        }

        SortedList<string, DataView> _TotalViewsByGrouping = new SortedList<string, DataView>();
        public SortedList<string, DataView> TotalViewsByGrouping => _TotalViewsByGrouping;

        public void AddTotalViewByGrouping(MainDataSet.ViewGroupingsRow groupingRow)
        {
            if (_TotalViewsByGrouping.ContainsKey(groupingRow.GroupingKey))
                return; // dont add duplicate entry

            // Create DataView of all the Totals rows with this grouping:
            DataView view = new DataView(_TotalsTbl);
            view.Sort = "TrMonth ASC";
            view.RowFilter = "GroupingKey = '" + groupingRow.GroupingKey + "'";
            _TotalViewsByGrouping.Add(groupingRow.GroupingKey, view);
        }
    }
}
