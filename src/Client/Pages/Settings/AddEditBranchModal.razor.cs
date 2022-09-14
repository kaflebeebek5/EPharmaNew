using EPharma.Client.Extensions;
using EPharma.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using EPharma.Application.Features.Branch.Commands.AddEdit;
using EPharma.Client.Infrastructure.Managers.Settings.Branch;
using EPharma.Application.Features.Branch.Queries.GetAll;
using System.Collections.Generic;
using EPharma.Client.Infrastructure.Managers.Settings.TableSetup;
using EPharma.Application.Features.TableSetup.Queries.GetAll;
using System.Linq;
using System;

namespace EPharma.Client.Pages.Settings
{
    public partial class AddEditBranchModal
    {
        [Inject] private IBranchManager BranchManager { get; set; }

        [Parameter] public AddEditBranchCommand AddEditBranchModel { get; set; } = new();
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
            var response = await BranchManager.SaveAsync(AddEditBranchModel);
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
        }

        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}