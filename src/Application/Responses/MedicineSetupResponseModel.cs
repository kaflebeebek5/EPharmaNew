using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Responses
{
    public class MedicineSetupResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int QuantityAvailable { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Unit { get; set; }
        public string Manufacturer { get; set; }
    }
}
