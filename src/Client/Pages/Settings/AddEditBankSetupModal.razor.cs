using Blazored.FluentValidation;
using EPharma.Application.Features.BankSetup.Commands.AddEdit;
using EPharma.Application.Features.BankSetup.Queries.GetAll;
using EPharma.Application.Features.BankSetup.Queries.GetById;
using EPharma.Client.Extensions;
using EPharma.Client.Infrastructure.Managers.Settings.BankSetup;
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
    public partial class AddEditBankSetupModal
    {
        [Inject] private IBankSetupManager BankSetupManager { get; set; }

        [Parameter] public AddEditBankSetupCommand AddEditBankSetupModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        private List<GetBankSetupByIdResponse> _bankSetup = new();

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private bool resetValueOnEmptyText;
        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await BankSetupManager.SaveAsync(AddEditBankSetupModel);
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
            await LoadParentItemAsync();
        }
        private async Task LoadParentItemAsync()
        {
            var data = await BankSetupManager.GetParentItemAsync();
            if (data.Succeeded)
            {
                _bankSetup = data.Data;
                _bankSetup.Insert(0, new GetBankSetupByIdResponse() { Id = 0, Name = "Select" });
            }
        }
        private async Task<IEnumerable<int?>> ParentSearch(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return _bankSetup.Where(a => a.BankParentId == null).Select(x => (int?)x.Id);
            }
            else
            {
                return _bankSetup.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Where(a=>a.BankParentId==null)
                    .Select(x => (int?)x.Id);
            }
        }
    }
}
