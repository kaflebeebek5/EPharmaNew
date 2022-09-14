using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Infrastructure.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly IRepositoryAsync<Level, int> _repository;
        public LevelRepository(IRepositoryAsync<Level, int> repository)
        {
            _repository = repository;
        }
    }
}
