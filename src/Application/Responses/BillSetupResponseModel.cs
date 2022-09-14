using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Responses
{
    public class BillSetupResponseModel
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StartingNumber { get; set; }
        public string NoOfDigit { get; set; }
    }
}
