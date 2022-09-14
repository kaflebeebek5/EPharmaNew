using EPharma.Application.Features.Vdc.Queries.GetById;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.Localbodies
{
    public interface ILocalBodiesManager : IManager
    {
        Task<Result<List<GetVdcByIdResponse>>> GetById(int id);
    }
}
