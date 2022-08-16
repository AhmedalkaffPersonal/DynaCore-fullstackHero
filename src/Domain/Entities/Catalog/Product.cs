using DynaCore.Domain.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynaCore.Domain.Entities.Catalog
{
    public class Product : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string SKU { get; set; }

        [Column(TypeName = "text")]
        public string ImageDataURL { get; set; }

        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}