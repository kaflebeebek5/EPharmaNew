using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPharma.Domain.Contracts;

namespace EPharma.Domain.Entities.Settings
{
    public class MasterTableSetup : AuditableEntity<int>
    {
        [StringLength(50)]
        public string TableName { get; set; }

        [StringLength(20)]
        public string ColumnId { get; set; }

        [StringLength(20)]
        public string ColumnName { get; set; }
    }
}
