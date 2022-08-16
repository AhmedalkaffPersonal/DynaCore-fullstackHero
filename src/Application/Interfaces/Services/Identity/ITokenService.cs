using DynaCore.Application.Interfaces.Common;
using DynaCore.Application.Requests.Identity;
using DynaCore.Application.Responses.Identity;
using DynaCore.Shared.Wrapper;
using System.Threading.Tasks;

namespace DynaCore.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}