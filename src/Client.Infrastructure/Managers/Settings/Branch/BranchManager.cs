using EPharma.Application.Features.Branch.Commands.AddEdit;
using EPharma.Application.Features.Branch.Queries.GetAll;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.Branch
{
    public class BranchManager: IBranchManager
    {
        private readonly HttpClient _httpClient;

        public BranchManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.BranchEndpoints.Export
                : Routes.BranchEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.BranchEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllBranchResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.BranchEndpoints.GetAll);
            return await response.ToResult<List<GetAllBranchResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditBranchCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.BranchEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}
