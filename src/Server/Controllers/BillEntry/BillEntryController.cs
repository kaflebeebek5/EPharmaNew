using EPharma.Application.Interfaces.BillEntry;
using EPharma.Application.Interfaces.MedicineSeup;
using EPharma.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.BillEntry
{
    [Route("api/BillEntry")]
    [Authorize]
    [AllowAnonymous]
    public class BillEntryController : ControllerBase
    {
        private readonly IBillEntryService _billEntryService;

        public BillEntryController(IBillEntryService billEntryService)
        {
            _billEntryService = billEntryService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveMedicine([FromBody] BillEntryRequestModel requestModel)
        {
            var Response = await _billEntryService.SaveBillEntry(requestModel);
            return Ok(Response);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllMedicine()
        {
            var Response = await _billEntryService.GetAll();
            return Ok(Response);
        }
        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteMedicine(int Id)
        {
            var Response = await _billEntryService.DeleteBill(Id);
            return Ok(Response);
        }
        [HttpGet("BillNumber")]
        public async Task<IActionResult> BillNumber()
        {
            var Response = await _billEntryService.GetBillNumber();
            return Ok(Response);
        }
        [HttpGet("UpdateStartingNumber")]
        public async Task<IActionResult> UpdateStartingNumber()
        {
           var Response= await _billEntryService.UpdateStartingNumber();
            return Ok(Response);
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GerUser()
        {
            var Response = await _billEntryService.GetAllUser();
            return Ok(Response);
        }


    }
}
