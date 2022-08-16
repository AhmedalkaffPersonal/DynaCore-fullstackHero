using System.Threading.Tasks;

namespace DynaCore.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsVendorUsed(int brandId);
    }
}