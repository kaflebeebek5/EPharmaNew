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
using EPharma.Application.Responses;
using EPharma.Application.Interfaces.MedicineSeup;
using System.IO;
using EPharma.Application.Requests;
using EPharma.Client.Infrastructure.Managers.MedicineSetup;
using EPharma.Application.Features.TableSetup.Queries.GetAll;
using EPharma.Client.Infrastructure.Managers.Settings.TableSetup;

namespace EPharma.Client.Pages.Content
{
    public partial class ProductFullDetails
    {
        [Parameter] public string Id { get; set; }

        [Inject] private IMedicineSetupManager medicineSetUpService { get; set; }
        [Inject] private ITableSetupManager TableSetupManager { get; set; }
        private MedicineSetupResponseModel _MedicineList { get; set; } = new();
        private List<MedicineSetupResponseModel> _MedicineList1 { get; set; } = new();
        private List<GetAllTableSetupResponse> _ProductList { get; set; } = new(); 
        private EPharma.Application.Requests.UploadPrescription UserRequest { get; set; }=new();
        private int? CatId;
        private UploadPrescriptionFile uploadReceipt { get; set; } = new();
        private string ImageName;
        protected override async Task OnInitializedAsync()
        {
           await GetProductById();
            await GetProductByCategory();
        //    var ProductResponse = await TableSetupManager.GetAllAsync("tblMedicine");
        //    if (ProductResponse.Succeeded)
        //    {
        //        _ProductList = ProductResponse.Data.ToList();
        //    }
        //    CatId = _ProductList.Where(x => x.Id == Convert.ToInt32(Id)).Select(x => x.CategorId).FirstOrDefault();
        }
        public async Task GetProductById()
        {
            var Response = await medicineSetUpService.GetByProductId(int.Parse(Id));
            if (Response.Succeeded && Response.Data != null)
            {
                _MedicineList = Response.Data;
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
                uploadReceipt = new UploadPrescriptionFile { Data = buffer, FileName = fileName, Extension = extension, UploadType = Application.Enums.UploadType.EPharmaPhotos };
            }
        }
        private void Check()
        {
            if(UserRequest.Quantity>_MedicineList.QuantityAvailable)
            {
                _snackBar.Add("Enter Valid Quantity",Severity.Warning);
                UserRequest.Quantity = 0;
            }
        }
        private async Task Save()
        {
            UserRequest.productId = _MedicineList.Id;
            UserRequest.uploadMedicine = uploadReceipt;
            UserRequest.Price = _MedicineList.SalePrice;
            if (UserRequest.Quantity == 0)
            {
                _snackBar.Add("Please Enter Quantity", Severity.Warning);
            }   
            else if(_MedicineList.IsActive==true && ImageName==null)
            {
                _snackBar.Add("Please Upload The Prescription", Severity.Warning);
            }
            else
            {
                var Message = await medicineSetUpService.SaveCart(UserRequest);
                if (Message.Succeeded)
                {
                    _snackBar.Add(Message.Messages[0], Severity.Success);
                    _navigationManager.NavigateTo("/viewcartdetails");
                }
                else
                {
                    foreach (var message in Message.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }

        }
        public async Task GetProductByCategory()
        {
            var Response = await medicineSetUpService.GetById(1);
            if (Response.Succeeded && Response.Data != null)
            {
                _MedicineList1 = Response.Data.ToList();
            }
        }

    }
}