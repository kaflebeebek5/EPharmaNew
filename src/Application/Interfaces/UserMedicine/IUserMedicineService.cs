using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Interfaces.UserMedicine
{
    public interface IUserMedicineService
    {
        Task<Result<MessageResponse>> SaveUserMedicine(BillEntryRequestModel userMedicineRequestModel);
        Task<Result<List<UserMedicineDetails>>> GetUserMedicine(string UserId);
    }
}
