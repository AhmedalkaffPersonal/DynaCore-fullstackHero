using DynaCore.Domain.Contracts;
using System.Collections.Generic;

namespace DynaCore.Domain.Entities.Catalog
{
    public class Vendor : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal TaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactPerson { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}