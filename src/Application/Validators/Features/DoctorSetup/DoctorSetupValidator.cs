using EPharma.Application.Requests;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Validators.Features.DoctorSetup
{
    public class DoctorSetupValidator : AbstractValidator<DoctorSetupRequestModel>
    {
        public DoctorSetupValidator(IStringLocalizer<DoctorSetupValidator> localizer)
        {
            RuleFor(request => request.Email)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Email is required"])
                .EmailAddress().WithMessage(x => localizer["Email is not correct"]);
        }
    }
}
