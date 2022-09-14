namespace EPharma.Client.Infrastructure.Routes
{
    public static class StaticVariableEndpoints
    {
        public static string GetByName(string name)
        {
            return $"api/v1/staticvariable/{name}";
        }
        public static string GetAll = "api/v1/staticvariable";
        public static string GetCount = "api/v1/staticvariable/count";
    }
}
