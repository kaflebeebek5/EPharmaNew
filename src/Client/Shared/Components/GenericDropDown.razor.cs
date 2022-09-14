using EPharma.Application.Features.TableSetup.Queries.GetAll;
using EPharma.Client.Infrastructure.Managers.Settings.TableSetup;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharma.Client.Shared.Components
{
    public partial class GenericDropDown
    {
        [Parameter]
        public string TableName { get; set; }
        [Parameter]
        public string LabelName { get; set; }
        [Parameter]
        public Margin Margin { get; set; }
        [Parameter]
        public Variant Variant { get; set; }
        [Parameter]
        public string InitialValue { get; set; }
        [Parameter]
        public bool All { get; set; } = false;
        [Parameter]
        public bool Select { get; set; } = false;

        [Inject] private ITableSetupManager TableSetupManager { get; set; }
        private List<GetAllTableSetupResponse> _items = new();
        private GetAllTableSetupResponse _tablesetup = new();

        private string _value;

        public string BindingValue
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDropDown();
            if (string.IsNullOrEmpty(LabelName))
            {
                LabelName = TableName;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await LoadDropDown();
            if (string.IsNullOrEmpty(LabelName))
            {
                LabelName = TableName;
            }
            if (!string.IsNullOrEmpty(InitialValue))
            {
                BindingValue = InitialValue;
            }
            //else
            //{
            //    BindingValue = "";
            //}
        }
        private async Task LoadDropDown()
        {
            var response = await TableSetupManager.GetAllAsync(TableName);
            if (response.Succeeded)
            {
                _items = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
            if (All)
            {
                _items.Insert(0, new GetAllTableSetupResponse() { Id = -1, Name = "All" });
            }
            if (Select)
            {
                _items.Insert(0, new GetAllTableSetupResponse() { Id = 0, Name = "Select" });
            }

            return;
        }
        private async Task<IEnumerable<string>> SearchItems(string value)
        {
            if (string.IsNullOrEmpty(value))
                return _items.Select(x => x.Id.ToString());

            return _items.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id.ToString());
        }
    }
}
