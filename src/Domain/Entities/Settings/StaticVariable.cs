using EPharma.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace EPharma.Domain.Entities.Settings
{
    public class StaticVariable : MasterTable
    {
        [StringLength(50)]
        public string Value { get; set; }
    }
}
