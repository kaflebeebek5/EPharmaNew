using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPharma.Infrastructure.Models.Identity
{
    public class MenuRole
    {
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Column(Order = 2)]
        public virtual string RoleId { get; set; }

        [Column(Order = 3)]
        public virtual int MenuId { get; set; }

        public virtual HrRole Role { get; set; }
        public virtual MenuList MenuList { get; set; }
    }
}
