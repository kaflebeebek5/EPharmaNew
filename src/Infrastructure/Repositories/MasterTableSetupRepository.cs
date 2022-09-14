using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Repositories
{
    class MasterTableSetupRepository : IMasterTableSetupRepository
    {
        private readonly IRepositoryAsync<MasterTableSetup, int> _repository;

        public MasterTableSetupRepository (IRepositoryAsync<MasterTableSetup, int> repository)
        {
            _repository = repository;
        }
    }
}
