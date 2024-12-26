namespace BudgetWPF
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

        public static string ConnectionString => "Data Source=speepmaster\\sqlexpress;Initial Catalog=Budget;Integrated Security=True;TrustServerCertificate=True";
    }
}