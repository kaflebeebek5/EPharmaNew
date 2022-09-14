using EPharma.Application.Interfaces.Common;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Interfaces.Services.Identity
{
   public interface IMenuRoleService : IService
    {
        Task<Result<List<MenuRoleResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<List<MenuRoleResponse>>> GetByIdAsync(string id);
        Task<Result<List<MenuRoleResponse>>> GetUserMenuListAsync();

        Task<Result<string>> SaveAsync(List<MenuRoleRequest> request);

        Task<Result<string>> DeleteAsync(int id);
    }
}
