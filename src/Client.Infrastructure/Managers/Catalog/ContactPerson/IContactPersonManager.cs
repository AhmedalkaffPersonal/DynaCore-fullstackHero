using DynaCore.Application.Features.ContactPersons.Commands.AddEdit;
using DynaCore.Application.Features.ContactPersons.Queries.GetAll;
using DynaCore.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynaCore.Client.Infrastructure.Managers.Catalog.ContactPerson
{
    public interface IContactPersonManager : IManager
    {
        Task<IResult<List<GetAllContactPersonsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditContactPersonCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}