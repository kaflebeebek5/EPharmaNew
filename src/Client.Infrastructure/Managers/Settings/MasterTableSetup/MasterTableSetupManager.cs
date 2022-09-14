using EPharma.Application.Features.MasterTableSetup.Commands.AddEdit;
using EPharma.Application.Features.MasterTableSetup.Queries.GetAll;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.MasterTableSetup
{
    public class MasterTableSetupManager : IMasterTableSetupManager
    {
        private readonly HttpClient _httpClient;

        public MasterTableSetupManager(HttpClient httpClient)
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

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.MasterTableSetupEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllMasterTableSetupResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.MasterTableSetupEndpoints.GetAll);
            return await response.ToResult<List<GetAllMasterTableSetupResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditMasterTableSetupCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.MasterTableSetupEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}
