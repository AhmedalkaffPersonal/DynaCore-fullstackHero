namespace DynaCore.Client.Infrastructure.Routes
{
    public static class VendorsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/vendors/export";

        public static string GetAll = "api/v1/vendors";
        public static string Delete = "api/v1/vendors";
        public static string Save = "api/v1/vendors";
        public static string GetCount = "api/v1/vendors/count";
    }
}