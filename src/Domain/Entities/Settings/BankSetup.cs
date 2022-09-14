using EPharma.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Domain.Entities.Settings
{
    public class BankSetup: AuditableEntity<int>
    {

        public BankSetup()
        {
            Children = new HashSet<BankSetup>();
        }
        //[Required]
        //public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string BranchName { get; set; }
        public int? BankParentId { get; set; }
        public virtual ICollection<BankSetup> Children { get; set; }

        public virtual BankSetup ParentItem { get; set; }


    }
}
