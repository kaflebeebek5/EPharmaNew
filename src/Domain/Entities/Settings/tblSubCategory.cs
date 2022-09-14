using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Domain.Entities.Settings
{
    public class tblSubCategory:AuditableEntity<int>
    {
        public string Name { get; set; }
    }
}
