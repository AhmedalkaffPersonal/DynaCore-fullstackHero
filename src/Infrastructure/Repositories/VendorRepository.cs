using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;

namespace DynaCore.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly IRepositoryAsync<Vendor, int> _repository;

        public VendorRepository(IRepositoryAsync<Vendor, int> repository)
        {
            _repository = repository;
        }
    }
}