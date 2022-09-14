using EPharma.Domain.Contracts;
using EPharma.Domain.Entities.ExtendedAttributes;
using System.ComponentModel.DataAnnotations;

namespace EPharma.Domain.Entities.Misc
{
    public class Document : AuditableEntityWithExtendedAttributes<int, int, Document, DocumentExtendedAttribute>
    {
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        [StringLength(150)]
        public string URL { get; set; }
        public int DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}