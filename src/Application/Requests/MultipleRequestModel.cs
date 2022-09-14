using EPharma.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests
{
    public class MultipleRequestModel
    {
        public string Remarks { get; set; }
        public string BillingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public List<UserOrderResponse> MultipleReq { get; set; } = new();
    }
}
