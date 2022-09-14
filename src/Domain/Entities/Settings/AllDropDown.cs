using EPharma.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Domain.Entities.Settings
{
   public class AllDropDown : AuditableEntity<int>
    {
        [StringLength(50)]
        public string Name { get; set; }
    }
}
