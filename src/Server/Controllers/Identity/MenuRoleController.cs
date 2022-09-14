
using EPharma.Application.Interfaces.Services.Identity;
using EPharma.Application.Requests.Identity;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.Identity
{

    [Route("api/identity/MenuRole")]
    [ApiController]
    public class MenuRoleController : ControllerBase
    {
        private readonly IMenuRoleService _menuroleService;

        public MenuRoleController(IMenuRoleService menuroleService)
        {
            _menuroleService = menuroleService;
        }

        /// <summary>
        /// Get All MenuRole
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MenuRole.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var menurole = await _menuroleService.GetAllAsync();
            return Ok(menurole);
        }

        /// <summary>
        /// Get User Menu List
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize]
        [HttpGet("GetUserMenuList")]
        public async Task<IActionResult> GetUserMenuListAsync()
        {
            var menurole = await _menuroleService.GetUserMenuListAsync();
            return Ok(menurole);
        }

        /// <summary>
        /// Get MenuRole By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MenuRole.View)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            var response = await _menuroleService.GetByIdAsync(Id);
            return Ok(response);
        }

        /// <summary>
        /// Add MenuRole
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK </returns>
        [Authorize(Policy = Permissions.MenuRole.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(List<MenuRoleRequest> request)
        {
            var response = await _menuroleService.SaveAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete a MenuRole
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MenuRole.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _menuroleService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
