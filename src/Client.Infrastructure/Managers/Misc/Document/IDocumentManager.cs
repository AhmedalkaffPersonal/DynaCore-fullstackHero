using DynaCore.Application.Features.Documents.Commands.AddEdit;
using DynaCore.Application.Features.Documents.Queries.GetAll;
using DynaCore.Application.Requests.Documents;
using DynaCore.Shared.Wrapper;
using System.Threading.Tasks;
using DynaCore.Application.Features.Documents.Queries.GetById;

namespace DynaCore.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}