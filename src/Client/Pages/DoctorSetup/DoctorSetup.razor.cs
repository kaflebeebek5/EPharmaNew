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
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using EPharma.Client.Infrastructure.Managers.DoctorSetup;
using EPharma.Application.Responses;
using EPharma.Application.Requests;

namespace EPharma.Client.Pages.DoctorSetup
{
    public partial class DoctorSetup
    {
        [Inject] private IDoctorSetupManager doctorSetupManager{ get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        private List<DoctoSetupResponse> _doctorList = new();
        private DoctoSetupResponse _doctor = new();
        private string _searchString = "";
        private ClaimsPrincipal _currentUser;
        private bool _canCreateDoctorSetup;
        private bool _canEditDoctorSetup;
        private bool _canDeleteDoctorSetup;
        private bool _canExportDoctorSetup;
        private bool _canSearchDoctorSetup;
        private string ImagePath;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            await GetDoctorSetupAsync();
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DoctorSetup.Create)).Succeeded;
            _canEditDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DoctorSetup.Edit)).Succeeded;
            _canDeleteDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DoctorSetup.Delete)).Succeeded;
            _canExportDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DoctorSetup.Export)).Succeeded;
            _canSearchDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.DoctorSetup.Search)).Succeeded;
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
           

        }
        private async Task GetDoctorSetupAsync()
        {
            var response = await doctorSetupManager.GetAll();
            if (response.Succeeded)
            {
                _doctorList = response.Data.ToList();
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
            string deleteContent = "Delete Content";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await doctorSetupManager.DeleteDoctor(id);
                if (response.Succeeded)
                {
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetDoctorSetupAsync();
                }
                else
                {
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        //private async Task ExportToExcel()
        //{
        //    var response = await GenderManager.ExportToExcelAsync(_searchString);
        //    if (response.Succeeded)
        //    {
        //        await _jsRuntime.InvokeVoidAsync("Download", new
        //        {
        //            ByteArray = response.Data,
        //            FileName = $"{nameof(Genders).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
        //            MimeType = ApplicationConstants.MimeTypes.OpenXml
        //        });
        //        _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
        //            ? _localizer["Genders exported"]
        //            : _localizer["Filtered Genders exported"], Severity.Success);
        //    }
        //    else
        //    {
        //        foreach (var message in response.Messages)
        //        {
        //            _snackBar.Add(message, Severity.Error);
        //        }
        //    }
        //}

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _doctor = _doctorList.Where(x => x.Id == id).FirstOrDefault();
                if (_doctor != null)
                {
                    parameters.Add(nameof(AddEditDoctorSetup.AddEditDoctorModel), new DoctorSetupRequestModel
                    {
                        Id = _doctor.Id,
                        Name = _doctor.Name,
                        Address=_doctor.Address,
                        GenderId=_doctor.GenderId,
                        Specialist=_doctor.Specialist,
                        Email=_doctor.Email,
                        PhoneNumber=_doctor.PhoneNumber,
                        ImagePath=_doctor.ImagePath,
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditDoctorSetup>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            await GetDoctorSetupAsync();
        }
        private bool Search(DoctoSetupResponse Docor)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (Docor.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
        public async Task ViewImage(int Id)
        {
            ImagePath = _doctorList.Where(x => x.Id == Id).Select(y => y.ImagePath).FirstOrDefault();
            var parameters = new DialogParameters();
            parameters.Add("ImagePath", ImagePath);
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<ViewImage>(_localizer["Image"], parameters, options);
            var result = await dialog.Result;
        }
    }
}