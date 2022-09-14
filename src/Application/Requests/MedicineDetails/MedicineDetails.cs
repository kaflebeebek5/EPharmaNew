using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests.MedicineDetails
{
    public class MedicineDetails
    {
        public List<Medicine> MedicineList { get; set; } = new();
    }
    public class Medicine
    {
        public int SN { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }

    }
}
