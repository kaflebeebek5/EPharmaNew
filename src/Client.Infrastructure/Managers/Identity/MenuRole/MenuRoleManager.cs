using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Identity.MenuRole
{
    public class MenuRoleManager : IMenuRoleManager
    {
        private readonly HttpClient _httpClient;

        public MenuRoleManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<List<MenuRoleResponse>>> GetUserMenuListAsync()
        {
            var response = await _httpClient.GetAsync($"{Routes.MenuRoleEndpoints.GetUserMenuList}");
            return (Result<List<MenuRoleResponse>>)await response.ToResult<List<MenuRoleResponse>>();
        }

        public async Task<Result<List<MenuRoleResponse>>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{Routes.MenuRoleEndpoints.GetById}/{id}");
            return (Result<List<MenuRoleResponse>>)await response.ToResult<List<MenuRoleResponse>>();
        }

        async Task<Result<string>> IMenuRoleManager.SaveAsync(List<MenuRoleRequest> request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.MenuRoleEndpoints.Save, request);
            return (Result<string>)await response.ToResult<string>();
        }

        async Task<Result<string>> IMenuRoleManager.DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.MenuRoleEndpoints.Delete}/{id}");
            return (Result<string>)await response.ToResult<int>();
        }

        async Task<Result<List<MenuRoleResponse>>> IMenuRoleManager.GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.MenuRoleEndpoints.GetAll);
            return (Result<List<MenuRoleResponse>>)await response.ToResult<List<MenuRoleResponse>>();
        }
    }
}
