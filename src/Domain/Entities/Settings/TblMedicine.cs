using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Domain.Entities.Settings
{
   public class TblMedicine:AuditableEntity<int>
   {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int QuantityAvailable { get; set; }
        public string ImagePath { get; set; }
        public int StatusId { get; set; }
    }
}
