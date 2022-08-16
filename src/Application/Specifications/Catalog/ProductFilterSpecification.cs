using DynaCore.Application.Specifications.Base;
using DynaCore.Domain.Entities.Catalog;

namespace DynaCore.Application.Specifications.Catalog
{
    public class ProductFilterSpecification : HeroSpecification<Product>
    {
        public ProductFilterSpecification(string searchString)
        {
            Includes.Add(a => a.Vendor);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.SKU != null && (p.Name.Contains(searchString) || p.Description.Contains(searchString) || p.SKU.Contains(searchString) || p.Vendor.Name.Contains(searchString) || p.Vendor.Address.Contains(searchString));
            }
            else
            {
                Criteria = p => p.SKU != null;
            }
        }
    }
}