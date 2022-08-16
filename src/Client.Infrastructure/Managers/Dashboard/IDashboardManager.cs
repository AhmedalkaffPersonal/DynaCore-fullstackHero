using DynaCore.Shared.Wrapper;
using System.Threading.Tasks;
using DynaCore.Application.Features.Dashboards.Queries.GetData;

namespace DynaCore.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}