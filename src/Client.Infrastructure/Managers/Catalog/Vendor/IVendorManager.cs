using DynaCore.Application.Features.Vendors.Commands.AddEdit;
using DynaCore.Application.Features.Vendors.Queries.GetAll;
using DynaCore.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynaCore.Client.Infrastructure.Managers.Catalog.Vendor
{
    public interface IVendorManager : IManager
    {
        Task<IResult<List<GetAllVendorsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditVendorCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}