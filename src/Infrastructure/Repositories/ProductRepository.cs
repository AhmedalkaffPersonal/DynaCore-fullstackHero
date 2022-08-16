using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DynaCore.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepositoryAsync<Product, int> _repository;

        public ProductRepository(IRepositoryAsync<Product, int> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsVendorUsed(int vendorId)
        {
            return await _repository.Entities.AnyAsync(b => b.VendorId == vendorId);
        }
    }
}