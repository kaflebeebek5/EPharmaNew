using EPharma.Application.Features.Branch.Commands.AddEdit;
using EPharma.Application.Features.Branch.Queries.GetAll;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.Branch
{
    public interface IBranchManager : IManager
    {
        Task<IResult<List<GetAllBranchResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditBranchCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}
