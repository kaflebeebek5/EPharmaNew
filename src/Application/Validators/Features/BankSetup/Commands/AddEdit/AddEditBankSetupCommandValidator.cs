using FluentValidation;
using EPharma.Application.Features.BankSetup.Commands.AddEdit;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Validators.Features.BankSetup.Commands.AddEdit
{
    public class AddEditBankSetupCommandValidator : AbstractValidator<AddEditBankSetupCommand>         
    {
        public  AddEditBankSetupCommandValidator(IStringLocalizer<AddEditBankSetupCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["BankName Is Required"]);
            RuleFor(request => request.BranchName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["BranchName Is Required"]);
        }
    }
}
