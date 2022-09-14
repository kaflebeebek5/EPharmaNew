using EPharma.Application.Responses;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.UserMedicineOrder;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPharma.Client.Pages.UserMedicineOrder
{
    public partial class UserMedicineOrder
    {
        [Inject] private IUserMedicineOrderManager UserMedicineOrderManager { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        private List<UserMedicineDetails> _userMedicineList { get; set; } = new();
        private string _searchString = "";
        private ClaimsPrincipal _currentUser;
        private bool _canCreateDoctorSetup;
        private bool _canEditDoctorSetup;
        private bool _canDeleteDoctorSetup;
        private bool _canExportDoctorSetup;
        private bool _canSearchDoctorSetup;
        private string ImagePath;
        private bool _loaded;
        private string UserId;
        protected override async Task OnInitializedAsync()
        {
            await GetUserMedicine();
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.UserMedicine.Create)).Succeeded;
            _canEditDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.UserMedicine.Edit)).Succeeded;
            _canDeleteDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.UserMedicine.Delete)).Succeeded;
            _canExportDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.UserMedicine.Export)).Succeeded;
            _canSearchDoctorSetup = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.UserMedicine.Search)).Succeeded;
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
            UserId = _currentUser.Claims.Where(c => c.Type == "NameIdentifier")
             .Select(c => c.Value).SingleOrDefault();
        }

        public async Task GetUserMedicine()
        {
            var Response = await UserMedicineOrderManager.GetUserMedicine(UserId);
            if(Response.Succeeded)
            {
                _userMedicineList=Response.Data.ToList();
            }
        }
    }
}
