using AutoMapper;
using EPharma.Application.Interfaces.MedicineSeup;
using EPharma.Application.Interfaces.Services;
using EPharma.Application.Requests;
using EPharma.Application.Responses;
using EPharma.Domain.Entities.Settings;
using EPharma.Infrastructure.Contexts;
using EPharma.Infrastructure.Repositories.Dapper;
using EPharma.Shared.Wrapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Services.MedicineSetUp
{
    public class MedicineSetUpService : RepositoryBase, IMedicineSetUpService
    {
        private readonly IConfiguration _configuration;
        private IUploadService _uploadService;
        private readonly ICurrentUserService _currentUserService;
        public MedicineSetUpService(IConfiguration configuration, IUploadService uploadService, ICurrentUserService currentUserService) : base(configuration)
        {
            _configuration = configuration;
            _uploadService = uploadService;
            _currentUserService = currentUserService;
        }
        public async Task<Result<MessageResponse>> SaveMedicine(MedicineRequestModel Request)
        {
            if (Request.Id == 0)
            {
                try
                {
                    if (Request.uploadMedicine != null)
                    {
                        var filePath = _uploadService.UploadAsync(Request.uploadMedicine);
                        Request.ImagePath = filePath;
                    }
                    //var Medicine = _mapper.Map<Medicine>(Request);
                    //await _DB.tblMedicine.AddAsync(Medicine);
                    //await _DB.SaveChangesAsync();
                    //return await Result<MessageResponse>.SuccessAsync("Medicine Saved");
                    var Response = await this.ExecuteAsync("Execute SP_MEDICINE @Flag='I',@Name=@Name,@Description=@Description,@QuantityAvailable=@QuantityAvailable," +
                        "@CreatedBy=@CreatedBy,@ManuFactureDate=@ManuFactureDate,@ExpiryDate=@ExpiryDate,@ImagePath=@ImagePath,@CategoryId=@CategoryId,@SalePrice=@Saleprice,@Buyprice=@BuyPrice,@ManufacturerName=@Manufacturer,@Unit=@Unit,@IsActive=@IsActive,@SubCategory=@SubCategory"
                        , new
                        {
                            Name = Request.Name,
                            QuantityAvailable = Request.QuantityAvailable,
                            Description = Request.Description,
                            ManuFactureDate = Request.ManufactureDate,
                            ExpiryDate = Request.ExpiryDate,
                            ImagePath = Request.ImagePath,
                            CreatedBy = _currentUserService.UserId,
                            SalePrice = Request.SalePrice,
                            BuyPrice = Request.BuyPrice,
                            Manufacturer = Request.Manufacturer,
                            IsActive = Request.IsActive,
                            CategoryId = Request.CategoryId,
                            Unit = Request.Unit,
                            SubCategory= Request.SubCategory,
                        });
                    return await Result<MessageResponse>.SuccessAsync("Medicine Saved");
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
                    if (Request.uploadMedicine.FileName != null)
                    {
                        var filePath = _uploadService.UploadAsync(Request.uploadMedicine);
                        Request.ImagePath = filePath;
                    }
                    var Response = await this.ExecuteAsync("EXECUTE SP_MEDICINE @Flag='U',@Id=@Id,@Name=@Name,@Description=@Description,@QuantityAvailable=@QuantityAvailable," +
                        "@ManuFactureDate=@ManuFactureDate,@ExpiryDate=@ExpiryDate,@ImagePath=@ImagePath,@CategoryId=@CategoryId,@SalePrice=@Saleprice,@Buyprice=@BuyPrice," +
                        "@ManufacturerName=@Manufacturer,@Unit=@Unit,@IsActive=@IsActive",
                       new
                       {
                           Id = Request.Id,
                           Name = Request.Name,
                           QuantityAvailable = Request.QuantityAvailable,
                           Description = Request.Description,
                           ManuFactureDate = Request.ManufactureDate,
                           ExpiryDate = Request.ExpiryDate,
                           ImagePath = Request.ImagePath,
                           SalePrice = Request.SalePrice,
                           BuyPrice = Request.BuyPrice,
                           Manufacturer = Request.Manufacturer,
                           IsActive = Request.IsActive,
                           CategoryId = Request.CategoryId,
                           Unit = Request.Unit,
                       });
                    return await Result<MessageResponse>.SuccessAsync("Medicine Updated");
                }
                catch (Exception ex)
                {
                    return await Result<MessageResponse>.SuccessAsync(ex.Message);
                }
            }
        }
        public async Task<Result<List<MedicineSetupResponseModel>>> GetAll()
        {
            try
            {
                var data = await this.GetQueryResultAsync<MedicineSetupResponseModel>("Execute SP_MEDICINE @Flag='G'");
                return await Result<List<MedicineSetupResponseModel>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<MedicineSetupResponseModel>>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<List<CategoryResponse>>> GetAllCategory()
        {
            try
            {
                var data = await this.GetQueryResultAsync<CategoryResponse>("Execute SP_MEDICINE @Flag='E'");
                return await Result<List<CategoryResponse>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<CategoryResponse>>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<MessageResponse>> DeleteMedicine(int Id)
        {
            try
            {
                var Message = await this.ExecuteAsync("EXECUTE SP_MEDICINE @Flag='D',@Id=@Id",
                        new
                        {
                            Id = Id
                        });
                return await Result<MessageResponse>.SuccessAsync("Medicine Deleted Successfully");
            }
            catch (Exception ex)
            {
                return await Result<MessageResponse>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<List<MedicineSetupResponseModel>>> GetById(int? Id)
        {
            try
            {
                var data = await this.GetQueryResultAsync<MedicineSetupResponseModel>("Execute SP_MEDICINE @Flag='H',@Id=@Id",
                    new
                    {
                        Id = Id
                    });
                return await Result<List<MedicineSetupResponseModel>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<MedicineSetupResponseModel>>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<MedicineSetupResponseModel>> GetByProductId(int Id)
        {
            try
            {
                var data = await this.GetQueryFirstOrDefaultResultAsync<MedicineSetupResponseModel>("Execute SP_MEDICINE @Flag='K',@Id=@Id",
                    new
                    {
                        Id = Id
                    });
                return await Result<MedicineSetupResponseModel>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<MedicineSetupResponseModel>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<List<MedicineSetupResponseModel>>> GetTop10()
        {
            try
            {
                var data = await this.GetQueryResultAsync<MedicineSetupResponseModel>("Execute SP_MEDICINE @Flag='J'");
                return await Result<List<MedicineSetupResponseModel>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<MedicineSetupResponseModel>>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<MessageResponse>> SavePrescription(UploadPrescription Request)
        {
            try
            {
                if (Request.uploadMedicine != null)
                {
                    var filePath = _uploadService.UploadAsync(Request.uploadMedicine);
                    Request.ImagePath = filePath;
                }
                //var Medicine = _mapper.Map<Medicine>(Request);
                //await _DB.tblMedicine.AddAsync(Medicine);
                //await _DB.SaveChangesAsync();
                //return await Result<MessageResponse>.SuccessAsync("Medicine Saved");
                var Response = await this.ExecuteAsync("Execute SP_MEDICINE @Flag='L',@PhoneNumber=@PhoneNumber,@BillingAddress=@BillingAddress," +
                    "@CreatedBy=@CreatedBy,@ImagePath=@ImagePath,@Remarks=@Remarks"
                    , new
                    {
                        PhoneNumber = Request.PhoneNumber,
                        BillingAddress = Request.BillingAddress,
                        CreatedBy = _currentUserService.UserId,
                        ImagePath = Request.ImagePath,
                        Remarks=Request.Remarks,
                    });
                return await Result<MessageResponse>.SuccessAsync("Prescription Requested Successfully");
            }
            catch (Exception ex)
            {
                return await Result<MessageResponse>.FailAsync(ex.Message);
            }

        }
        public async Task<Result<MessageResponse>> SaveCartOrder(MultipleRequestModel Request)
        {
            try
            {
                var Jsondata = JsonConvert.SerializeObject(Request.MultipleReq);
                var Response = await this.ExecuteAsync("Execute SP_MEDICINE @Flag='V',@PhoneNumber=@PhoneNumber,@BillingAddress=@BillingAddress," +
                    "@CreatedBy=@CreatedBy,@Remarks=@Remarks,@JsonFile=@JsonFile"
                    , new
                    {
                        PhoneNumber = Request.PhoneNumber,
                        BillingAddress = Request.BillingAddress,
                        CreatedBy = _currentUserService.UserId,
                        Remarks = Request.Remarks,
                        JsonFile=Jsondata,
                    });
                return await Result<MessageResponse>.SuccessAsync("Order Requested Successfully");
            }
            catch (Exception ex)
            {
                return await Result<MessageResponse>.FailAsync(ex.Message);
            }

        }
        public async Task<Result<MessageResponse>> UserOTP(UploadPrescription Request)
        {
            try
            {
                var Response = await this.ExecuteAsync("Execute SP_MEDICINE @Flag='M',@PhoneNumber=@PhoneNumber,@OTP=@OTP" 
                    
                    , new
                    {
                        PhoneNumber = Request.PhoneNumber,
                        OTP = Request.RandomOTP,
                    });
                return await Result<MessageResponse>.SuccessAsync("OTP Sent Successfully");
            }
            catch (Exception ex)
            {
                return await Result<MessageResponse>.FailAsync(ex.Message);
            }

        }
        public async Task<Result<OTPModel>> GetOTP(string PhoneNumber)
        {
            var Response = await this.GetQueryFirstOrDefaultResultAsync<OTPModel>("Execute SP_MEDICINE @Flag='N',@PhoneNumber=@PhoneNumber"

                      , new
                      {
                          PhoneNumber =PhoneNumber,
                      });
            return await Result<OTPModel>.SuccessAsync(Response);
        }
        public async Task UpdateOTP(string PhoneNumber)
        {
          await this.ExecuteAsync("Execute SP_MEDICINE @Flag='O',@PhoneNumber=@PhoneNumber"

                      , new
                      {
                          PhoneNumber = PhoneNumber,
                      });
        }
        public async Task<Result<List<UserOrderResponse>>> UserOrder()
        {
            try
            {
                var data = await this.GetQueryResultAsync<UserOrderResponse>("Execute SP_MEDICINE @Flag='P'");
                return await Result<List<UserOrderResponse>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<UserOrderResponse>>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<List<UserOrderResponse>>> CartOrder()
        {
            try
            {
                var data = await this.GetQueryResultAsync<UserOrderResponse>("Execute SP_MEDICINE @Flag='S',@UserId=@UserId",
                    new
                    {
                        UserId=_currentUserService.UserId,
                    });
                return await Result<List<UserOrderResponse>>.SuccessAsync(data);
            }
            catch (Exception ex)
            {
                return await Result<List<UserOrderResponse>>.FailAsync(ex.Message);
            }
        }
        public async Task<Result<MessageResponse>> SaveCart(UploadPrescription Request)
        {
            try
            {
                if (Request.uploadMedicine != null)
                {
                    var filePath = _uploadService.UploadAsync(Request.uploadMedicine);
                    Request.ImagePath = filePath;
                }
                //var Medicine = _mapper.Map<Medicine>(Request);
                //await _DB.tblMedicine.AddAsync(Medicine);
                //await _DB.SaveChangesAsync();
                //return await Result<MessageResponse>.SuccessAsync("Medicine Saved");
                var Response = await this.ExecuteAsync("Execute SP_MEDICINE @Flag='Q',@Price=@Price,@ProductId=@ProductId," +
                    "@UserId=@UserId,@ImagePath=@ImagePath,@Quantity=@Quantity"
                    , new
                    {
                        Quantity = Request.Quantity,
                        ProductId = Request.productId,
                        UserId = _currentUserService.UserId,
                        ImagePath = Request.ImagePath,
                        Price=Request.Price
                    });
                return await Result<MessageResponse>.SuccessAsync("Items Added to Cart Successfully");
            }
            catch (Exception ex)
            {
                return await Result<MessageResponse>.FailAsync(ex.Message);
            }

        }
        public async Task<Result<GetCartCount>> GetCount()
        {
            var Response = await this.GetQueryFirstOrDefaultResultAsync<GetCartCount>("Execute SP_MEDICINE @Flag='R',@UserId=@UserId"

                      , new
                      {
                          UserId = _currentUserService.UserId,
                      });
            return await Result<GetCartCount>.SuccessAsync(Response);
        }
        public async Task CancleProduct(int Id)
        {
            await this.ExecuteAsync("Execute SP_MEDICINE @Flag=@Flag,@Id=@Id",
                new
                {
                    Flag = 'T',
                    Id = Id
                });
        }
        public async Task CancleAllProduct()
        {
            await this.ExecuteAsync("Execute SP_MEDICINE @Flag=@Flag,@UserId=@Id",
                new
                {
                    Flag = 'W',
                    Id = _currentUserService.UserId
                });
        }

    }
}
