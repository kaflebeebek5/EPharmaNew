using EPharma.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EPharma.Application.Interfaces.Chat;
using EPharma.Application.Models.Chat;
using System.ComponentModel.DataAnnotations;

namespace EPharma.Infrastructure.Models.Identity
{
    public class HrUser : IdentityUser<string>, IChatUser, IAuditableEntity<string>
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "text")]
        public string ProfilePictureDataUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }

        [StringLength(1000)]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<ChatHistory<HrUser>> ChatHistoryFromUsers { get; set; }
        public virtual ICollection<ChatHistory<HrUser>> ChatHistoryToUsers { get; set; }

        public HrUser()
        {
            ChatHistoryFromUsers = new HashSet<ChatHistory<HrUser>>();
            ChatHistoryToUsers = new HashSet<ChatHistory<HrUser>>();
        }
    }
}