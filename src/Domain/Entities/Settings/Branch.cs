using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPharma.Domain.Entities.Settings
{
    public class Branch: Address
    {
        [StringLength(50)]
        public string NameNepali { get; set; }
        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(20)]
        public string NRBCode { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? OperationDate { get; set; }
        public int? ParentBranchId { get; set; }
        public virtual Branch ParentBranch { get; set; }
        public int? BranchTypeId { get; set; }
        public virtual BranchType BranchType { get; set; }
    }
}
