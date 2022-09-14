using EPharma.Application.Features.TableSetup.Commands.AddEdit;
using EPharma.Application.Features.TableSetup.Commands.Delete;
using EPharma.Application.Features.TableSetup.Queries.GetAll;
using EPharma.Application.Features.TableSetup.Queries.GetById;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.v1.Management
{
    public class TableSetupController :  BaseApiController<TableSetupController>
    {
        /// <summary>
        /// Get All Table Setup
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.TableSetup.View)]
        [HttpGet("{tableName}")]
        public async Task<IActionResult> GetAll(string tableName)
        {
            var genders = await _mediator.Send(new GetAllTableSetupQuery(){TableName = tableName });
            return Ok(genders);
        }

        /// <summary>
        /// Get a Table Setup By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.TableSetup.View)]
        [HttpGet("{id}/{tableName}")]
        public async Task<IActionResult> GetById(int id, string tableName)
        {
            var gender = await _mediator.Send(new GetTableSetupByIdQuery() { Id = id, TableName = tableName });
            return Ok(gender);
        }

        /// <summary>
        /// Create/Update a Table Setup
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.TableSetup.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditTableSetupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a TableSetup
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.TableSetup.Delete)]
        [HttpDelete("{id}/{tableName}")]
        public async Task<IActionResult> Delete(int id, string tableName)
        {
            return Ok(await _mediator.Send(new DeleteTableSetupCommand { Id = id, TableName= tableName}));
        }
    }
}
