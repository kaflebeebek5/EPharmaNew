using EPharma.Application.Interfaces.Repositories;
using EPharma.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Infrastructure.Repositories
{
   public class ForAllDropdownRepository : IForAllDropdownRepository
    {
        private readonly IRepositoryAsync<AllDropDown, int> _repository;
        public ForAllDropdownRepository(IRepositoryAsync<AllDropDown, int> repository)
        {
            _repository = repository;
        }
    }
}
