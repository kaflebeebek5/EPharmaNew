using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using EPharma.Domain.Contracts;


namespace EPharma.Infrastructure.Models.Identity
{
    public class MenuList 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuList()
        {
            Children = new HashSet<MenuList>();
            RoleMenus = new HashSet<MenuRole>();
        }

        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string MenuName { get; set; }

        [StringLength(100)]
        public string MenuNameNepali { get; set; }

        public int? ParentId { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        [StringLength(50)]
        public string Path { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuList> Children { get; set; }

        public virtual MenuList ParentItem { get; set; }

        public virtual ICollection<MenuRole> RoleMenus { get; set; }
    }
}

