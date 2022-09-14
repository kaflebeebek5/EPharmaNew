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

namespace EPharma.Client.Infrastructure.Managers.Identity.MenuList
{
    public class MenuListManager : IMenuListManager
    {
        private readonly HttpClient _httpClient;

        public MenuListManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<MenuListResponse>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{Routes.MenuListEndpoints.GetById}/{id}");
            return (Result<MenuListResponse>)await response.ToResult<List<MenuListResponse>>();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<MenuListResponse>>> GetParentItemAsync()
        {
            var response = await _httpClient.GetAsync($"{Routes.MenuListEndpoints.GetParentItem}/ParentItem");
            return (Result<List<MenuListResponse>>)await response.ToResult<List<MenuListResponse>>();
        }

        public async Task<Result<string>> SaveAsync(MenuListRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.MenuListEndpoints.Save, request);
            return (Result<string>)await response.ToResult<string>();
        }

        async Task<Result<int>> IMenuListManager.DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.MenuListEndpoints.Delete}/{id}");
            return (Result<int>)await response.ToResult<int>();
        }

        async Task<Result<List<MenuListResponse>>> IMenuListManager.GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.MenuListEndpoints.GetAll);
            return (Result<List<MenuListResponse>>)await response.ToResult<List<MenuListResponse>>();
        }

    }
}
