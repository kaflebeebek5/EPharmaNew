namespace EPharma.Client.Infrastructure.Routes
{
    public static class IncomeExpensesEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/incomeExpenses/export";
        public static string GetAll = "api/v1/incomeExpenses";
        public static string Delete = "api/v1/incomeExpenses";
        public static string Save = "api/v1/incomeExpenses";
        public static string GetCount = "api/v1/incomeExpenses/count";
    }
}
