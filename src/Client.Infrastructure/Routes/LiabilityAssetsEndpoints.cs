namespace EPharma.Client.Infrastructure.Routes
{
    public static class LiabilityAssetsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/liabilityAssets/export";
        public static string GetAll = "api/v1/liabilityAssets";
        public static string Delete = "api/v1/liabilityAssets";
        public static string Save = "api/v1/liabilityAssets";
        public static string GetCount = "api/v1/liabilityAssets/count";
    }
}
