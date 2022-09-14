using EPharma.Application.Interfaces.Services;
using EPharma.Application.Interfaces.UserMedicine;
using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Infrastructure.Repositories.Dapper;
using EPharma.Shared.Wrapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Services.UserMedicine
{
    public class UserMedicineService : RepositoryBase, IUserMedicineService
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        public UserMedicineService(IConfiguration configuration, ICurrentUserService currentUserService) : base(configuration)
        {
            _configuration = configuration;
            _currentUserService = currentUserService;
        }
        public async Task<Result<MessageResponse>> SaveUserMedicine(BillEntryRequestModel userMedicineRequestModel)
        {
            try
            {
                var UserMedicineJson = JsonSerializer.Serialize(userMedicineRequestModel.MedicineList);
                var Message = await this.ExecuteAsync("Execute SP_USER_MEDICINE @Flag='I',@UserMedicineJson=@UserMedicineJson,@BillNumber=@BillNumber,@DoctorId=@DoctorId,@UserId=@UserId",
                    new
                    {
                        UserId=userMedicineRequestModel.UserId,
                        UserMedicineJson=UserMedicineJson,
                        BillNumber=userMedicineRequestModel.BillNumber,
                        DoctorId=_currentUserService.UserId,
                    });
                return await Result<MessageResponse>.SuccessAsync(Message.ToString());

            }
            catch (Exception ex)
            {
                return await Result<MessageResponse>.SuccessAsync(ex.Message);
            }
        }
        public async Task<Result<List<UserMedicineDetails>>> GetUserMedicine(string UserId)
        {
            try
            {
                var Data = await this.GetQueryResultAsync<UserMedicineDetails>("EXECUTE SP_USER_MEDICINE @Flag='G',@UserId=@UserId",
                    new
                    {
                        UserId = UserId
                    });
                return await Result<List<UserMedicineDetails>>.SuccessAsync(Data);
            }
            catch(Exception Ex)
            {
                return await Result<List<UserMedicineDetails>>.FailAsync(Ex.Message);
            }
        }
    }
}
