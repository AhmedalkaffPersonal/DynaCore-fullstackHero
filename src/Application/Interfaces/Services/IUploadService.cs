using DynaCore.Application.Requests;

namespace DynaCore.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}