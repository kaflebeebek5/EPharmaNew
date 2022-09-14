using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests
{
    public class BillEntryRequestModel
    {
        public BillEntryRequestModel()
        {
            MedicineList = new HashSet<UserMedicines>();
        }

        public int Id { get; set; }
        public string BillNumber { get; set; }
        public string UserId { get; set; }
        public int DoctorId { get; set; }
        public ICollection<UserMedicines> MedicineList { get; set; } 

    }
    public class UserMedicines
    {
        public int SN { get; set; }
        public string MedicineName { get; set; }
        public int MedicineId { get; set; }
        public string Timing { get; set; }
        public int Quantity { get; set; }
    }
}
