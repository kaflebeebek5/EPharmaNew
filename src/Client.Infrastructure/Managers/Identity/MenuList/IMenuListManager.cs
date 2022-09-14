using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Identity.MenuList
{
    public interface IMenuListManager :IManager
    {
        Task<Result<List<MenuListResponse>>> GetAllAsync();

        Task<int> GetCountAsync();
        Task<Result<List<MenuListResponse>>> GetParentItemAsync();

        Task<Result<MenuListResponse>> GetByIdAsync(int id);

        Task<Result<string>> SaveAsync(MenuListRequest request);

        Task<Result<int>> DeleteAsync(int id);
    }
}
