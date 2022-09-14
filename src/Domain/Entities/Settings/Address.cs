using EPharma.Domain.Entities.Settings;
using System.ComponentModel.DataAnnotations;

namespace EPharma.Domain.Entities.Settings
{
    public class Address : MasterTable
    {
        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public int? DistrictId { get; set; }
        public virtual District District { get; set; }
        public int? LocalBodiesId { get; set; }
        public virtual LocalBodies LocalBodies { get; set; }
        [StringLength(100)]
        public string Locality { get; set; }
        public int? WardNo { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
