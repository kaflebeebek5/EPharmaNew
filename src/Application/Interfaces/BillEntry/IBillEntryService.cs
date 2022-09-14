using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Interfaces.BillEntry
{
    public interface IBillEntryService
    {
        Task<Result<MessageResponse>> SaveBillEntry(BillEntryRequestModel RequestModel);
        Task<Result<List<BillEntryResponseModel>>> GetAll();
        Task<Result<MessageResponse>> DeleteBill(int Id);
        Task<Result<BillNumberberResponsecs>> GetBillNumber();
        Task<Result<string>> UpdateStartingNumber();
        Task<Result<List<UserResponseModel>>> GetAllUser();
    }
}
