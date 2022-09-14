using EPharma.Application.Specifications.Base;
using EPharma.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Specifications.Settings
{
    public class BankSetupFilterSpecification :HeroSpecification<BankSetup>
    {
        public BankSetupFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
