using EPharma.Application.Features.StaticVariable.Queries.GetAll;
using EPharma.Application.Features.StaticVariables.Queries.GetByName;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EPharma.Server.Controllers.v1.Management
{
    public class StaticVariableController :  BaseApiController<StaticVariableController>
    {
        /// <summary>
        /// Get All Static Variables
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var staticVariables = await _mediator.Send(new GetAllStaticVariableQuery());
            return Ok(staticVariables);
        }

        /// <summary>
        /// Get a Static Variable By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Status 200 Ok</returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetById(string name)
        {
            var staticVariable = await _mediator.Send(new GetStaticVariableByNameQuery() { Name = name });
            return Ok(staticVariable);
        }
    }
}
