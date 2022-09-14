using EPharma.Application.Requests.Identity;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EPharma.Application.Validators.Requests.Identity
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator(IStringLocalizer<TokenRequestValidator> localizer)
        {
            RuleFor(request => request.Username)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Username is required!"]);
            RuleFor(request => request.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password is required!"]);
        }
    }
}
