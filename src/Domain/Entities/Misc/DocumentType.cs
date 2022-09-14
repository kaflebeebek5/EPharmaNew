using EPharma.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace EPharma.Domain.Entities.Misc
{
    public class DocumentType : AuditableEntity<int>
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
    }
}