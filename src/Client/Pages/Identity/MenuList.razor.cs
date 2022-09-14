using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.Identity.MenuList;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPharma.Client.Pages.Identity
{
    public partial class MenuList
    {
        [Inject] private IMenuListManager MenuManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private ClaimsPrincipal _currentUser;
        private bool _canCreateMenuList;
        private bool _canEditMenuList;
        private bool _canDeleteMenuList;
        private bool _canSearchMenuList;
        private bool _loaded;
        private List<MenuListResponse> _hrmenuList = new();
        private MenuListResponse _hrmenu = new();
        private string _searchString = "";


        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateMenuList = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MenuList.Create)).Succeeded;
            _canEditMenuList = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MenuList.Edit)).Succeeded;
            _canDeleteMenuList = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MenuList.Delete)).Succeeded;
            _canSearchMenuList = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MenuList.Search)).Succeeded;
            _loaded = true;
            await GetHrMenuListAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }
        private async Task GetHrMenuListAsync()
        {
            var response = await MenuManager.GetAllAsync();
            if (response.Succeeded)
            {
                _hrmenuList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await MenuManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }


        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _hrmenu = _hrmenuList.FirstOrDefault(c => c.Id == id);
                if (_hrmenu != null)
                {
                    parameters.Add(nameof(AddEditHrMenuListModal.AddEditHrMenuListModel), new MenuListRequest
                    {
                        Id = _hrmenu.Id,
                        MenuName = _hrmenu.MenuName,
                        MenuNameNepali = _hrmenu.MenuNameNepali,
                        ParentId = _hrmenu.ParentId,
                        Icon = _hrmenu.Icon,
                        Path = _hrmenu.Path,
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditHrMenuListModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _hrmenu = new MenuListResponse();
            await GetHrMenuListAsync();
        }

        private bool Search(MenuListResponse hrMenuList)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (hrMenuList.MenuName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}

