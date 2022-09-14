using EPharma.Application.Interfaces.Common;

namespace EPharma.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}