using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPharma.Application.Responses;
using EPharma.Client.Infrastructure.Managers.MedicineSetup;
using Microsoft.AspNetCore.Components;

namespace EPharma.Client.Pages.Content
{
    public partial class ViewProductCategoryWise
    {
        [Parameter] public string Id { get; set; }
        [Inject] private IMedicineSetupManager MedicineSetupManager { get; set; }
        private List<CategoryResponse> _CategoryList { get; set; } = new();
        private List<MedicineSetupResponseModel> _MedicineList { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await GetProductByCategory();
        }
        public async Task GetProductByCategory()
        {
            var Response = await MedicineSetupManager.GetById(Convert.ToInt32(Id));
            if (Response.Succeeded && Response.Data != null)
            {
                _MedicineList = Response.Data.ToList();
            }
        }
       
    }
}