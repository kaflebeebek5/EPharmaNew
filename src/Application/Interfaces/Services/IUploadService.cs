using EPharma.Application.Requests;

namespace EPharma.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}