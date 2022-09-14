using System.ComponentModel.DataAnnotations;

namespace EPharma.Domain.Entities.Settings
{
    public class Province : MasterTableNepali
    {
        [StringLength(20)]
        public string Code { get; set; }
    }
}
