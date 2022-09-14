using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPharma.Domain.Entities.Settings
{
    public class LocalBodies : MasterTableNepali
    {
        [StringLength(20)]
        public string Code { get; set; }
        public int LocalBodiesTypeId { get; set; }
        public virtual LocalBodiesType LocalBodiesType { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
    }
}
