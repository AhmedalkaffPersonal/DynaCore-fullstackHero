using System.Collections.Generic;
using System.Threading.Tasks;
using DynaCore.Application.Interfaces.Common;
using DynaCore.Application.Requests.Identity;
using DynaCore.Application.Responses.Identity;
using DynaCore.Shared.Wrapper;

namespace DynaCore.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}