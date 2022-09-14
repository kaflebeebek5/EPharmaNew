using System.ComponentModel.DataAnnotations;

namespace EPharma.Domain.Entities.Settings
{
    public abstract class MasterTable : AuditableEntity<int>
    {
        [StringLength(50)]
        public string Name { get; set; }
    }
}
