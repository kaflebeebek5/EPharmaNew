using EPharma.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPharma 
{
    public abstract class AuditableEntity<TId> : IAuditableEntity<TId>
    {
        public TId Id { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(50)]
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}