using AutoMapper;
using EPharma.Application.Requests.Identity;
using EPharma.Application.Responses.Identity;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.Identity.MenuRole;
using EPharma.Client.Infrastructure.Managers.Identity.Roles;
using EPharma.Client.Infrastructure.Mappings;
using EPharma.Client.Pages.Identity;
using EPharma.Shared.Constants.Application;
using EPharma.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPharma.Client.Pages.Identity
{
    public partial class MenuRole
    {
        [Inject] private IRoleManager RoleManager { get; set; }
        [Inject] private IMenuRoleManager MenuRoleManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private ClaimsPrincipal _currentUser;
        private bool _canCreateMenuRole;
        private bool _canEditMenuRole;
        private bool _canDeleteMenuRole;
        private bool _canSearchMenuRole;
        private List<RoleResponse> _Role = new();
        private List<MenuRoleResponse> _menuRole = new();
        private RoleResponse _menu = new();
        private IMapper _mapper;
        private string _searchString = "";
        private string value;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateMenuRole = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MenuRole.Create)).Succeeded;
            _canEditMenuRole = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MenuRole.Edit)).Succeeded;
            _canDeleteMenuRole = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MenuRole.Delete)).Succeeded;
            _canSearchMenuRole = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.MenuRole.Search)).Succeeded;
            await GetMenuRoleAsync();
            //await GetMenuListAsync("7e774d07-514b-4f0f-88ad-1449237bcf27");
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }
        private async Task GetMenuRoleAsync()
        {
            var response = await RoleManager.GetRolesAsync();
            if (response.Succeeded)
            {
                _Role = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private async Task GetMenuListAsync(string Id)
        {
            value = Id;
            _mapper = new MapperConfiguration(c => { c.AddProfile<MenuRoleProfile>(); }).CreateMapper();
            var response = await MenuRoleManager.GetByIdAsync(Id);
            if (response.Succeeded)
            {
                _menuRole = response.Data.ToList();
                foreach (var item in _menuRole)
                {
                    if (item.RoleId == null)
                    {
                        item.RoleId = Id;
                    }
                }
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private void onchange(bool x)
        {
            foreach (var item in _menuRole)
            {
                if (x == true)
                {
                    item.IsChecked = true;
                }
                else
                {
                    item.IsChecked = false;
                }
            }
        }

        private async Task BtnSave()
        {
            //Expression<Func<MenuRole, MenuRoleResponse>> expression = e => new MenuRoleResponse
            //{
            //   Id=e._RoleMenu.Id,
            //   RoleId=e._RoleMenu.RoleId,
            //   MenuId=e._RoleMenu.MenuId,
            //   IsChecked=e._RoleMenu.IsChecked
            //};
            var request = _mapper.Map<List<MenuRoleResponse>, List<MenuRoleRequest>>(_menuRole);
            var result = await MenuRoleManager.SaveAsync(request);
            if (result.Succeeded)
            {
                _snackBar.Add(result.Messages[0], Severity.Success);
                await GetMenuListAsync(value);
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
        }

        private async Task Reset()
        {
            await GetMenuListAsync(value);
        }

        //private bool Search(MenuRoleResponse MenuRoleList)
        //{
        //    if (string.IsNullOrWhiteSpace(_searchString)) return true;
        //    if (MenuRoleList.RoleId?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
