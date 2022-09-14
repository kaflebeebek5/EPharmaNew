using EPharma.Application.Features.StaticVariable.Queries.GetAll;
using EPharma.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.StaticVariable
{
    public interface IStaticVariableManager : IManager
    {
        Task<IResult<List<GetAllStaticVariableResponse>>> GetAllAsync();
        Task<IResult<GetAllStaticVariableResponse>> GetByNameAsync(string name);

    }
}
