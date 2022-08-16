using DynaCore.Application.Specifications.Base;
using DynaCore.Domain.Entities.Catalog;

namespace DynaCore.Application.Specifications.Catalog
{
    public class VendorFilterSpecification : HeroSpecification<Vendor>
    {
        public VendorFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Address.Contains(searchString) || p.TaxNumber.ToString().Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
