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
using EPharma.Client.Infrastructure.Managers.MedicineSetup;
using EPharma.Application.Responses;
using EPharma.Application.Requests;

namespace EPharma.Client.Pages.MedicineSetup
{
    public partial class MedicineSetup
    {
        [Inject] private IMedicineSetupManager MedicineSetupManager { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        private List<MedicineSetupResponseModel> _medicineList = new();
        private MedicineSetupResponseModel _medicine = new();
        private string _searchString = "";

        private ClaimsPrincipal _currentUser;
        private bool _canCreateMedicineSetup;
        private bool _canEditMedicineSetup;
        private bool _canDeleteMedicineSetup;
        private bool _canExportMedicineSetup;
        private bool _canSearchMedicineSetup;
        private bool _loaded;
        private string ImagePath;

        protected override async Task OnInitializedAsync()
        {
            await GetMedicineSetupAsync();
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MedicineSetup.Create)).Succeeded;
            _canEditMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MedicineSetup.Edit)).Succeeded;
            _canDeleteMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MedicineSetup.Delete)).Succeeded;
            _canExportMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MedicineSetup.Export)).Succeeded;
            _canSearchMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MedicineSetup.Search)).Succeeded;
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }

        }
        private async Task GetMedicineSetupAsync()
        {
            var response = await MedicineSetupManager.GetAll();
            if (response.Succeeded)
            {
                _medicineList = response.Data.ToList();
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
                var response = await MedicineSetupManager.DeleteMedicine(id);
                if (response.Succeeded)
                {
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetMedicineSetupAsync();
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
                _medicine = _medicineList.FirstOrDefault(c => c.Id == id);
                if (_medicine != null)
                {
                    parameters.Add(nameof(AddEditMedicineSetupModal.medicineRequestModel), new MedicineRequestModel
                    {
                        Id = _medicine.Id,
                        Name = _medicine.Name,
                        Description = _medicine.Description,
                        ManufactureDate = _medicine.ManufactureDate,
                        ExpiryDate = _medicine.ExpiryDate,
                        ImagePath = _medicine.ImagePath,
                        QuantityAvailable = _medicine.QuantityAvailable,
                        Unit=_medicine.Unit,
                        Manufacturer = _medicine.Manufacturer,
                        SalePrice = _medicine.SalePrice,
                        BuyPrice = _medicine.BuyPrice,
                        CategoryId = _medicine.CategoryId,
                        IsActive = _medicine.IsActive,
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditMedicineSetupModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            await GetMedicineSetupAsync();
        }
        private bool Search(MedicineSetupResponseModel Docor)
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
            ImagePath = _medicineList.Where(x => x.Id == Id).Select(y => y.ImagePath).FirstOrDefault();
            var parameters = new DialogParameters();
            parameters.Add("ImagePath", ImagePath);
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<ViewImage>(_localizer["Image"], parameters, options);
            var result = await dialog.Result;
        }
    }
}