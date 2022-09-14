using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EPharma.Application.Responses;
using EPharma.Client.Infrastructure.Managers.MedicineSetup;
using EPharma.Client.Pages.UploadPrescription;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace EPharma.Client.Pages.Cart
{
    public partial class CartItems
    {
        private bool Check;
        [Inject] private IMedicineSetupManager medicineSetUpManager { get; set; }
        private List<UserOrderResponse> _CartList { get; set; } = new();
        private bool _loaded;
        protected override async Task OnInitializedAsync()
        {
            Check = false;
            await GetUserCart();
            _loaded = true;
        }
        private async Task GetUserCart()
        {
            var Data = await medicineSetUpManager.GetAllcartOrder();
            if (Data.Succeeded)
            {
                _CartList = Data.Data.ToList();
            }
        }
        private async Task Cancel(int Id)
        {
            await medicineSetUpManager.CancleProduct(Id);
            await GetUserCart();
        }
        public async Task Upload()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
            {
                await GetUserCart();
                _CartList.ForEach(x =>
                {
                    if (x.Quantity > x.AvailableQuantity)
                    {
                        var data = string.Format("We only have {0} Product Left On our Database but you Ordered {1} Quantity", x.AvailableQuantity, x.Quantity);
                        _snackBar.Add(data, Severity.Warning);
                        Check = true;
                    }
                });
                if (Check == false)
                {
                    var parameters = new DialogParameters();
                    parameters.Add("SaveModel", _CartList);
                    var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
                    var dialog = _dialogService.Show<UploadPreFile>("Upload Prescription", parameters, options);
                    var result = await dialog.Result;
                    await GetUserCart();
                }
            }
            else
            {
                _navigationManager.NavigateTo("/NewLogin");
            }
        }
        public async Task CancelAll()
        {
            await medicineSetUpManager.CancleAllProduct();
            await GetUserCart();
        }
        public async Task Continue()
        {
            Check = false;
            _CartList.ForEach(x =>
            {
                if(x.Quantity > x.AvailableQuantity)
                {
                    x.Quantity= x.AvailableQuantity;
                }
            });
            await Upload();
        }
    }
}
