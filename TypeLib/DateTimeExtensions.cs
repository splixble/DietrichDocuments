using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLib
{
    /// <summary>
    /// Contains methods added to the System.DateTime class as Extension Methods.
    /// </summary>
    /// Even though these methods, internally, are members of this class rather than of System.String, these methods allows the developer to call custom-designed DateTime functions 
    /// with the same syntax as actual DateTime methods, and make use of IntelliSense and other Visual Studio features that make software development so much quicker and more intuitive. 
    public static class DateTimeExtensions
    {
        /** <summary> SQLDateLiteral returns a string representaion of a DateTime value that 
         * could be used in a well-formed SQL Server SQL query.</summary>
         * */
        public static string SQLDateLiteral(this DateTime date)
        {
            // if it were MS Access, it would be: 
            //    return "#" + date.ToString("yyyy-MM-dd") + "#";
            // no time figure -- but that doesn't matter; we're not using Access any more

            // This is the format for SQL Server:
            return "\'" + date.ToString("dd-MMM-yyyy HH:mm:ss") + "\'";
        }

        public static string MySQLDateLiteral(this DateTime date)
        {
            // This is the format for MySQL:
            return "\'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "\'";
        }

        public static string DotNetExpressionDateLiteral(this DateTime date)
        {
            return "#" + date.ToString("MM/dd/yyyy HH:mm:ss") + "#";
        }

        /// <summary>
        /// Extracts the date portion of a given DateTime, returning a DateTime with a vaue of midnight of the given DateTime's day.
        /// </summary>
        /// <param name="dateTime">DateTime value to extract the date from</param>
        /// <returns>A DateTime value</returns>
        /// This function is not needed as much now, since we changed DateTimeNullablePicker on 6/19/12 to initialize its 
        /// default date value to DateTime.Today (midnight of today's date) rather than leaving it as DateTime.Now. 
        /// NOTE: This function isn't actually needed, since it duplicates property DateTime.Date. But a couple things call it, so I left it in (10/24/17)
        public static DateTime Midnight(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static DateTime ToTheSecond(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static DateTime ToTheMinute(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        public static DateTime FirstOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime FirstOfQuarter(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, (dateTime.Month-1) / 3 + 1, 1);
        }

        public static int Quarter(this DateTime dateTime)
        {
            return (dateTime.Month - 1) / 3 + 1;
        }
    }
}

