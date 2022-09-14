using Blazored.FluentValidation;
using EPharma.Application.Features.TableSetup.Commands.AddEdit;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.Settings.TableSetup;
using EPharma.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Client.Pages.Settings
{
    public partial class AddEditTableSetupModal
    {
        [Inject] private ITableSetupManager TableSetupManager { get; set; }
        [Parameter] public AddEditTableSetupCommand AddEditTableSetupModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
           
        }
        private async Task SaveAsync()
        {
            var response = await TableSetupManager.SaveAsync(AddEditTableSetupModel);
            if(response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach(var message in response.Messages)
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

        }

        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}
