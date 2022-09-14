using EPharma.Application.Interfaces.UserMedicine;
using EPharma.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.UserMedicine
{
    [Route("api/UserMedicine")]
    [Authorize]
    [AllowAnonymous]
    public class UserMedicineController : ControllerBase
    {
        private readonly IUserMedicineService _usermedicineService;

        public UserMedicineController(IUserMedicineService usermedicineService)
        {
            _usermedicineService = usermedicineService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] BillEntryRequestModel requestModel)
        {
            var Response = await _usermedicineService.SaveUserMedicine(requestModel);
            return Ok(Response);
        }
        [HttpGet("GetAllById")]
        public async Task<IActionResult> GetAsync(string UserId)
        {
            var Response=await _usermedicineService.GetUserMedicine(UserId);
            return Ok(Response);
        }
    }
}
