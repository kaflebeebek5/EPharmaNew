using EPharma.Application.Features.Province.Queries.GetById;
using EPharma.Application.Responses.Identity;
using EPharma.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Client.Infrastructure.Managers.Settings.District
{
    public interface IDistrictManager : IManager
    {
        Task<Result<List<GetDistrictByIdResponse>>> GetById(int id);
    }
}
