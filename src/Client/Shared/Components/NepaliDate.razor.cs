using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace  EPharma.Client.Shared.Components
{
    public partial class NepaliDate
    {
        [Parameter]
        public string LabelName { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        [Parameter]
        public string InitialValue { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }
        private Dictionary<string, object> InputAttributes { get; set; } = 
            new Dictionary<string, object>()
            {
               { "id", "datePicker" },
            };

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
        protected override async Task OnAfterRenderAsync(bool firstrender)
        {
            if (firstrender)
            {
                await JSRuntime.InvokeVoidAsync("LoadDatePicker") ;
            }
            if (!string.IsNullOrEmpty(InitialValue))
            {
                BindingValue = InitialValue;
            }
        }
    }
}
