using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPharma.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace EPharma.Infrastructure.Models.Identity
{
    public class HrRole : IdentityRole, IAuditableEntity<string>
    {
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual ICollection<HrRoleClaim> RoleClaims { get; set; }

        public ICollection<MenuRole> menuRoles { get; set; }
        public HrRole() : base()
        {
            RoleClaims = new HashSet<HrRoleClaim>();
            menuRoles = new HashSet<MenuRole>();
        }

        public HrRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<HrRoleClaim>();
            Description = roleDescription;
            menuRoles = new HashSet<MenuRole>();
        }
    }
}