using DynaCore.Application.Interfaces.Common;

namespace DynaCore.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}