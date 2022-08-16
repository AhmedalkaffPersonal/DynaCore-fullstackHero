using DynaCore.Application.Features.ContactPersons.Commands.AddEdit;
using DynaCore.Application.Features.ContactPersons.Queries.GetAll;
using DynaCore.Client.Infrastructure.Extensions;
using DynaCore.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DynaCore.Client.Infrastructure.Managers.Catalog.ContactPerson
{
    public class ContactPersonManager : IContactPersonManager
    {
        private readonly HttpClient _httpClient;

        public ContactPersonManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.ContactPersonsEndpoints.Export
                : Routes.ContactPersonsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.ContactPersonsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllContactPersonsResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.ContactPersonsEndpoints.GetAll);
            return await response.ToResult<List<GetAllContactPersonsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditContactPersonCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ContactPersonsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}