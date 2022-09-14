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

namespace EPharma.Client.Infrastructure.Managers.MedicineSetup
{
    public class MedicineSetupManager : IMedicineSetupManager
    {
        private readonly HttpClient _httpClient;

        public MedicineSetupManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<MessageResponse>> SaveMedicine(MedicineRequestModel Request)
        {
            var Response = await _httpClient.PostAsJsonAsync("api/Medicine", Request);
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<List<MedicineSetupResponseModel>>> GetAll()
        {
            var Response = await _httpClient.GetAsync("api/Medicine/GetAll");
            return await Response.ToResult<List<MedicineSetupResponseModel>>();
        }
        public async Task<IResult<List<CategoryResponse>>> GetAllCategory()
        {
            var Response = await _httpClient.GetAsync("api/Medicine/GetAllCategory");
            return await Response.ToResult<List<CategoryResponse>>();
        }
        public async Task<IResult<MessageResponse>> DeleteMedicine(int Id)
        {
            var Response = await _httpClient.GetAsync(String.Concat("api/Medicine/Delete/?Id=",Id));
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<List<MedicineSetupResponseModel>>> GetById(int? Id)
        {
            var Response = await _httpClient.GetAsync(String.Concat("api/Medicine/GetById/?Id=", Id));
            return await Response.ToResult<List<MedicineSetupResponseModel>>();
        }
        public async Task<IResult<MedicineSetupResponseModel>>GetByProductId(int Id)
        {
            var Response = await _httpClient.GetAsync(String.Concat("api/Medicine/GetByProductId/?Id=", Id));
            return await Response.ToResult<MedicineSetupResponseModel>();
        }
        public async Task<IResult<MessageResponse>> SavePrescription(UploadPrescription Request)
        {
            var Response = await _httpClient.PostAsJsonAsync("api/Medicine/UploadPrescription", Request);
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<MessageResponse>> SaveCartOrder(MultipleRequestModel Request)
        {
            var Response = await _httpClient.PostAsJsonAsync("api/Medicine/SaveCartOrder", Request);
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<MessageResponse>> SaveCart(UploadPrescription Request)
        {
            var Response = await _httpClient.PostAsJsonAsync("api/Medicine/SaveCart", Request);
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<MessageResponse>> SaveOTP(UploadPrescription Request)
        {
            var Response = await _httpClient.PostAsJsonAsync("api/Medicine/OTP", Request);
            return await Response.ToResult<MessageResponse>();
        }
        public async Task<IResult<OTPModel>> GetOtp(string PhoneNumber)
        {
            var Response = await _httpClient.GetAsync(String.Concat("api/Medicine/GetUserOTP/?PhoneNumber=", PhoneNumber));
            return await Response.ToResult<OTPModel>();
        }
        public async Task<IResult<GetCartCount>> GetCount()
        {
            var Response = await _httpClient.GetAsync(String.Concat("api/Medicine/GetCount"));
            return await Response.ToResult<GetCartCount>();
        }
        public async Task UpdateOTP(string PhoneNumber)
        {
            await _httpClient.GetAsync("api/Medicine/UpdateOTP");
        }
        public async Task<IResult<List<UserOrderResponse>>> GetAllUserOrder()
        {
            var Response = await _httpClient.GetAsync("api/Medicine/GetAllUserOrder");
            return await Response.ToResult<List<UserOrderResponse>>();
        }
        public async Task<IResult<List<UserOrderResponse>>> GetAllcartOrder()
        {
            var Response = await _httpClient.GetAsync("api/Medicine/GetCartOrder");
            return await Response.ToResult<List<UserOrderResponse>>();
        }
       public async Task CancleProduct(int Id)
       {
            await _httpClient.GetAsync(String.Concat("api/Medicine/Cancel/?Id=",Id));
       }
        public async Task CancleAllProduct()
        {
            await _httpClient.GetAsync("api/Medicine/CancelAll");
        }
    }
}
