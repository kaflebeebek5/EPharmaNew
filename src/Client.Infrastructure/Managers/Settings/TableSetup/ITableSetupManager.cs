using EPharma.Application.Features.TableSetup.Commands.AddEdit;
using EPharma.Application.Features.TableSetup.Queries.GetAll;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.TableSetup
{
    public interface ITableSetupManager : IManager
    {
        Task<IResult<List<GetAllTableSetupResponse>>> GetAllAsync(string tableName);

        Task<IResult<int>> SaveAsync(AddEditTableSetupCommand request);

        Task<IResult<int>> DeleteAsync(int id, string tableName);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}
