using EPharma.Application.Features.Genders.Commands.AddEdit;
using EPharma.Application.Features.Genders.Queries.GetAll;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.Gender
{
    public class GenderManager: IGenderManager
    {
        private readonly HttpClient _httpClient;

        public GenderManager(HttpClient httpClient)
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
            var response = await _httpClient.DeleteAsync($"{Routes.GendersEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllGendersResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.GendersEndpoints.GetAll);
            return await response.ToResult<List<GetAllGendersResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditGenderCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.GendersEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}
