using EPharma.Application.Specifications.Base;
using EPharma.Domain.Entities.Settings;

namespace EPharma.Application.Specifications.Settings
{
    public class BranchFilterSpecification : HeroSpecification<Branch>
    {
        public BranchFilterSpecification(string searchString)
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
