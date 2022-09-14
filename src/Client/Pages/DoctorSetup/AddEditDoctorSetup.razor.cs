using EPharma.Client.Extensions;
using EPharma.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using EPharma.Application.Features.Genders.Commands.AddEdit;
using EPharma.Client.Infrastructure.Managers.Settings.Gender;
using EPharma.Client.Infrastructure.Managers.DoctorSetup;
using EPharma.Application.Requests;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using System;

namespace EPharma.Client.Pages.DoctorSetup
{
    public partial class AddEditDoctorSetup
    {
        [Inject] private IDoctorSetupManager DoctorSetupManager { get; set; }

        [Parameter] public DoctorSetupRequestModel AddEditDoctorModel { get; set; } = new();
        private UploadReceipt uploadReceipt { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private string ImageName;
        public void Cancel()
        {
            MudDialog.Cancel();
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
                uploadReceipt = new UploadReceipt { Data = buffer, FileName = fileName, Extension = extension, UploadType = Application.Enums.UploadType.EPharmaPhotos };
            }
        }
        private async Task SaveAsync()
        {
            AddEditDoctorModel.uploadReceipt = uploadReceipt;
            var response = await DoctorSetupManager.SaveDoctor(AddEditDoctorModel);
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
            ImageName = AddEditDoctorModel.ImagePath;
        }

        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
        }
      
    }
}