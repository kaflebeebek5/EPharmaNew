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
using EPharma.Application.Features.TableSetup.Queries.GetAll;
using EPharma.Client.Infrastructure.Managers.Settings.TableSetup;
using EPharma.Application.Features.TableSetup.Commands.AddEdit;

namespace EPharma.Client.Pages.Settings
{
    public partial class TableSetup
    {
        [Inject] private IMasterTableSetupManager MasterTableSetupManager { get; set; }
        [Inject] private ITableSetupManager TableSetupManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllMasterTableSetupResponse> _tablesetupList = new();

        private List<GetAllTableSetupResponse> _tableDetailList = new();
        [Parameter] public AddEditTableSetupCommand TableSetupCommand { get; set; } = new();


        private GetAllMasterTableSetupResponse _tablesetup = new();
        private GetAllTableSetupResponse _tableDetail = new();
        private string _searchString = "";

        private ClaimsPrincipal _currentUser;
        private bool _canCreateTableSetup;
        private bool _canEditTableSetup;
        private bool _canDeleteTableSetup;
        private bool _canExportTableSetup;
        private bool _canSearchTableSetup;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.TableSetup.Create)).Succeeded;
            _canEditTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.TableSetup.Edit)).Succeeded;
            _canDeleteTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.TableSetup.Delete)).Succeeded;
            _canExportTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.TableSetup.Export)).Succeeded;
            _canSearchTableSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.TableSetup.Search)).Succeeded;

            await GetTableSetupAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetTableSetupAsync()
        {
            var response = await MasterTableSetupManager.GetAllAsync();
            if (response.Succeeded)
            {
                _tablesetupList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        public async Task CreateTableData()
        {
            var response = await TableSetupManager.SaveAsync(TableSetupCommand);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        public async Task GetTableDetailsAsync(string tableName)
        {
            TableSetupCommand.TableName = tableName;
            var response = await TableSetupManager.GetAllAsync(tableName);
            if(response.Succeeded)
            {
                _tableDetailList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private async Task Delete(int id,string tableName)
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
                var response = await TableSetupManager.DeleteAsync(id,tableName);
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

        //private async Task ExportToExcel()
        //{
        //    var response = await MasterTableSetupManager.ExportToExcelAsync(_searchString);
        //    if (response.Succeeded)
        //    {
        //        await _jsRuntime.InvokeVoidAsync("Download", new
        //        {
        //            ByteArray = response.Data,
        //            FileName = $"{nameof(MasterTableSetup).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
        //            MimeType = ApplicationConstants.MimeTypes.OpenXml
        //        });
        //        _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
        //            ? _localizer["Master Table exported"]
        //            : _localizer["Master Table exported"], Severity.Success);
        //    }
        //    else
        //    {
        //        foreach (var message in response.Messages)
        //        {
        //            _snackBar.Add(message, Severity.Error);
        //        }
        //    }
        //}

        private async Task InvokeModal(int id = 0,string tableName="")
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _tablesetup = _tablesetupList.FirstOrDefault(c => c.TableName ==tableName);
                _tableDetail = _tableDetailList.FirstOrDefault(c=>c.Id ==id);
                if (_tablesetup != null)
                {
                    parameters.Add(nameof(AddEditTableSetupModal.AddEditTableSetupModel), new AddEditTableSetupCommand
                    {
                        Id = id,
                        TableName = TableSetupCommand.TableName,
                        Name = _tableDetail.Name,
                    }); ;
                }
            }
            else {
                parameters.Add(nameof(AddEditTableSetupModal.AddEditTableSetupModel), new AddEditTableSetupCommand
                {
                    Id = TableSetupCommand.Id,
                    TableName = TableSetupCommand.TableName,
                });
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditTableSetupModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _tablesetup = new GetAllMasterTableSetupResponse();
            await GetTableDetailsAsync(TableSetupCommand.TableName);
        }

        private bool Search(GetAllTableSetupResponse tablesetup)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (tablesetup.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
        private async Task<IEnumerable<string>> SearchTable(string value)
        {

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _tablesetupList.Select(x => x.TableName);

            return _tablesetupList.Where(x => x.TableName.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.TableName);
        }
    }
}