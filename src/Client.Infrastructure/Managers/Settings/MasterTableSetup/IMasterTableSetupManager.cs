using EPharma.Application.Features.MasterTableSetup.Commands.AddEdit;
using EPharma.Application.Features.MasterTableSetup.Queries.GetAll;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.MasterTableSetup
{
    public interface IMasterTableSetupManager : IManager
    {
        Task<IResult<List<GetAllMasterTableSetupResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditMasterTableSetupCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}
