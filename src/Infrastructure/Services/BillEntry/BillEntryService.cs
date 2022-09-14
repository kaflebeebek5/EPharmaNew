using EPharma.Application.Interfaces.BillEntry;
using EPharma.Application.Interfaces.Services;
using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Infrastructure.Contexts;
using EPharma.Infrastructure.Repositories.Dapper;
using EPharma.Shared.Wrapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Services.BillEntry
{
    public class BillEntryService : RepositoryBase, IBillEntryService
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        private readonly EPharmaContext _DB;
        public BillEntryService(IConfiguration configuration, ICurrentUserService currentUserService,EPharmaContext ePharmaContext) : base(configuration)
        {
            _configuration = configuration;
            _currentUserService = currentUserService;
            _DB=ePharmaContext;
        }

        public async Task<Result<MessageResponse>> SaveBillEntry(BillEntryRequestModel RequestModel)
        {
            if (RequestModel.Id == 0)
            {
                try
                {
                    var Response = await this.ExecuteAsync("Execute SP_BILL_ENTRY @Flag='I',@BillNumber=@BillNumber,@UserId=@UserId,@DoctorId=@DoctorId" 
                        , new
                        {
                           BillNumber= RequestModel.BillNumber,
                           UserId= RequestModel.UserId,
                           DoctorId= RequestModel.DoctorId,
                        });
                    return await Result<MessageResponse>.SuccessAsync("Bill Saved Successfully");
                   
                }
                catch (Exception ex)
                {
                    return await Result<MessageResponse>.FailAsync(ex.Message);
                }

            }
            else
            {
                try
                {
                    var Response = await this.ExecuteAsync("Execute SP_BILL_ENTRY @Flag='U',@UserId=@UserId,@DoctorId=@DoctorId,"
                       , new
                       {
                           UserId = RequestModel.UserId,
                           DoctorId = RequestModel.DoctorId,
                       });
                    return await Result<MessageResponse>.SuccessAsync("Bill Updated SuccessFully");
                }
                catch (Exception ex)
                {
                    return await Result<MessageResponse>.SuccessAsync(ex.Message);
                }
            }
        }
        public async Task<Result<List<BillEntryResponseModel>>> GetAll()
        {
            try
            {
                var data = await this.GetQueryResultAsync<BillEntryResponseModel>("Execute SP_BILL_ENTRY @Flag='G'");
                return await Result<List<BillEntryResponseModel>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<BillEntryResponseModel>>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<MessageResponse>> DeleteBill(int Id)
        {
            try
            {
                var Message = await this.ExecuteAsync("EXECUTE SP_BILL_ENTRY @Flag='D',@Id=@Id",
                        new
                        {
                            Id = Id
                        });
                return await Result<MessageResponse>.SuccessAsync("Bill Deleted Successfully");
            }
            catch (Exception ex)
            {
                return await Result<MessageResponse>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<BillNumberberResponsecs>> GetBillNumber()
        {
            var BillSetup = _DB.tblBillSetup.FirstOrDefault();
            var Billnumber = BillSetup.Prefix + (BillSetup.StartingNumber).ToString("D" + BillSetup.NoOfDigit) + BillSetup.Suffix;
            return await Result<BillNumberberResponsecs>.SuccessAsync(Billnumber);
        }
        public async Task<Result<string>> UpdateStartingNumber()
        {
            var BillSetup = _DB.tblBillSetup.FirstOrDefault();
            BillSetup.StartingNumber = BillSetup.StartingNumber + 1;
            _DB.tblBillSetup.Update(BillSetup);
            _DB.SaveChanges();
            return await Result<string>.SuccessAsync();
        }
        public async Task<Result<List<UserResponseModel>>> GetAllUser()
        {
            try
            {
                var Message = await this.GetQueryResultAsync<UserResponseModel>("EXECUTE SP_BILL_ENTRY @Flag='Z'");
                return await Result<List<UserResponseModel>>.SuccessAsync(Message);
            }
            catch (Exception ex)
            {
                return await Result<List<UserResponseModel>>.SuccessAsync(ex.Message);
            }
        }
    }
}
