using EPharma.Application.Features.Province.Queries.GetById;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.District
{
    public class DistrictManager : IDistrictManager
    {
        private readonly HttpClient _httpClient;

        public DistrictManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<List<GetDistrictByIdResponse>>> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"{Routes.DistrictEndpoints.GetById}/{id}");
            return (Result<List<GetDistrictByIdResponse>>)await response.ToResult<List<GetDistrictByIdResponse>>();
        }
    }
}
