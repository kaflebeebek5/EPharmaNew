using FluentValidation;
using Microsoft.Extensions.Localization;
using EPharma.Application.Features.Branch.Commands.AddEdit;

namespace EPharma.Application.Validators.Features.Branch.Commands.AddEdit
{
    public class AddEditBranchCommandValidator : AbstractValidator<AddEditBranchCommand>
    {
        public AddEditBranchCommandValidator(IStringLocalizer<AddEditBranchCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.BranchTypeId)
                .Must(x => (x!=null && x != 0)).WithMessage(x => localizer["Branch Type is required!"]);
        }
    }
}