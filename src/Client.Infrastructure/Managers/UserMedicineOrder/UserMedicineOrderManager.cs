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

namespace EPharma.Client.Infrastructure.Managers.UserMedicineOrder
{
    public class UserMedicineOrderManager:IUserMedicineOrderManager
    {
        private readonly HttpClient _httpClient;

        public UserMedicineOrderManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
     public async Task<IResult<List<UserMedicineDetails>>> GetUserMedicine(string UserId)
     {
            var Response = await _httpClient.GetAsync(String.Concat("api/UserMedicine/GetAllById?UserId=",UserId));
            return await Response.ToResult<List<UserMedicineDetails>>();
     }
        public async Task<IResult<MessageResponse>> SaveBillEntry(BillEntryRequestModel requestModel)
        {
            var Response=await _httpClient.PostAsJsonAsync("api/UserMedicine", requestModel);
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<string>> UpdateStartingNumber()
        {
            var Response = await _httpClient.GetAsync("api/BillEntry/UpdateStartingNumber");
            return await Response.ToResult<string>();
        }
    }
}
