using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Domain.Entities.Settings
{
    public class BillSetup
    {
        [Key]
        public int Id { get; set; }
        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public int StartingNumber { get; set; }
        public int NoOfDigit { get; set; }
    }
}
