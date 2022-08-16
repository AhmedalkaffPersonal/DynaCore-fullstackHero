using DynaCore.Application.Responses.Audit;
using DynaCore.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynaCore.Application.Interfaces.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);

        Task<IResult<string>> ExportToExcelAsync(string userId, string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}