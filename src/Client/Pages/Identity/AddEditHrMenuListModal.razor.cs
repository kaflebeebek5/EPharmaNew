using Blazored.FluentValidation;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.Identity.MenuList;
using EPharma.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Client.Pages.Identity
{
    public partial class AddEditHrMenuListModal
    {
        [Inject] private IMenuListManager HrMenuManager { get; set; }

        [Parameter] public MenuListRequest AddEditHrMenuListModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        private List<MenuListResponse> _menuList = new();
        private MenuListResponse _hrmenu = new();
        private bool resetValueOnEmptyText;

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await HrMenuManager.SaveAsync(AddEditHrMenuListModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
            await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
            await LoadParentItemAsync();
        }
        private async Task LoadParentItemAsync()
        {
            var data = await HrMenuManager.GetParentItemAsync();
            if (data.Succeeded)
            {
                _menuList = data.Data;
                _menuList.Insert(0, new MenuListResponse() { Id = 0, MenuName = "Select" });

            }
        }
        private async Task<IEnumerable<int?>> ParentSearch(string value)
        {
            if (string.IsNullOrEmpty(value))
                return _menuList.Select(x => (int?)x.Id);

            return _menuList.Where(x => x.MenuName.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => (int?)x.Id);

        }

    }
}
