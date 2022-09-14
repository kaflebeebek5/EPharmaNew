using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using Blazored.LocalStorage;
using Blazored.FluentValidation;
using EPharma.Client.Infrastructure.Managers.Identity.Account;
using EPharma.Client.Infrastructure.Managers.Identity.Authentication;
using EPharma.Client.Infrastructure.Managers.Identity.Roles;
using EPharma.Client.Infrastructure.Managers.Identity.RoleClaims;
using EPharma.Client.Infrastructure.Managers.Identity.Users;
using EPharma.Client.Infrastructure.Managers.Preferences;
using EPharma.Client.Infrastructure.Managers.Interceptors;
using EPharma.Client.Infrastructure.Managers.Dashboard;
using EPharma.Client.Infrastructure.Managers.Communication;
using EPharma.Client.Infrastructure.Managers.Audit;
using EPharma.Shared.Settings;
using EPharma.Shared.Constants.Permission;
using EPharma.Client.Shared.Components;
using EPharma.Client;
using EPharma.Client.Shared;
using EPharma.Client.Shared.Dialogs;
using EPharma.Client.Infrastructure.Settings;
using EPharma.Application.Requests.Identity;
using EPharma.Client.Pages.Authentication;
using EPharma.Client.Infrastructure.Authentication;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.MedicineSetup;
using System.IO;
using EPharma.Application.Requests;
using EPharma.Application.Responses;

namespace EPharma.Client.Pages.UploadPrescription
{
    public partial class UploadPreFile
    {
        private bool OTPCheck;
        private bool OTPDisable;
        private bool IsSelected;
        private bool OTPMatched;
        private string ImageName;

        private EPharma.Application.Requests.UploadPrescriptionFile uploadPrescriptionFile { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public List<UserOrderResponse> SaveModel { get; set; } = new();
        [Inject] private IMedicineSetupManager medicineSetup { get; set; }
        private MultipleRequestModel MultipleItems { get; set; } = new();
        private EPharma.Application.Requests.UploadPrescription Upload { get; set; } = new();
        private OTPModel OTPModel { get; set; }=new();
        protected override async Task OnInitializedAsync()
        {
           
            OTPCheck = false;
            IsSelected = true;
            OTPDisable = false;
            OTPMatched = false;
            if (SaveModel.Count > 0)
            {
                IsSelected = false;
            }
        }
        public async Task SendOTP()
        {
            if(Upload.PhoneNumber == null)
            {
                _snackBar.Add("Please Enter Phone Number", Severity.Warning);
            }
            else
            {
                int _min = 0000;
                int _max = 9999;
                Random _rdm = new Random();
                var Number = _rdm.Next(_min, _max);
                Upload.RandomOTP=Number.ToString();
                var Response = await medicineSetup.SaveOTP(Upload);
                if (Response.Succeeded)
                {
                    OTPDisable=true;
                    _snackBar.Add(Response.Messages[0], Severity.Success);
                }
                else
                {
                    foreach (var message in Response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
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
                uploadPrescriptionFile = new EPharma.Application.Requests.UploadPrescriptionFile { Data = buffer, FileName = fileName, Extension = extension, UploadType = Application.Enums.UploadType.EPharmaPhotos };
            }
        }
        private async Task SaveAsync()
        {
            if (SaveModel.Count == 0)
            {
                Upload.uploadMedicine = uploadPrescriptionFile;
                if (Upload.BillingAddress == null)
                {
                    _snackBar.Add("Please Enter Billing Address!", Severity.Warning);
                }
                if (Upload.uploadMedicine == null)
                {
                    _snackBar.Add("Please Upload the Prescription!", Severity.Warning);
                }
                else if (OTPMatched == false)
                {
                    _snackBar.Add("OTP Verification is not Completed Yet", Severity.Warning);
                }
                else
                {

                    var response = await medicineSetup.SavePrescription(Upload);
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
                }
            }
            else
            {
                if (Upload.BillingAddress == null)
                {
                    _snackBar.Add("Please Enter Billing Address!", Severity.Warning);
                }
                else if (OTPMatched == false)
                {
                    _snackBar.Add("OTP Verification is not Completed Yet", Severity.Warning);
                }
                else
                {
                    MultipleItems.BillingAddress = Upload.BillingAddress;
                    MultipleItems.PhoneNumber=Upload.PhoneNumber;
                    MultipleItems.Remarks=Upload.Remarks;
                    MultipleItems.MultipleReq = SaveModel;
                    var response = await medicineSetup.SaveCartOrder(MultipleItems);
                    if (response.Succeeded)
                    {
                        await medicineSetup.CancleAllProduct();
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

                }
            }
        }
        public void Checked(bool Value)
        {
            if (SaveModel.Count == 0)
            {
                if (Value == true)
                    IsSelected = false;
                else
                    IsSelected = true;
            }
            else
            {
                IsSelected = false;
            }
        }
        public async void CheckOTPValid()
        {
            var Response = await medicineSetup.GetOtp(Upload.PhoneNumber);
            if(Response.Succeeded)
            {
                OTPModel = Response.Data;
                if(OTPModel.OTP!=Upload.OTP)
                {
                    _snackBar.Add("OTP Not Matched",Severity.Error);
                }
                else if(OTPModel.Time>60)
                {
                    _snackBar.Add("OTP Not Valid Resend OTP", Severity.Error);
                    OTPDisable = false;
                }
                else if(OTPModel.OTP==Upload.OTP && OTPModel.Time<=60)
                {
                    _snackBar.Add("OTP MATCHED", Severity.Success);
                    OTPMatched = true;
                    await medicineSetup.UpdateOTP(Upload.PhoneNumber);
                }
            }
        }
    }
}