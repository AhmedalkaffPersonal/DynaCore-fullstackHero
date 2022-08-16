using DynaCore.Application.Features.Products.Commands.AddEdit;
using DynaCore.Application.Features.Products.Queries.GetAllPaged;
using DynaCore.Application.Requests.Catalog;
using DynaCore.Shared.Wrapper;
using System.Threading.Tasks;

namespace DynaCore.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}