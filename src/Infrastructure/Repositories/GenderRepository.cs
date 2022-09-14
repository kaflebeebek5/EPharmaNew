using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Infrastructure.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly IRepositoryAsync<Gender, int> _repository;

        public GenderRepository(IRepositoryAsync<Gender, int> repository)
        {
            _repository = repository;
        }

    }
}
