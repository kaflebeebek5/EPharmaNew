using EPharma.Application.Features.MasterTableSetup.Queries.GetAll;
using EPharma.Client.Extensions;
using EPharma.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EPharma.Application.Features.MasterTableSetup.Commands.AddEdit;
using EPharma.Client.Infrastructure.Managers.Settings.MasterTableSetup;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;

namespace EPharma.Client.Pages.Settings
{
    public partial class MasterTableSetup
    {
        [Inject] private IMasterTableSetupManager MasterTableSetupManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllMasterTableSetupResponse> _mastertablesetupList = new();
        private GetAllMasterTableSetupResponse _mastertablesetup = new();
        private string _searchString = "";

        private ClaimsPrincipal _currentUser;
        private bool _canCreateMasterTableSetup;
        private bool _canEditMasterTableSetup;
        private bool _canDeleteMasterTableSetup;
        private bool _canExportMasterTableSetup;
        private bool _canSearchMasterTableSetup;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateMasterTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MasterTableSetup.Create)).Succeeded;
            _canEditMasterTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MasterTableSetup.Edit)).Succeeded;
            _canDeleteMasterTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MasterTableSetup.Delete)).Succeeded;
            _canExportMasterTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MasterTableSetup.Export)).Succeeded;
            _canSearchMasterTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MasterTableSetup.Search)).Succeeded;

            await GetMasterTableSetupAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetMasterTableSetupAsync()
        {
            var response = await MasterTableSetupManager.GetAllAsync();
            if (response.Succeeded)
            {
                _mastertablesetupList = response.Data.ToList();
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
                var response = await MasterTableSetupManager.DeleteAsync(id);
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

        private async Task ExportToExcel()
        {
            var response = await MasterTableSetupManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(MasterTableSetup).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Master Table exported"]
                    : _localizer["Master Table exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _mastertablesetup = _mastertablesetupList.FirstOrDefault(c => c.Id == id);
                if (_mastertablesetup != null)
                {
                    parameters.Add(nameof(AddEditMasterTableSetupModal.AddEditMasterTableSetupModel), new AddEditMasterTableSetupCommand
                    {
                        Id = _mastertablesetup.Id,
                        TableName = _mastertablesetup.TableName,
                        ColumnId = _mastertablesetup.ColumnId,
                        ColumnName = _mastertablesetup.ColumnName,
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditMasterTableSetupModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _mastertablesetup = new GetAllMasterTableSetupResponse();
            await GetMasterTableSetupAsync();
        }

        private bool Search(GetAllMasterTableSetupResponse mastertablesetup)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (mastertablesetup.TableName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}