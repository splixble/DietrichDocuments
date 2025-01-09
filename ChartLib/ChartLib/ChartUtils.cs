using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartLib
{
    public static class ChartUtils
    {
        /// <summary>
        /// Returns default interval, interval type, padded minimum, and padded maximum for an X or Y grid axis that shows numeric values.
        /// </summary>
        /// <param name="minDivisions">Minimum grid-line-delimited expanses to divide the grid axis into.</param>
        /// <param name="minDate">Input/output parameter; input as the minimum value; output as the minimum grid-line value on the graph, 
        /// "padded" if necessary to the nearest Interval.</param>
        /// <param name="maxDate">Input/output parameter; input as the maximum value; output as the maximum grid-line value on the graph, 
        /// "padded" if necessary to the nearest Interval.</param>
        /// <param name="interval">Output parameter; value to assign the Axis's Interval to.</param>
        /// <param name="intervalType">Output parameter; value to assign the Axis's Interval Type to.</param>
        public static void GetDefaultIntervalInfo(int minDivisions, ref double minDataNum, ref double maxDataNum, out double interval, out DateTimeIntervalType intervalType)
        {
            // Deal with there being only 1 data point in range - make it look OK:
            if (minDataNum == maxDataNum)
            {
                double tokenIncrement = Math.Abs(minDataNum / 10);
                minDataNum -= tokenIncrement;
                maxDataNum += tokenIncrement;
            }

            double valueRange = maxDataNum - minDataNum;
            double maxInterval = valueRange / minDivisions;
            int rangeScale = (int)Math.Floor(Math.Log10(maxInterval)); // this is the # digits minus 1, or analogous value if it's fractional
            double rangeScaleMultiplier = Math.Pow(10, rangeScale);
            // Make interval the digit 1, 2, or 5 (depending on what the 1st signif. digit is), scaled by the range scale:
            if (maxInterval > 5 * rangeScaleMultiplier)
                interval = 5 * rangeScaleMultiplier;
            else if (maxInterval > 2 * rangeScaleMultiplier)
                interval = 2 * rangeScaleMultiplier;
            else
                interval = rangeScaleMultiplier;

            // Make Axis Maximum and Minimum the max & min values, padded if necc. to the next or previous interval boundary:
            maxDataNum = Math.Ceiling(maxDataNum / interval) * interval;
            minDataNum = Math.Floor(minDataNum / interval) * interval;

            intervalType = DateTimeIntervalType.Number;
        }

        /// <summary>
        /// Returns default interval, interval type, padded minimum, and padded maximum for an X or Y grid axis that shows date values.
        /// </summary>
        /// <param name="minDivisions">Minimum grid-line-delimited expanses to divide the grid axis into.</param>
        /// <param name="minDate">Input/output parameter; input as the minimum date value; output as the minimum grid-line value on the graph, 
        /// "padded" if necessary to the nearest Interval.</param>
        /// <param name="maxDate">Input/output parameter; input as the maximum date value; output as the maximum grid-line value on the graph, 
        /// "padded" if necessary to the nearest Interval.</param>
        /// <param name="interval">Output parameter; value to assign the Axis's Interval to.</param>
        /// <param name="intervalType">Output parameter; value to assign the Axis's Interval Type to.</param>
        /// <param name="dateInterval">Output parameter; DateGraphInterval referring to the length of time represented by each gridline on the axis.</param>
        public static void GetDefaultIntervalInfo(int minDivisions, ref DateTime minDate, ref DateTime maxDate,
            out double interval, out DateTimeIntervalType intervalType, out DateGraphInterval dateInterval)
        {
            // Deal with there being only 1 data point in range - make it look OK:
            if (minDate == maxDate)
            {
                minDate = minDate.AddHours(-1);
                maxDate = maxDate.AddHours(1);
            }

            TimeSpan dateRange = maxDate - minDate;
            dateInterval = DateGraphIntervals.GetInterval(dateRange, minDivisions);

            int nInterval;
            DateGraphIntervals.GetChartAxisInfo(dateInterval, out nInterval, out intervalType);

            // Pad the Maximum and Minimum to the next or previous interval boundary:
            TimeSpan totalTimeSpan = maxDate - minDate;
            int daysInInterval;
            int numIntervals;
            switch (intervalType)
            {
                case DateTimeIntervalType.Years: // first of the year, to first of the next year
                    minDate = new DateTime(minDate.Year, 1, 1);
                    maxDate = new DateTime(maxDate.Year + 1, 1, 1);
                    break;
                case DateTimeIntervalType.Months: // first of the month in the month interval
                    minDate = new DateTime(minDate.Year, ((minDate.Month - 1) / nInterval * nInterval) + 1, 1);
                    maxDate = new DateTime(maxDate.Year, ((maxDate.Month - 1) / nInterval * nInterval) + 1, 1).AddMonths(nInterval);
                    break;
                case DateTimeIntervalType.Weeks: // round up the time span to an even multiple of weeks
                    daysInInterval = nInterval * 7;
                    numIntervals = (int)Math.Ceiling(totalTimeSpan.TotalDays / daysInInterval);
                    minDate = minDate.Date; // midnight of the first day of the span
                    maxDate = minDate.AddDays(numIntervals * daysInInterval);
                    break;
                case DateTimeIntervalType.Days: // round up the time span to an even multiple of days
                    daysInInterval = nInterval;
                    numIntervals = (int)Math.Ceiling(totalTimeSpan.TotalDays / daysInInterval);
                    minDate = minDate.Date; // midnight of the first day of the span
                    maxDate = minDate.AddDays(numIntervals * daysInInterval);
                    break;
            }
            interval = nInterval;
        }
    }
}
