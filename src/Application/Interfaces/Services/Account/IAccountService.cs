using DynaCore.Application.Interfaces.Common;
using DynaCore.Application.Requests.Identity;
using DynaCore.Shared.Wrapper;
using System.Threading.Tasks;

namespace DynaCore.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}