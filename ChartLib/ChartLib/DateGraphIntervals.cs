using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// NOTE: might want to remove the System.Windows.Forms and System.Windows.Forms.DataVisualization.Charting references, to make it WPF compatible

namespace ChartLib
{
    public enum DateGraphInterval { None, _1Day, _3Days, _1Week, _2Weeks, _1Month, _2Months, _3Months, _6Months, _1Year, _2Year, _5Year }; // add more if necessary

    public struct DateGraphIntervalStruct // struct class that you can put in a combo box
    {
        public DateGraphInterval _Value;

        public DateGraphIntervalStruct(DateGraphInterval val)
        {
            _Value = val;
        }

        public override string ToString()
        {
            return DateGraphIntervals.GetIntervalLabel(_Value);
        }
    }

    public static class DateGraphIntervals
    {
        public static DateGraphInterval GetInterval(TimeSpan dateRange, int minDivisions)
        {
            if (dateRange.TotalDays > 1825 * minDivisions)
                return DateGraphInterval._5Year;
            else if (dateRange.TotalDays > 730 * minDivisions)
                return DateGraphInterval._2Year;
            else if (dateRange.TotalDays > 365 * minDivisions)
                return DateGraphInterval._1Year;
            else if (dateRange.TotalDays > 183 * minDivisions)
                return DateGraphInterval._6Months;
            else if (dateRange.TotalDays > 91 * minDivisions)
                return DateGraphInterval._3Months;
            else if (dateRange.TotalDays > 61 * minDivisions)
                return DateGraphInterval._2Months;
            else if (dateRange.TotalDays > 30 * minDivisions)
                return DateGraphInterval._1Month;
            else if (dateRange.TotalDays > 14 * minDivisions)
                return DateGraphInterval._2Weeks;
            else if (dateRange.TotalDays > 7 * minDivisions)
                return DateGraphInterval._1Week;
            else if (dateRange.TotalDays > 3 * minDivisions)
                return DateGraphInterval._3Days;
            else
                return DateGraphInterval._1Day;
        }

        public static DateGraphInterval GetInterval(TimeSpan dateRange, int minDivisions, DateGraphInterval minInterval, DateGraphInterval maxInterval)
        {
            DateGraphInterval interval = GetInterval(dateRange, minDivisions);
            if (interval > maxInterval)
                return maxInterval;
            else if (interval < minInterval)
                return minInterval;
            else
                return interval;
        }

        public static string GetIntervalLabel(DateGraphInterval interval)
        {
            switch (interval)
            {
                case DateGraphInterval._1Day:
                    return "1 Day";
                case DateGraphInterval._3Days:
                    return "3 Days";
                case DateGraphInterval._1Week:
                    return "1 Week";
                case DateGraphInterval._2Weeks:
                    return "2 Weeks";
                case DateGraphInterval._1Month:
                    return "1 Month";
                case DateGraphInterval._2Months:
                    return "2 Months";
                case DateGraphInterval._3Months:
                    return "3 Months";
                case DateGraphInterval._6Months:
                    return "6 Months";
                case DateGraphInterval._1Year:
                    return "1 Year";
                case DateGraphInterval._2Year:
                    return "2 Year";
                case DateGraphInterval._5Year:
                    return "5 Year";
                default:
                    return "";
            }
        }

        public static void GetChartAxisInfo(DateGraphInterval interval, out int numUnits, out DateTimeIntervalType unitType)
        {
            switch (interval)
            {
                case DateGraphInterval._1Day:
                    numUnits = 1;
                    unitType = DateTimeIntervalType.Days;
                    return;
                case DateGraphInterval._3Days:
                    numUnits = 3;
                    unitType = DateTimeIntervalType.Days;
                    return;
                case DateGraphInterval._1Week:
                    numUnits = 1;
                    unitType = DateTimeIntervalType.Weeks;
                    return;
                case DateGraphInterval._2Weeks:
                    numUnits = 2;
                    unitType = DateTimeIntervalType.Weeks;
                    return;
                case DateGraphInterval._1Month:
                    numUnits = 1;
                    unitType = DateTimeIntervalType.Months;
                    return;
                case DateGraphInterval._2Months:
                    numUnits = 2;
                    unitType = DateTimeIntervalType.Months;
                    return;
                case DateGraphInterval._3Months:
                    numUnits = 3;
                    unitType = DateTimeIntervalType.Months;
                    return;
                case DateGraphInterval._6Months:
                    numUnits = 6;
                    unitType = DateTimeIntervalType.Months;
                    return;
                case DateGraphInterval._1Year:
                    numUnits = 1;
                    unitType = DateTimeIntervalType.Years;
                    return;
                case DateGraphInterval._2Year:
                    numUnits = 2;
                    unitType = DateTimeIntervalType.Years;
                    return;
                case DateGraphInterval._5Year:
                    numUnits = 5;
                    unitType = DateTimeIntervalType.Years;
                    return;
                default: // never used
                    numUnits = 0;
                    unitType = DateTimeIntervalType.NotSet;
                    return;
            }
        }

        public static void PopulateComboBox(ComboBox combo, DateGraphInterval minInterval, DateGraphInterval maxInterval)
        {
            combo.Items.Clear();
            for (DateGraphInterval interval = minInterval; interval <= maxInterval; interval++)
                combo.Items.Add(new DateGraphIntervalStruct(interval));
        }
    }
}
