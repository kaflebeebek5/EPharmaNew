using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Identity.MenuRole
{
    public interface IMenuRoleManager :IManager
    {
        Task<Result<List<MenuRoleResponse>>> GetAllAsync();

        Task<Result<List<MenuRoleResponse>>> GetByIdAsync(string id);
        Task<Result<List<MenuRoleResponse>>> GetUserMenuListAsync();

        Task<Result<string>> SaveAsync(List<MenuRoleRequest> request);

        Task<Result<string>> DeleteAsync(int id);
    }
}
