using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Infrastructure.Repositories
{
    public class StaticVariableRepository : IStaticVariableRepository
    {
        private readonly IRepositoryAsync<StaticVariable, int> _repository;

        public StaticVariableRepository(IRepositoryAsync<StaticVariable, int> repository)
        {
            _repository = repository;
        }
    }
}
