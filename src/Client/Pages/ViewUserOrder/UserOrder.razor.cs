using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using Blazored.LocalStorage;
using Blazored.FluentValidation;
using EPharma.Client.Infrastructure.Managers.Identity.Account;
using EPharma.Client.Infrastructure.Managers.Identity.Authentication;
using EPharma.Client.Infrastructure.Managers.Identity.Roles;
using EPharma.Client.Infrastructure.Managers.Identity.RoleClaims;
using EPharma.Client.Infrastructure.Managers.Identity.Users;
using EPharma.Client.Infrastructure.Managers.Preferences;
using EPharma.Client.Infrastructure.Managers.Interceptors;
using EPharma.Client.Infrastructure.Managers.Dashboard;
using EPharma.Client.Infrastructure.Managers.Communication;
using EPharma.Client.Infrastructure.Managers.Audit;
using EPharma.Shared.Settings;
using EPharma.Shared.Constants.Permission;
using EPharma.Client.Shared.Components;
using EPharma.Client;
using EPharma.Client.Shared;
using EPharma.Client.Shared.Dialogs;
using EPharma.Client.Infrastructure.Settings;
using EPharma.Application.Requests.Identity;
using EPharma.Client.Pages.Authentication;
using EPharma.Client.Infrastructure.Authentication;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.MedicineSetup;
using EPharma.Application.Responses;
using EPharma.Client.Pages.MedicineSetup;

namespace EPharma.Client.Pages.ViewUserOrder
{
   
    public partial class UserOrder
    {
        [Inject] private IMedicineSetupManager medicineSetup { get; set; }
        private string ImagePath;
        private List<UserOrderResponse> _userOrderList { get; set; } = new();
        private bool _loaded;
        private int value = 1;
        protected override async Task OnInitializedAsync()
        {
            await GetUserOrder();
            _loaded = true;
        }
        private async Task GetUserOrder()
        {
            var Data = await medicineSetup.GetAllUserOrder();
            if(Data.Succeeded)
            {
                _userOrderList = Data.Data.ToList();
                _userOrderList.ForEach(x =>
                {
                    x.SN = value;
                    if (x.IsApproved == 0)
                    {
                        x.ApproveStatus = "Pending";
                    }
                    else if (x.IsApproved == 1)
                    {
                        x.ApproveStatus = "Approved";
                    }
                    else
                    {
                        x.ApproveStatus = "Rejected";
                    }
                    if (x.IsDelivered == 0)
                    {
                        x.DeliveredStatus = "Not Delivered";
                    }
                    else if (x.IsDelivered == 1)
                    {
                        x.DeliveredStatus = "Delivered";
                    }
                    value = value + 1;
                });
            }

        }
        public async Task ViewImage(int Id)
        {
            ImagePath = _userOrderList.Where(x => x.Id == Id).Select(y => y.ImagePath).FirstOrDefault();
            var parameters = new DialogParameters();
            parameters.Add("ImagePath", ImagePath);
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<ViewImage>("Image", parameters, options);
            var result = await dialog.Result;
        }
        private async Task Reject()
        {
            
        }
        private async Task Accept()
        {
         
        }
    }
}