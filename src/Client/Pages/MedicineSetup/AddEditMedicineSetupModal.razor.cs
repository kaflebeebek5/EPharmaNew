using EPharma.Client.Extensions;
using EPharma.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using EPharma.Client.Infrastructure.Managers.MedicineSetup;
using EPharma.Application.Requests;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using System;
using EPharma.Client.Infrastructure.Managers.Settings.TableSetup;
using EPharma.Application.Features.TableSetup.Queries.GetAll;
using System.Collections.Generic;
using System.Linq;

namespace EPharma.Client.Pages.MedicineSetup
{
    public partial class AddEditMedicineSetupModal
    {
        [Inject] private IMedicineSetupManager MedicineSetupManager { get; set; }
        [Inject] private ITableSetupManager TableSetupManager { get; set; }
        [Parameter] public MedicineRequestModel medicineRequestModel { get; set; } = new();
        private List<GetAllTableSetupResponse> _List { get; set; } = new();
        private UploadMedicine  uploadMedicine { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private string ImageName;
        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var Response = await TableSetupManager.GetAllAsync("tblSubCategory");
            if(Response.Succeeded)
            {
                _List = Response.Data.ToList();
            }
            medicineRequestModel.SubCategory = _List.Where(x => x.Id == medicineRequestModel.SubCategoryId).Select(x => x.Name).FirstOrDefault();
            medicineRequestModel.uploadMedicine = uploadMedicine;
            var response = await MedicineSetupManager.SaveMedicine(medicineRequestModel);
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
            ImageName = medicineRequestModel.ImagePath;
        }
        private IBrowserFile _file;
        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                ImageName = imageFile.Name;
                await imageFile.OpenReadStream().ReadAsync(buffer);
                uploadMedicine = new UploadMedicine { Data = buffer, FileName = fileName, Extension = extension, UploadType = Application.Enums.UploadType.EPharmaPhotos };
            }
        }
        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}