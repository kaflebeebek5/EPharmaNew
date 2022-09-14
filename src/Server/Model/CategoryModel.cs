using System;

namespace EPharma.Server.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
