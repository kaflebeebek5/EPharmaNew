using System.Threading.Tasks;
using EPharma.Application.Interfaces.Services.Identity;
using EPharma.Application.Requests.Identity;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPharma.Server.Controllers.v1.Settings
{
    [Route("api/identity/hrmenulist")]
    [ApiController]
    public class MenuListController : ControllerBase
    {
        private readonly IMenuListService _menulistService;

        public MenuListController(IMenuListService menulistService)
        {
            _menulistService = menulistService;
        }

        /// <summary>
        /// Get All MenuList
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MenuList.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var menulist = await _menulistService.GetAllAsync();
            return Ok(menulist);
        }
        /// <summary>
        /// Get All ParentItem
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MenuList.View)]
        [HttpGet("ParentItem/")]
        public async Task<IActionResult> GetParentItem()
        {
            var menulist = await _menulistService.GetParentItemAsync();
            return Ok(menulist);
        }

        /// <summary>
        /// Get MenuList By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MenuList.View)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById( int Id)
        {
            var response = await _menulistService.GetByIdAsync(Id);
            return Ok(response);
        }

        /// <summary>
        /// Add MenuList
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK </returns>
        [Authorize(Policy = Permissions.MenuList.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(MenuListRequest request)
        {
            var response = await _menulistService.SaveAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete a MenuList
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MenuList.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _menulistService.DeleteAsync(id);
            return Ok(response);
        }

    }
}
