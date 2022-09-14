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

namespace EPharma.Client.Infrastructure.Managers.BillEntry
{
    public class BillEntryManager: IBillEntryManager
    {
        private readonly HttpClient _httpClient;

        public BillEntryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<MessageResponse>> SaveBillEntry(BillEntryRequestModel Request)
        {
            var Response = await _httpClient.PostAsJsonAsync("api/BillEntry", Request);
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<List<BillEntryResponseModel>>> GetAll()
        {
            var Response = await _httpClient.GetAsync("api/BillEntry/GetAll");
            return await Response.ToResult<List<BillEntryResponseModel>>();
        }
        public async Task<IResult<MessageResponse>> DeleteBill(int Id)
        {
            var Response = await _httpClient.GetAsync(String.Concat("api/BillEntry/Delete/?Id=", Id));
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<BillNumberberResponsecs>> GetBillNumber()
        {
            var Response = await _httpClient.GetAsync("api/BillEntry/BillNumber");
            return await Response.ToResult<BillNumberberResponsecs>();
        }
        public async Task<IResult<string>> UpdateStartingNumber()
        {
            var Response = await _httpClient.GetAsync("api/BillEntry/UpdateStartingNumber");
            return await Response.ToResult<string>();
        }
        public async Task<IResult<List<UserResponseModel>>> GetAllUser()
        {
            var Response = await _httpClient.GetAsync("api/BillEntry/GetAllUser");
            return await Response.ToResult<List<UserResponseModel>>();
        }

    }
}
