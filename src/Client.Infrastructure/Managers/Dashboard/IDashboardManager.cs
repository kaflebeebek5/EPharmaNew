using EPharma.Shared.Wrapper;
using System.Threading.Tasks;
using EPharma.Application.Features.Dashboards.Queries.GetData;

namespace EPharma.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}