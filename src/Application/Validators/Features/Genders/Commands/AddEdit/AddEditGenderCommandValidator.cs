using FluentValidation;
using Microsoft.Extensions.Localization;
using EPharma.Application.Features.Genders.Commands.AddEdit;

namespace EPharma.Application.Validators.Features.Genders.Commands.AddEdit
{
    public class AddEditGenderCommandValidator : AbstractValidator<AddEditGenderCommand>
    {
        public AddEditGenderCommandValidator(IStringLocalizer<AddEditGenderCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
        }
    }
}