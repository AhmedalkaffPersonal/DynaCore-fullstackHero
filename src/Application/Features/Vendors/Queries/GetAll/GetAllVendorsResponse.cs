namespace DynaCore.Application.Features.Vendors.Queries.GetAll
{
    public class GetAllVendorsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal TaxNumber { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }

    }
}