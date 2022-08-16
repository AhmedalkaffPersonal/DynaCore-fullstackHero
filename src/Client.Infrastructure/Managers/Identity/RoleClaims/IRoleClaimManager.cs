using System.Collections.Generic;
using System.Threading.Tasks;
using DynaCore.Application.Requests.Identity;
using DynaCore.Application.Responses.Identity;
using DynaCore.Shared.Wrapper;

namespace DynaCore.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}