using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Responses
{
    public class BillEntryResponseModel
    {
        public int Id { get; set; }
        public string BillNumber { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DoctorName { get; set; }
        public int DoctorId { get; set; }
    }
}
