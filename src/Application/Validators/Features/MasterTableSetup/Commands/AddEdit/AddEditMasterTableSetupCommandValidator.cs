using FluentValidation;
using EPharma.Application.Features.MasterTableSetup.Commands.AddEdit;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Validators.Features.MasterTableSetup.Commands.AddEdit
{
   public class AddEditMasterTableSetupCommandValidator : AbstractValidator<AddEditMasterTableSetupCommand>
    {
        public AddEditMasterTableSetupCommandValidator(IStringLocalizer<AddEditMasterTableSetupCommandValidator> localizer)
        {
            RuleFor(request => request.TableName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Table Name is required!"]);
            RuleFor(request => request.ColumnId)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Column ID is required!"]);
            RuleFor(request => request.ColumnName)
                   .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Column Name is required!"]);
        }
    }
}
