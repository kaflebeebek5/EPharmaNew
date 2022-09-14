using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Features.BankSetup.Queries.GetAll
{
    public class GetAllBankSetupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BranchName { get; set; }
        public int? BankParentId { get; set; }
        public string ParentItem { get; set; }
    }
}
