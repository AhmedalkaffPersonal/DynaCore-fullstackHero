using DynaCore.Application.Specifications.Base;
using DynaCore.Domain.Entities.Catalog;

namespace DynaCore.Application.Specifications.Catalog
{
    public class ContactPersonFilterSpecification : HeroSpecification<ContactPerson>
    {
        public ContactPersonFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString) || p.PhoneNumber.Contains(searchString) || p.Email.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}