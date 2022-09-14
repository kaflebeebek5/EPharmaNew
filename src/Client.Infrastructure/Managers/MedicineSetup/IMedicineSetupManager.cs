using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.MedicineSetup
{
    public interface IMedicineSetupManager:IManager
    {
        Task<IResult<MessageResponse>> SaveMedicine(MedicineRequestModel Request);
        Task<IResult<MessageResponse>> SavePrescription(UploadPrescription Request);
        Task<IResult<MessageResponse>> SaveCartOrder(MultipleRequestModel Request);
        Task<IResult<MessageResponse>> SaveCart(UploadPrescription Request);
        Task<IResult<MessageResponse>> SaveOTP(UploadPrescription Request);
        Task<IResult<List<MedicineSetupResponseModel>>> GetAll();
        Task<IResult<List<CategoryResponse>>> GetAllCategory();
        Task<IResult<MessageResponse>> DeleteMedicine(int Id);
        Task<IResult<List<MedicineSetupResponseModel>>> GetById(int? Id);
        Task<IResult<MedicineSetupResponseModel>> GetByProductId(int Id);
        Task<IResult<OTPModel>> GetOtp(string PhoneNumber);
        Task<IResult<GetCartCount>> GetCount();
        Task UpdateOTP(string PhoneNumber);
        Task<IResult<List<UserOrderResponse>>> GetAllUserOrder();
        Task<IResult<List<UserOrderResponse>>> GetAllcartOrder();
        Task CancleProduct(int Id);
        Task CancleAllProduct();
    }
}
