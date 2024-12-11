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
        public static class GroupingType
        {
            public const string BalanceTotal = "L";
            public const string BalanceOfAccount = "A";
        }
        public static class GroupingName
        {
            public const string Income = "Income";
            public const string Expense = "Expenses";
            public const string FundBalanceChange = "FBC";
        }
    }
}
