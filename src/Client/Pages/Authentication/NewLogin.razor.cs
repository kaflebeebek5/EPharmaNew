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
using System.Security.Claims;

namespace EPharma.Client.Pages.Authentication
{
    public partial class NewLogin
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private TokenRequest _tokenModel = new();

        protected override async Task OnInitializedAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            if (state == new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
            {
                _navigationManager.NavigateTo("/");
            }
            else
            {
                _navigationManager.NavigateTo("/newlogin");
            }
        }

        private async Task SubmitAsync()
        {
            var result = await _authenticationManager.Login(_tokenModel);
            if (result.Succeeded)
            {
                _snackBar.Add(string.Format(_localizer["Welcome {0}"], _tokenModel.Username), Severity.Success);
                _navigationManager.NavigateTo("/", true);
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
    }
}