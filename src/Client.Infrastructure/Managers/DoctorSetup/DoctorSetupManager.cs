using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Client.Infrastructure.Extensions;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.DoctorSetup
{
    public class DoctorSetupManager:IDoctorSetupManager
    {
        private readonly HttpClient _httpClient;

        public DoctorSetupManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<MessageResponse>> SaveDoctor(DoctorSetupRequestModel Request)
        {
            var Response = await _httpClient.PostAsJsonAsync("api/Doctor", Request);
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<List<DoctoSetupResponse>>> GetAll()
        {
            var Response = await _httpClient.GetAsync("api/Doctor/GetAll");
            return await Response.ToResult<List<DoctoSetupResponse>>();
        }
        public async Task<IResult<MessageResponse>> DeleteDoctor(int Id)
        {
            var Response = await _httpClient.GetAsync(String.Concat("api/Doctor/Delete/?Id=",Id));
            return await Response.ToResult<MessageResponse>();
        }

    }
}
