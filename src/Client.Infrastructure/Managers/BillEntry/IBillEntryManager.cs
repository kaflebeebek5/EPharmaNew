using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.BillEntry
{
    public interface IBillEntryManager:IManager
    {
        Task<IResult<MessageResponse>> SaveBillEntry(BillEntryRequestModel RequestModel);
        Task<IResult<List<BillEntryResponseModel>>> GetAll();
        Task<IResult<MessageResponse>> DeleteBill(int Id);
        Task<IResult<BillNumberberResponsecs>> GetBillNumber();
        Task<IResult<string>> UpdateStartingNumber();
        Task<IResult<List<UserResponseModel>>> GetAllUser();
    }
}
