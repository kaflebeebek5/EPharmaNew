using EPharma.Application.Features.BankSetup.Commands.AddEdit;
using EPharma.Application.Features.BankSetup.Commands.Delete;
using EPharma.Application.Features.BankSetup.Queries.Export;
using EPharma.Application.Features.BankSetup.Queries.GetAll;
using EPharma.Application.Features.BankSetup.Queries.GetById;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.v1.Settings
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[AllowAnonymous]
    public class BankSetupController : BaseApiController<BankSetupController>
    {
        /// <summary>
        /// Get All Bank Setups
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Permissions.BankSetup.View)]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var setup = await _mediator.Send(new GetAllBankSetupQuery());
            return Ok(setup);
        }
        /// <summary>
        /// Get All bank Setup By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.BankSetup.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var setup = await _mediator.Send(new GetBankSetupByIdQuery() { Id = id });
            return Ok(setup);
        }
        /// <summary>
        /// Create Bank Setup
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.BankSetup.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditBankSetupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Delete BankSetup
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize (Policy =Permissions.BankSetup.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteBankSetupCommand { Id = id }));
        }
        /// <summary>
        /// Export BankSetup
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy =Permissions.BankSetup.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export (string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportBankSetupQuery(searchString)));
        }
        /// <summary>
        /// Get All ParentItem
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.MenuList.View)]
        [HttpGet("ParentItem/")]
        public async Task<IActionResult> GetParentItem()
        {
            var menulist = await _mediator.Send(new GetBankSetupByIdQuery());
            return Ok(menulist);
        }
    }
}
