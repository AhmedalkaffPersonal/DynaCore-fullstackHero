using DynaCore.Application.Features.Products.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DynaCore.Application.Validators.Features.Products.Commands.AddEdit
{
    public class AddEditProductCommandValidator : AbstractValidator<AddEditProductCommand>
    {
        public AddEditProductCommandValidator(IStringLocalizer<AddEditProductCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.SKU)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["SKU is required!"]);
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Address is required!"]);
            RuleFor(request => request.VendorId)
                .GreaterThan(0).WithMessage(x => localizer["Vendor is required!"]);
            RuleFor(request => request.Quantity)
                .GreaterThan(0).WithMessage(x => localizer["Quantity must be greater than 0"]);
        }
    }
}