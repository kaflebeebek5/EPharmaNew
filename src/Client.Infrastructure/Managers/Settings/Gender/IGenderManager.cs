using EPharma.Application.Features.Genders.Commands.AddEdit;
using EPharma.Application.Features.Genders.Queries.GetAll;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.Gender
{
    public interface IGenderManager : IManager
    {
        Task<IResult<List<GetAllGendersResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditGenderCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}
