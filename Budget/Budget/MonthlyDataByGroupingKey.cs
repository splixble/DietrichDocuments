using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    // DIAG change Chart to use this!
    internal class MonthlyDataByGroupingKey : SortedList<string, DataView>
    {
        public void AddData(List<string> groupingKeysList, MainDataSet.ViewMonthlyReportDataTable monthlyDataTbl)
        {
            foreach (string groupingKey in groupingKeysList)
            {
                DataView view = new DataView(monthlyDataTbl);
                view.Sort = "TrMonth ASC";
                view.RowFilter = "GroupingKey = '" + groupingKey + "'";
                Add(groupingKey, view);
            }

        }
    }
}
