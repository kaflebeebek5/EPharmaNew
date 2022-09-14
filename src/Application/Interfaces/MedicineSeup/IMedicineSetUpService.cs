using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Interfaces.MedicineSeup
{
    public interface IMedicineSetUpService
    {
        Task<Result<MessageResponse>> SaveMedicine(MedicineRequestModel Request);
        Task<Result<MessageResponse>> SavePrescription(UploadPrescription Request);
        Task<Result<MessageResponse>> SaveCartOrder(MultipleRequestModel Request);
        Task<Result<MessageResponse>> UserOTP(UploadPrescription Request);
        Task<Result<List<MedicineSetupResponseModel>>> GetAll();
        Task<Result<List<CategoryResponse>>> GetAllCategory();
        Task<Result<MessageResponse>> DeleteMedicine(int Id);
        Task<Result<List<MedicineSetupResponseModel>>> GetById(int? Id);
        Task<Result<List<MedicineSetupResponseModel>>> GetTop10();
        Task<Result<MedicineSetupResponseModel>> GetByProductId(int Id);
        Task<Result<OTPModel>> GetOTP(string PhoneNumber);
        Task<Result<List<UserOrderResponse>>> UserOrder();
        Task<Result<List<UserOrderResponse>>> CartOrder();
        Task<Result<MessageResponse>> SaveCart(UploadPrescription Request);
        Task<Result<GetCartCount>> GetCount();
        Task UpdateOTP(string PhoneNumber);
        Task CancleProduct(int id);
        Task CancleAllProduct();
    }
}
