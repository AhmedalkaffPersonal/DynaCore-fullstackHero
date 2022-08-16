using DynaCore.Application.Features.Vendors.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DynaCore.Application.Validators.Features.Vendors.Commands.AddEdit
{
    public class AddEditVendorCommandValidator : AbstractValidator<AddEditVendorCommand>
    {
        public AddEditVendorCommandValidator(IStringLocalizer<AddEditVendorCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Address)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Address is required!"]);
            RuleFor(request => request.PhoneNumber)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["PhoneNumber is required!"]);
            RuleFor(request => request.TaxNumber)
                .GreaterThan(0).WithMessage(x => localizer["TaxNumber must be greater than 0"]);
        }
    }
}