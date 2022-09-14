using EPharma.Application.Features.Vdc.Queries.GetById;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.v1.Settings
{
    public class LocalBodiesController : BaseApiController<DistrictController>
    {
        /// <summary>
        /// Get a Gender By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Genders.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vdc = await _mediator.Send(new GetVdcByIdQuery() { Id = id });
            return Ok(vdc);
        }
    }
}
