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

namespace EPharma.Client.Pages.Category
{
    public partial class AddCategory
    {
    }
}