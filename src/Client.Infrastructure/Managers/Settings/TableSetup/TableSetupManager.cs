using EPharma.Application.Features.TableSetup.Commands.AddEdit;
using EPharma.Application.Features.TableSetup.Queries.GetAll;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.TableSetup
{
    public class TableSetupManager : ITableSetupManager
    {
        private readonly HttpClient _httpClient;

        public TableSetupManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.GendersEndpoints.Export
                : Routes.GendersEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id, string tableName)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.TableSetupEndpoints.Delete}/{id}/{tableName}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllTableSetupResponse>>> GetAllAsync(string tableName)
        {
            var response = await _httpClient.GetAsync($"{Routes.TableSetupEndpoints.GetAll}/{tableName}");
            return await response.ToResult<List<GetAllTableSetupResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditTableSetupCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.TableSetupEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}
