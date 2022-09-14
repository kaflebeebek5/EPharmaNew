using EPharma.Application.Responses;
using EPharma.Client.Infrastructure.Managers.MedicineSetup;
using EPharma.Client.Pages.UploadPrescription;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPharma.Client.Pages.Content
{
 
    public partial class Home
    {
        [Inject] private IMedicineSetupManager MedicineSetupManager { get; set; }
        private List<CategoryResponse> _CategoryList { get; set; } = new();
        private List<MedicineSetupResponseModel> _MedicineList { get; set; } = new();
        private ClaimsPrincipal _currentUser;
        private string? UserId { get; set; }= null;
        protected override async Task OnInitializedAsync()
        {
            await GetCategory();
            //UserId = _currentUser.Claims.Where(c => c.Type == "UserId")
            //      .Select(c => c.Value).SingleOrDefault();

        }
        public async Task GetCategory()
        {
            var Response = await MedicineSetupManager.GetAllCategory();
            if(Response.Succeeded && Response.Data!=null)
            {
              _CategoryList=Response.Data.ToList();
            }
        }
        public async Task GetTopProduct()
        {
            var Response = await MedicineSetupManager.GetAll();
            if(Response.Succeeded && Response.Data!=null)
            {
                _MedicineList=Response.Data.ToList();
                //_MedicineList = _MedicineList.OrderByDescending(x => x.Id).ToList();
            }

        }
        public async Task Upload()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
            {
                var parameters = new DialogParameters();
                var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
                var dialog = _dialogService.Show<UploadPreFile>("Upload Prescription", parameters, options);
                var result = await dialog.Result;
            }
            else
            {
                _navigationManager.NavigateTo("/NewLogin");
            }
        }
    }
}
