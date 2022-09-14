using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests
{
    public class MedicineRequestModel
    {
     public int Id { get; set; }
     public int CategoryId { get; set; }  
     public int SubCategoryId { get; set; }  
     public bool IsActive { get; set; }
     public string Name { get; set; }
     public string Description { get; set; }
     public DateTime ManufactureDate { get; set; }
     public DateTime ExpiryDate { get; set; }
     public int QuantityAvailable { get; set; }
     public decimal BuyPrice { get; set; }
     public decimal SalePrice { get; set; }
     public string Unit { get; set; }
     public string Manufacturer { get; set; }
     public string ImagePath { get; set; }
     public string SubCategory { get; set; }
     public UploadMedicine  uploadMedicine { get; set; } = new();
    }
    public class UploadMedicine : UploadRequest
    {

    }
}
