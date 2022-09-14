using EPharma.Application.Features.MasterTableSetup.Commands.AddEdit;
using EPharma.Application.Features.MasterTableSetup.Commands.Delete;
using EPharma.Application.Features.MasterTableSetup.Queries.Export;
using EPharma.Application.Features.MasterTableSetup.Queries.GetAll;
using EPharma.Application.Features.MasterTableSetup.Queries.GetById;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace EPharma.Server.Controllers.v1.Settings
{
    public class MasterTableSetupController : BaseApiController<MasterTableSetupController>
    {

        [Authorize(Policy = Permissions.MasterTableSetup.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mastertable = await _mediator.Send(new GetAllMasterTableSetupQuery());
            return Ok(mastertable);
        }

        [Authorize(Policy = Permissions.MasterTableSetup.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mastertable = await _mediator.Send(new GetMasterTableSetupByIdQuery() { Id = id });
            return Ok(mastertable);
        }

        [Authorize(Policy = Permissions.MasterTableSetup.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditMasterTableSetupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.MasterTableSetup.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteMasterTableSetupCommand { Id = id }));
        }

        [Authorize(Policy = Permissions.MasterTableSetup.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportMasterTableSetupQuery(searchString)));
        }
    }
}
