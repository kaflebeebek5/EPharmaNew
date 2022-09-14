using EPharma.Application.Interfaces.DoctorSetup;
using EPharma.Application.Interfaces.Services;
using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Infrastructure.Repositories.Dapper;
using EPharma.Shared.Wrapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Services.DocorSetup
{
    public class DoctorSetupService: RepositoryBase, IDoctorSetupService
    {
        private readonly IConfiguration _configuration;
        private IUploadService _uploadService;
        private readonly ICurrentUserService _currentUserService;
        public DoctorSetupService(IConfiguration configuration, IUploadService uploadService, ICurrentUserService currentUserService) : base(configuration)
        {
            _configuration = configuration;
            _uploadService = uploadService;
            _currentUserService = currentUserService;
        }

        public async Task<Result<MessageResponse>> SaveDoctor(DoctorSetupRequestModel Request)
        {
            if (Request.Id == 0)
            {
                try
                {
                    if (Request.uploadReceipt != null)
                    {
                        var filePath = _uploadService.UploadAsync(Request.uploadReceipt);
                        Request.ImagePath = filePath;
                    }
                    //var Medicine = _mapper.Map<Medicine>(Request);
                    //await _DB.tblMedicine.AddAsync(Medicine);
                    //await _DB.SaveChangesAsync();
                    //return await Result<MessageResponse>.SuccessAsync("Medicine Saved");
                    var Response = await this.ExecuteAsync("Execute SP_DOCTOR @Flag='I',@Name=@Name,@Email=@Email,@Address=@Address," +
                        "@CreatedBy=@CreatedBy,@PhoneNumber=@PhoneNumber,@Specialist=@Specialist,@ImagePath=@ImagePath,@GenderId=@GenderId"
                        , new
                        {
                            Name = Request.Name,
                            Email = Request.Email,
                            Address = Request.Address,
                            Specialist = Request.Specialist,
                            PhoneNumber = Request.PhoneNumber,
                            ImagePath = Request.ImagePath,
                            GenderId=Request.GenderId,
                            CreatedBy = _currentUserService.UserId
                        });
                    return await Result<MessageResponse>.SuccessAsync("Doctor Saved Successfully");
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
                    if (Request.uploadReceipt.FileName != null)
                    {
                        var filePath = _uploadService.UploadAsync(Request.uploadReceipt);
                        Request.ImagePath = filePath;
                    }
                    var Response = await this.ExecuteAsync("Execute SP_DOCTOR @Flag='U',@Name=@Name,@Email=@Email,@Address=@Address," +
                       "@CreatedBy=@CreatedBy,@PhoneNumber=@PhoneNumber,@Specialist=@Specialist,@ImagePath=@ImagePath,@GenderId=@GenderId"
                       , new
                       {
                           Name = Request.Name,
                           Email = Request.Email,
                           Address = Request.Address,
                           Specialist = Request.Specialist,
                           PhoneNumber = Request.PhoneNumber,
                           ImagePath = Request.ImagePath,
                           GenderId=Request.GenderId,
                           CreatedBy = _currentUserService.UserId
                       });
                    return await Result<MessageResponse>.SuccessAsync("Doctor Updated SuccessFully");
                }
                catch (Exception ex)
                {
                    return await Result<MessageResponse>.SuccessAsync(ex.Message);
                }
            }
        }
        public async Task<Result<List<DoctoSetupResponse>>> GetAll()
        {
            try
            {
                var data = await this.GetQueryResultAsync<DoctoSetupResponse>("Execute SP_DOCTOR @Flag='G'");
                return await Result<List<DoctoSetupResponse>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<DoctoSetupResponse>>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<MessageResponse>> DeleteDoctor(int Id)
        {
            try
            {
                var Message = await this.ExecuteAsync("EXECUTE SP_DOCTOR @Flag='D',@Id=@Id",
                        new
                        {
                            Id = Id
                        });
                return await Result<MessageResponse>.SuccessAsync("Doctor Deleted Successfully");
            }
            catch (Exception ex)
            {
                return await Result<MessageResponse>.FailAsync(ex.Message);
            }
        }
    }
}
