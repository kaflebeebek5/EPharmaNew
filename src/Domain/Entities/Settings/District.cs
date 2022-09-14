using System.ComponentModel.DataAnnotations;

namespace EPharma.Domain.Entities.Settings
{
    public class District : MasterTableNepali
    {
        [StringLength(20)]
        public string Code { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}
