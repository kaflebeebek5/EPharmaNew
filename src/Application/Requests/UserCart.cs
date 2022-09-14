using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests
{
    public class UserCart
    {
         public int Id { get; set; } 
         public int ProductId { get; set; }
         public int Quantity { get; set; }
         public int Status { get; set; }
         public Decimal Price { get; set; }
        public string PrescriptionPhoto { get; set; }
        public UploadPrescriptionFile uploadMedicine { get; set; } = new();
    }
}
