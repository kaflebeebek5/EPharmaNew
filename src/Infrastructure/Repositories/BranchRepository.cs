using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IRepositoryAsync<Branch, int> _repository;

        public BranchRepository(IRepositoryAsync<Branch, int> repository)
        {
            _repository = repository;
        }

    }
}
