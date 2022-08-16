namespace DynaCore.Client.Infrastructure.Routes
{
    public static class ContactPersonsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string GetAll = "api/v1/contactPersons";
        public static string Save = "api/v1/contactPersons";
        public static string Delete = "api/v1/contactPersons";
        public static string Export = "api/v1/contactPersons/export";
    }
}