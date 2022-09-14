using System.ComponentModel.DataAnnotations;

namespace EPharma.Domain.Entities.Settings
{
    public abstract class MasterTableNepali : MasterTable
    {
        [StringLength(50)]
        public string NameNepali { get; set; }
    }
}
