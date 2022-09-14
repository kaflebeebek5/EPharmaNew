using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests
{
    public class UploadPrescription
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int productId { get; set; }
        public string BillingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int status { get; set; }
        public string OTP { get; set; }
        public string RandomOTP { get; set; }
        public bool IsChecked { get; set; }
        public string Remarks { get; set; }
        public UploadPrescriptionFile uploadMedicine { get; set; } = new();
    }
    public class UploadPrescriptionFile : UploadRequest
    {

    }
}
