using System;
using System.ComponentModel.DataAnnotations;
using EPharma.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace EPharma.Infrastructure.Models.Identity
{
    public class HrRoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
    {
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Group { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual HrRole Role { get; set; }

        public HrRoleClaim() : base()
        {
        }

        public HrRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}