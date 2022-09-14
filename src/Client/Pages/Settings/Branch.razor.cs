using EPharma.Application.Features.Branch.Queries.GetAll;
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
using EPharma.Application.Features.Branch.Commands.AddEdit;
using EPharma.Client.Infrastructure.Managers.Settings.Branch;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;

namespace EPharma.Client.Pages.Settings
{
    public partial class Branch
    {
        [Inject] private IBranchManager BranchManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllBranchResponse> _branchList = new();
        private GetAllBranchResponse _branch = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateBranch;
        private bool _canEditBranch;
        private bool _canDeleteBranch;
        private bool _canExportBranch;
        private bool _canSearchBranch;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateBranch = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Branch.Create)).Succeeded;
            _canEditBranch = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Branch.Edit)).Succeeded;
            _canDeleteBranch = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Branch.Delete)).Succeeded;
            _canExportBranch = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Branch.Export)).Succeeded;
            _canSearchBranch = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Branch.Search)).Succeeded;

            await GetBranchAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetBranchAsync()
        {
            var response = await BranchManager.GetAllAsync();
            if (response.Succeeded)
            {
                _branchList = response.Data.ToList();
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
                var response = await BranchManager.DeleteAsync(id);
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
            var response = await BranchManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Branch).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Branch exported"]
                    : _localizer["Filtered Branch exported"], Severity.Success);
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
                _branch = _branchList.FirstOrDefault(c => c.Id == id);
                if (_branch != null)
                {
                    parameters.Add(nameof(AddEditBranchModal.AddEditBranchModel), new AddEditBranchCommand
                    {
                        Id = _branch.Id,
                        Name = _branch.Name,
                        NameNepali = _branch.NameNepali,
                        Code = _branch.Code,
                        NRBCode = _branch.NRBCode,
                        OperationDate = _branch.OperationDate,
                        ParentBranchId = _branch.ParentBranchId,
                        BranchTypeId = _branch.BranchTypeId,
                        ProvinceId = _branch.ProvinceId,
                        DistrictId = _branch.DistrictId,
                        LocalBodiesId = _branch.LocalBodiesId,
                        Locality = _branch.Locality,
                        WardNo = _branch.WardNo,
                        PhoneNo = _branch.PhoneNo,
                        Email = _branch.Email
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditBranchModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _branch = new GetAllBranchResponse();
            await GetBranchAsync();
        }

        private bool Search(GetAllBranchResponse branch)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (branch.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}