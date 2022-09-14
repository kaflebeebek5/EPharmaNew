using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Domain.Entities.Settings
{
    [Table("TblCategory")]
    public class tblCategory:AuditableEntity<int>
    {
        public string Name { get; set; }
    }
}
