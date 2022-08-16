using DynaCore.Application.Features.ContactPersons.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DynaCore.Application.Validators.Features.ContactPerson.Commands.AddEdit
{
    public class AddEditContactPersonCommandValidator : AbstractValidator<AddEditContactPersonCommand>
    {
        public AddEditContactPersonCommandValidator(IStringLocalizer<AddEditContactPersonCommandValidator> localizer)
        {
            RuleFor(request => request.FirstName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["First Name is required!"]);
            RuleFor(request => request.LastName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Last Name is required!"]);
            RuleFor(request => request.PhoneNumber)
               .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["PhoneNumber is required!"]);

        }
    }
}