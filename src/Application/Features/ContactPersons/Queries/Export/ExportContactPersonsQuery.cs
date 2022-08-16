using DynaCore.Shared.Wrapper;
using MediatR;

namespace DynaCore.Application.Features.ContactPersons.Queries.Export
{
    public class ExportContactPersonsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportContactPersonsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }
}
