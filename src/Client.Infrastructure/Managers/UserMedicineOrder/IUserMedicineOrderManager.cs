using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.UserMedicineOrder
{
    public interface IUserMedicineOrderManager:IManager
    {
        Task<IResult<List<UserMedicineDetails>>> GetUserMedicine(string UserId);
        Task<IResult<MessageResponse>> SaveBillEntry(BillEntryRequestModel requestModel);
        Task<IResult<string>> UpdateStartingNumber();
    }
}
