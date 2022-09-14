using EPharma.Application.Features.BankSetup.Commands.AddEdit;
using EPharma.Application.Features.BankSetup.Queries.GetAll;
using EPharma.Application.Features.BankSetup.Queries.GetById;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.BankSetup
{
    public interface IBankSetupManager :IManager
    {
        Task<IResult<List<GetAllBankSetupResponse>>> GetAllAsync();
        Task<IResult<int>> SaveAsync(AddEditBankSetupCommand request);
        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<string>> ExportToExcelAsync(string searchString = "");

        Task<Result<List<GetBankSetupByIdResponse>>> GetParentItemAsync();
    }
}
