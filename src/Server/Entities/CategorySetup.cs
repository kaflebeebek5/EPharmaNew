using System;
using System.ComponentModel.DataAnnotations;

namespace EPharma.Server.Entities
{
    public class CategorySetup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
