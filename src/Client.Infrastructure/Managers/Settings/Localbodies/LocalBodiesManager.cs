using EPharma.Application.Features.Vdc.Queries.GetById;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.Localbodies
{
    public class LocalBodiesManager : ILocalBodiesManager
    {

        private readonly HttpClient _httpClient;

        public LocalBodiesManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<List<GetVdcByIdResponse>>> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"{Routes.LocalbodiesEndpoints.GetById}/{id}");
            return (Result<List<GetVdcByIdResponse>>)await response.ToResult<List<GetVdcByIdResponse>>();
        }
    }
}
