using EPharma.Application.Features.BankSetup.Commands.AddEdit;
using EPharma.Application.Features.BankSetup.Queries.GetAll;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.Settings.BankSetup;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPharma.Client.Pages.Settings
{
    public partial class BankSetup
    {
        [Inject] private IBankSetupManager BankSetupManager { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllBankSetupResponse> _bankSetupList = new();
        private GetAllBankSetupResponse _bankSetup = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateBankSetups;
        private bool _canEditBankSetups;
        private bool _canDeleteBankSetups;
        private bool _canExportBankSetups;
        private bool _canSearchBankSetups;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateBankSetups = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BankSetup.Create)).Succeeded;
            _canEditBankSetups = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BankSetup.Edit)).Succeeded;
            _canDeleteBankSetups = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BankSetup.Delete)).Succeeded;
            _canExportBankSetups = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BankSetup.Export)).Succeeded;
            _canSearchBankSetups = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BankSetup.Export)).Succeeded;

            await GetBankSetupsAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetBankSetupsAsync()
        {
            var response = await BankSetupManager.GetAllAsync();
            if (response.Succeeded)
            {
                _bankSetupList = response.Data.ToList();
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
            var parameter = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent,id) }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth=MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameter, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await BankSetupManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach(var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private async Task ExportToExcel()
        {
            var response = await BankSetupManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName= $"{nameof(BankSetup).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType=ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["BankSetup Exported"]
                    : _localizer["Filtered BankSetup Exported"], Severity.Success);
            }
            else
            {
                foreach(var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameter = new DialogParameters();
            if (id != 0)
            {
                _bankSetup = _bankSetupList.FirstOrDefault(c => c.Id == id);
                if (_bankSetup != null)
                {
                    parameter.Add(nameof(AddEditBankSetupModal.AddEditBankSetupModel), new AddEditBankSetupCommand
                    {
                        Id = _bankSetup.Id,
                        Name = _bankSetup.Name,
                        BranchName = _bankSetup.BranchName,
                        //BankParentId = _bankSetup.BankParentId
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditBankSetupModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameter, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }
        private async Task Reset()
        {
            _bankSetup = new GetAllBankSetupResponse();
            await GetBankSetupsAsync();
        }

        private bool Search(GetAllBankSetupResponse bankSetup)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (bankSetup.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}
