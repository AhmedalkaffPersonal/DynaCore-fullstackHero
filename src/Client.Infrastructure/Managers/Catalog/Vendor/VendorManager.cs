using DynaCore.Application.Features.Vendors.Queries.GetAll;
using DynaCore.Client.Infrastructure.Extensions;
using DynaCore.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DynaCore.Application.Features.Vendors.Commands.AddEdit;

namespace DynaCore.Client.Infrastructure.Managers.Catalog.Vendor
{
    public class VendorManager : IVendorManager
    {
        private readonly HttpClient _httpClient;

        public VendorManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.VendorsEndpoints.Export
                : Routes.VendorsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.VendorsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllVendorsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.VendorsEndpoints.GetAll);
            return await response.ToResult<List<GetAllVendorsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditVendorCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.VendorsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}