using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    internal static class Constants
    {
        public static class AccountType
        {
            public const char Bank = 'B';
            public const char Investment = 'I';
            public const char BothValue = '-';
        }
        public static class GroupingName
        {
            public const string FundBalanceChange = "FBC";
        }
        public static class GroupingKey
        {
            public const string Income = "TIN_";
            public const string Expenses = "E_";
            public const string BalanceTotal = "B_";
        }
    }
}
