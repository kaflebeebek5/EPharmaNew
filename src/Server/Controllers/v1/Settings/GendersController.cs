using EPharma.Application.Features.Genders.Commands.AddEdit;
using EPharma.Application.Features.Genders.Commands.Delete;
using EPharma.Application.Features.Genders.Queries.Export;
using EPharma.Application.Features.Genders.Queries.GetAll;
using EPharma.Application.Features.Genders.Queries.GetById;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.v1.Management
{
    public class GendersController :  BaseApiController<GendersController>
    {
        /// <summary>
        /// Get All Genders
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Genders.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genders = await _mediator.Send(new GetAllGendersQuery());
            return Ok(genders);
        }

        /// <summary>
        /// Get a Gender By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Genders.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gender = await _mediator.Send(new GetGenderByIdQuery() { Id = id });
            return Ok(gender);
        }

        /// <summary>
        /// Create/Update a Gender
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Genders.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditGenderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Gender
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Genders.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteGenderCommand { Id = id }));
        }

        /// <summary>
        /// Search Gender and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Genders.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportGendersQuery(searchString)));
        }
    }
}
