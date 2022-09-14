using EPharma.Application.Features.Genders.Queries.GetAll;
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
using EPharma.Application.Features.Genders.Commands.AddEdit;
using EPharma.Client.Infrastructure.Managers.Settings.Gender;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;

namespace EPharma.Client.Pages.Settings
{
    public partial class Genders
    {
        [Inject] private IGenderManager GenderManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllGendersResponse> _genderList = new();
        private GetAllGendersResponse _gender = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateGenders;
        private bool _canEditGenders;
        private bool _canDeleteGenders;
        private bool _canExportGenders;
        private bool _canSearchGenders;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateGenders = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Genders.Create)).Succeeded;
            _canEditGenders   = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Genders.Edit)).Succeeded;
            _canDeleteGenders = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Genders.Delete)).Succeeded;
            _canExportGenders = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Genders.Export)).Succeeded;
            _canSearchGenders = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Genders.Search)).Succeeded;

            await GetGendersAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetGendersAsync()
        {
            var response = await GenderManager.GetAllAsync();
            if (response.Succeeded)
            {
                _genderList = response.Data.ToList();
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
                var response = await GenderManager.DeleteAsync(id);
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
            var response = await GenderManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Genders).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Genders exported"]
                    : _localizer["Filtered Genders exported"], Severity.Success);
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
                _gender = _genderList.FirstOrDefault(c => c.Id == id);
                if (_gender != null)
                {
                    parameters.Add(nameof(AddEditGenderModal.AddEditGenderModel), new AddEditGenderCommand
                    {
                        Id = _gender.Id,
                        Name = _gender.Name,
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditGenderModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _gender = new GetAllGendersResponse();
            await GetGendersAsync();
        }

        private bool Search(GetAllGendersResponse gender)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (gender.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}