using EPharma.Application.Features.StaticVariable.Queries.GetAll;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.StaticVariable
{
    public class StaticVariableManager: IStaticVariableManager
    {
        private readonly HttpClient _httpClient;

        public StaticVariableManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<GetAllStaticVariableResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.StaticVariableEndpoints.GetAll);
            return await response.ToResult<List<GetAllStaticVariableResponse>>();
        }
        public async Task<IResult<GetAllStaticVariableResponse>> GetByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync(Routes.StaticVariableEndpoints.GetByName(name));
            return await response.ToResult<GetAllStaticVariableResponse>();
        }
    }
}
