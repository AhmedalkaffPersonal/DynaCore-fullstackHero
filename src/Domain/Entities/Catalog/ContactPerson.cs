using DynaCore.Domain.Contracts;

namespace DynaCore.Domain.Entities.Catalog
{
    public class ContactPerson : AuditableEntity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
