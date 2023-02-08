using FluentValidation;

namespace Borowik.Books.Contracts.Validators;

public class CreateBookshelfRequestValidator : AbstractValidator<CreateBookshelfRequest>
{
    public CreateBookshelfRequestValidator()
    {
        RuleFor(e => e.Name)
            .MinimumLength(3)
            .MaximumLength(20);

        RuleFor(e => e.Description)
            .MaximumLength(255);

        RuleFor(e => e.Color.A)
            .Equal(byte.MaxValue).WithMessage("Color cannot be transparent");
    }
}