using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests
{
    public class UserMedicineRequestModel
    {
        public int BillId { get; set; }
        public string Disease { get; set; }
        public List<UserMedicine> UserMedicineList { get; set; }
    }
    public class UserMedicine
    {
        public int SN { get; set; }
        public string MedicineName { get; set; }
        public int MedicineId { get; set; }
        public string Timing { get; set; }
        public int Quantity { get; set; }
    }
   
}
