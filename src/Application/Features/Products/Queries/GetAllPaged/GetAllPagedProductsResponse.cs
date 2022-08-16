namespace DynaCore.Application.Features.Products.Queries.GetAllPaged
{
    public class GetAllPagedProductsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public string Vendor { get; set; }
        public int VendorId { get; set; }
    }
}