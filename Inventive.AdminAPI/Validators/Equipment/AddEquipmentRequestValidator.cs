using FluentValidation;
using Inventive.AdminAPI.Models.V1.Equipment;

namespace Inventive.AdminAPI.Validators.Equipment;

internal abstract class AddEquipmentRequestValidator : AbstractValidator<AddEquipmentRequestModel>
{
    protected AddEquipmentRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Length)
            .NotNull()
            .GreaterThan(0)
            .PrecisionScale(10, 2, true)
            .WithMessage("Length must be greater than 0 and have at most 2 decimal places");

        RuleFor(x => x.Width)
            .NotNull()
            .GreaterThan(0)
            .PrecisionScale(10, 2, true)
            .WithMessage("Width must be greater than 0 and have at most 2 decimal places");

        RuleFor(x => x.Height)
            .GreaterThan(0)
            .PrecisionScale(10, 3, true)
            .WithMessage("Height must be greater than 0 and have at most 2 decimal places");

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .PrecisionScale(10, 3, true)
            .WithMessage("Weight must be greater than 0 and have at most 3 decimal places");
    }
}
