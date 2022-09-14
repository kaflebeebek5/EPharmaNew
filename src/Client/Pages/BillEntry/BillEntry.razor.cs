using EPharma.Application.Features.TableSetup.Queries.GetAll;
using EPharma.Application.Requests;
using EPharma.Application.Requests.MedicineDetails;
using EPharma.Application.Responses;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.BillEntry;
using EPharma.Client.Infrastructure.Managers.Settings.TableSetup;
using EPharma.Client.Infrastructure.Managers.UserMedicineOrder;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPharma.Client.Pages.BillEntry
{
    public partial class BillEntry
    {
        [Inject] private IBillEntryManager BillEntryManager { get; set; }
        [Inject] private ITableSetupManager tableSetupManager { get; set; }
        [Inject] private IUserMedicineOrderManager UserManager { get; set; }
        private MedicineDetails AddEditMedicineDetails { get; set; } = new();
        private Medicine medicine { get; set; } = new();
        private BillEntryRequestModel _billEntryRequestModel { get; set; } = new();
        private UserMedicines _userMedicine { get; set; } = new();
        private List<BillEntryResponseModel> _billEntryList { get; set; } = new();
        private BillNumberberResponsecs billNumber { get; set; } = new();
        private List<UserResponseModel> _userList { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        private List<GetAllTableSetupResponse> _getMedicineList = new();
        private string _searchString = "";

        private ClaimsPrincipal _currentUser;
        private bool _canCreateMedicineSetup;
        private bool _canEditMedicineSetup;
        private bool _canDeleteMedicineSetup;
        private bool _canExportMedicineSetup;
        private bool _canSearchMedicineSetup;
        private int value=1;
        private bool _loaded;
        private bool addbutton=true;
        public bool updatebutton=false;
        private string BillNO;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BillEntry.Create)).Succeeded;
            _canEditMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BillEntry.Edit)).Succeeded;
            _canDeleteMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BillEntry.Delete)).Succeeded;
            _canExportMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BillEntry.Export)).Succeeded;
            _canSearchMedicineSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.BillEntry.Search)).Succeeded;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
            await GetBillNumber();
            await GetAllUser();
            var Response = await tableSetupManager.GetAllAsync("TblMedicine");
            if (Response.Succeeded)
            {
                _getMedicineList = Response.Data.ToList();
            }
            _loaded = true;

        }
        public async Task GetBillNumber()
        {
            var Response = await BillEntryManager.GetBillNumber();
            if(Response.Succeeded)
            {
                BillNO = Response.Messages[0].ToString();
                _billEntryRequestModel.BillNumber = BillNO;

            }
        }
        public async Task GetAllUser()
        {
            var Response= await BillEntryManager.GetAllUser();
            if(Response.Succeeded)
            {
                _userList=Response.Data.ToList();
            }
        }
        private async Task<IEnumerable<string>> SearchUsers(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _userList.Select(x => x.Id);

            return _userList.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }
        private void adddata()
        {
            _userMedicine.SN = value;
            _billEntryRequestModel.MedicineList.Add(new UserMedicines
            {
               SN=_userMedicine.SN,
               MedicineId=_userMedicine.MedicineId,
               MedicineName=_getMedicineList.Where(x=>x.Id==_userMedicine.MedicineId).Select(x=>x.Name).FirstOrDefault(),
               Quantity=_userMedicine.Quantity,
               Timing=_userMedicine.Timing,
            });
            value = value + 1;
            _userMedicine.Quantity = 0;
            _userMedicine.MedicineName = "";
            _userMedicine.MedicineId = 0;
            _userMedicine.Timing = "";
            //LumberEntryList.Add(new AddEditLumberEntryDetailsCommand());

        }
        private void EditData(UserMedicines id)
        {
            _userMedicine.SN = id.SN;
            _userMedicine.MedicineName = id.MedicineName;
            _userMedicine.Quantity = id.Quantity;
            _userMedicine.Timing = id.Timing;
            _userMedicine.MedicineId=id.MedicineId;
            updatebutton = true;
            addbutton = false;
        }
        private void Updatedata()
        {
            _billEntryRequestModel.MedicineList.Where(x => x.SN == _userMedicine.SN).ToList().ForEach(x =>
            {
                x.SN = _userMedicine.SN;
                x.MedicineName = _getMedicineList.Where(x => x.Id == _userMedicine.MedicineId).Select(x => x.Name).FirstOrDefault();
                x.Quantity = _userMedicine.Quantity;
                x.Timing = _userMedicine.Timing;
                x.MedicineId=_userMedicine.MedicineId;
            });
            updatebutton = false;
            addbutton = true;
            _userMedicine.Quantity = 0;
            _userMedicine.MedicineName = "";
            _userMedicine.MedicineId = 0;
            _userMedicine.Timing = "";
        }
        private async Task deletedata(UserMedicines id)
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

                _billEntryRequestModel.MedicineList.Remove(id);
                updatebutton = false;
                addbutton = true;
                medicine.Quantity =0;
                medicine.MedicineName = "";
            }
        }
        private async Task SaveData()
        {
               string saveContent = _localizer["Do You Want to Send?"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.SaveConfirmation.ContentText), string.Format(saveContent)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.SaveConfirmation>(_localizer["Save"], parameters, options);
            var result = await dialog.Result;
            var Response = await UserManager.SaveBillEntry(_billEntryRequestModel);
            if(Response.Succeeded)
            {
                await UserManager.UpdateStartingNumber();
                _snackBar.Add("Data Saved", Severity.Success);
                _billEntryRequestModel.MedicineList.Clear();
                _billEntryRequestModel.BillNumber = "";
                _billEntryRequestModel.UserId = "";
            }
            else
            {
                _snackBar.Add(Response.Messages[0], Severity.Error);

            }
        }
    }

}
