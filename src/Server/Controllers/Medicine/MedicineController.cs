using EPharma.Application.Interfaces.MedicineSeup;
using EPharma.Application.Requests;
using EPharma.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.Medicine
{
    [Route("api/Medicine")]
    [AllowAnonymous]
    public class MedicineController: ControllerBase
    {
        private readonly IMedicineSetUpService _medicineSetUpService;

        public MedicineController(IMedicineSetUpService medicineSetUpService)
        {
            _medicineSetUpService = medicineSetUpService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveMedicine([FromBody]MedicineRequestModel requestModel)
        {
            var Response = await _medicineSetUpService.SaveMedicine(requestModel);
            return Ok(Response);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllMedicine()
        {
            var Response = await _medicineSetUpService.GetAll();
            return Ok(Response);
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var Response = await _medicineSetUpService.GetAllCategory();
            return Ok(Response);
        }
        [HttpGet("Delete")]
        public async Task<IActionResult>DeleteMedicine(int Id)
        {
            var Response = await _medicineSetUpService.DeleteMedicine(Id);
            return Ok(Response);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Response = await _medicineSetUpService.GetById(Id);
            return Ok(Response);
        }
        [HttpGet("GetByProductId")]
        public async Task<IActionResult> GetByProductId(int Id)
        {
            var Response = await _medicineSetUpService.GetByProductId(Id);
            return Ok(Response);
        }
        [HttpPost("UploadPrescription")]
        public async Task<IActionResult> SavePrescription([FromBody] UploadPrescription requestModel)
        {
            var Response = await _medicineSetUpService.SavePrescription(requestModel);
            return Ok(Response);
        }
        [HttpPost("SaveCartOrder")]
        public async Task<IActionResult> SaveCartOrder([FromBody] MultipleRequestModel requestModel)
        {
            var Response = await _medicineSetUpService.SaveCartOrder(requestModel);
            return Ok(Response);
        }
        [HttpPost("OTP")]
        public async Task<IActionResult> SaveOTP([FromBody] UploadPrescription requestModel)
        {
            var Response = await _medicineSetUpService.UserOTP(requestModel);
            return Ok(Response);
        }
        [HttpGet("GetUserOTP")]
        public async Task<IActionResult> GetOTP(string PhoneNumber)
        {
            var Response = await _medicineSetUpService.GetOTP(PhoneNumber);
            return Ok(Response);
        }
        [HttpGet("UpdateOTP")]
        public async Task UPDATEOTP(string PhoneNumber)
        {
            await _medicineSetUpService.UpdateOTP(PhoneNumber);
        }
        [HttpGet("GetAllUserOrder")]
        public async Task<IActionResult> GetAllUserOrder()
        {
            var Response = await _medicineSetUpService.UserOrder();
            return Ok(Response);
        }
        [HttpGet("GetCartOrder")]
        public async Task<IActionResult> GetAllCartOrder()
        {
            var Response = await _medicineSetUpService.CartOrder();
            return Ok(Response);
        }
        [HttpPost("SaveCart")]
        public async Task<IActionResult> SaveCart([FromBody] UploadPrescription requestModel)
        {
            var Response = await _medicineSetUpService.SaveCart(requestModel);
            return Ok(Response);
        }
        [HttpGet("GetCount")]
        public async Task<IActionResult> GetCount()
        {
            var Response = await _medicineSetUpService.GetCount();
            return Ok(Response);
        }
        [HttpGet("Cancel")]
        public async Task CancleProduct(int id)
        {
            await _medicineSetUpService.CancleProduct(id);

        }
        [HttpGet("CancelAll")]
        public async Task CancleProduct()
        {
            await _medicineSetUpService.CancleAllProduct();

        }
    }
}
