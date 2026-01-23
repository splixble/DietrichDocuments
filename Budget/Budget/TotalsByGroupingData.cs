using Microsoft.Data.SqlClient; // NOTE: Must be Microsoft.Data.SqlClient, NOT System.Data.SqlClient
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public TotalsByGroupingData( bool adjustForRefunds)
        {
            _AdjustForRefunds = adjustForRefunds;

            _TotalsTbl = new MainDataSet.ViewMonthlyReportDataTable();
            _GroupingsTbl = new MainDataSet.ViewGroupingsDataTable();
        }

        public void LoadGroupings()
        {
            MainDataSetTableAdapters.ViewGroupingsTableAdapter groupingsAdap = new MainDataSetTableAdapters.ViewGroupingsTableAdapter();
            _GroupingsTbl.Clear();
            groupingsAdap.FillInSelectorOrder(_GroupingsTbl);
        }

        public void LoadTotals(DateTime fromMonth, DateTime toMonth, string accountOwner, AssetType assetType)
        { 
            _FromMonth = fromMonth;
            _ToMonth = toMonth;
            _AccountOwner = accountOwner;
            _AssetType = assetType;


            if (_AssetType == AssetType.Both)
            {

                // in this case we need to do a special query to combine total balances of both account types:
                string selectStr = "SELECT SUM(AmountNormalized) AS AmountNormalized, " +
                    "SUM(AmountRefundAdjustedNormalized) AS AmountRefundAdjustedNormalized, " +
                    "MAX(GroupingKey) AS GroupingKey, " +
                    "MAX(TrMonth) AS TrMonth, " +
                    "MAX(AccountOwner) AS AccountOwner, " +
                    "'' AS AccountType " +
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
        }

        SortedList<string, DataView> _ViewsByGroupingList = new SortedList<string, DataView>();
        public SortedList<string, DataView> ViewsByGroupingList => _ViewsByGroupingList;

        public void AddViewByGroupingList(string groupingKey)
        {               
            DataView view = new DataView(_TotalsTbl);
            view.Sort = "TrMonth ASC";
            view.RowFilter = "GroupingKey = '" + groupingKey + "'";
            _ViewsByGroupingList.Add(groupingKey, view);
        }
    }
}
