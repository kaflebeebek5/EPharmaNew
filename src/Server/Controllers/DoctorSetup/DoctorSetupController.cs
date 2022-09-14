using EPharma.Application.Interfaces.DoctorSetup;
using EPharma.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.DoctorSetup
{
    [Route("api/Doctor")]
    [Authorize]
    [AllowAnonymous]
    public class DoctoSetupController : ControllerBase
    {
        private readonly IDoctorSetupService _doctorSetUpService;

        public DoctoSetupController(IDoctorSetupService doctorSetupService)
        {
            _doctorSetUpService = doctorSetupService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveMedicine([FromBody] DoctorSetupRequestModel requestModel)
        {
            var Response = await _doctorSetUpService.SaveDoctor(requestModel);
            return Ok(Response);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllMedicine()
        {
            var Response = await _doctorSetUpService.GetAll();
            return Ok(Response);
        }
        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteDoctor(int Id)
        {
            var Response = await _doctorSetUpService.DeleteDoctor(Id);
            return Ok(Response);
        }

    }
}
