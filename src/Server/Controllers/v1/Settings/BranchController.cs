using EPharma.Application.Features.Branch.Commands.AddEdit;
using EPharma.Application.Features.Branch.Commands.Delete;
using EPharma.Application.Features.Branch.Queries.Export;
using EPharma.Application.Features.Branch.Queries.GetAll;
using EPharma.Application.Features.Branch.Queries.GetById;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.v1.Management
{
    public class BranchController :  BaseApiController<BranchController>
    {
        /// <summary>
        /// Get All Branch
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Branch.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var branch = await _mediator.Send(new GetAllBranchQuery());
            return Ok(branch);
        }

        /// <summary>
        /// Get a Branch By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Branch.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var branch = await _mediator.Send(new GetBranchByIdQuery() { Id = id });
            return Ok(branch);
        }

        /// <summary>
        /// Create/Update a Branch
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Branch.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditBranchCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Branch
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Branch.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteBranchCommand { Id = id }));
        }

        /// <summary>
        /// Search Branch and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Branch.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportBranchQuery(searchString)));
        }
    }
}
