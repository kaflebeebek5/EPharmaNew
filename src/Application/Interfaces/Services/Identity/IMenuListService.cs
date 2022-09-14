using EPharma.Application.Interfaces.Common;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPharma.Application.Interfaces.Services.Identity
{
    public interface IMenuListService : IService
    {
        Task<Result<List<MenuListResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<List<MenuListResponse>>> GetParentItemAsync();

        Task<Result<MenuListResponse>> GetByIdAsync(int id);

        Task<Result<string>> SaveAsync(MenuListRequest request);

        Task<Result<int>> DeleteAsync(int id);

        //Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId);

        //Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}
