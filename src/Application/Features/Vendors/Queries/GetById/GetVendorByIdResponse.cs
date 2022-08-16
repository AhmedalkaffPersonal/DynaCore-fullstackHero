namespace DynaCore.Application.Features.Vendors.Queries.GetById
{
    public class GetVendorByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TaxNumber { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }

    }
}