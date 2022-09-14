using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Responses
{
    public class UserMedicineDetails
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Timing { get; set; }
        public int Quantity { get; set; }
    }
}
