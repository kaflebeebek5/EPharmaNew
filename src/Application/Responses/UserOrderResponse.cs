using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Responses
{
    public class UserOrderResponse
    {
         public string ImagePath { get; set; }
         public string BillingAddress { get; set; }
         public string PhoneNumber { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
         public int Id { get; set; }
         public string UserId { get; set; }
         public string Remarks { get; set; }
         public string ProductName { get; set; }
        public int IsApproved { get; set; }    
        public int IsDelivered { get; set; }
        public string ApproveStatus { get; set; }
        public string DeliveredStatus { get; set; }
        public int SN { get; set; }
    }
}
