using System.Collections.Generic;
using System.Threading.Tasks;
using DynaCore.Application.Features.DocumentTypes.Commands.AddEdit;
using DynaCore.Application.Features.DocumentTypes.Queries.GetAll;
using DynaCore.Shared.Wrapper;

namespace DynaCore.Client.Infrastructure.Managers.Misc.DocumentType
{
    public interface IDocumentTypeManager : IManager
    {
        Task<IResult<List<GetAllDocumentTypesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDocumentTypeCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}