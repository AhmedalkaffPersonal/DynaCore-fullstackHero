using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;

namespace DynaCore.Infrastructure.Repositories
{
    public class ContactPersonRepository : IContactPersonRepository
    {
        private readonly IRepositoryAsync<ContactPerson, int> _repository;

        public ContactPersonRepository(IRepositoryAsync<ContactPerson, int> repository)
        {
            _repository = repository;
        }
    }
}