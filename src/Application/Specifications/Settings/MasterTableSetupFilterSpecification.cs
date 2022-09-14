using EPharma.Application.Specifications.Base;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Application.Specifications.Settings
{
    public class MasterTableSetupFilterSpecification : HeroSpecification<MasterTableSetup>
    {
        public MasterTableSetupFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.TableName.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
    
}
