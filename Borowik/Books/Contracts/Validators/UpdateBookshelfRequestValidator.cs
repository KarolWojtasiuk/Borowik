using FluentValidation;

namespace Borowik.Books.Contracts.Validators;

public class UpdateBookshelfRequestValidator : AbstractValidator<UpdateBookshelfRequest>
{
    public UpdateBookshelfRequestValidator()
    {
        When(e => e.Name is not null, () =>
        {
            RuleFor(e => e.Name)
                .MinimumLength(3)
                .MaximumLength(20);
        });

        When(e => e.Description is not null, () =>
        {
            RuleFor(e => e.Description)
                .MaximumLength(255);
        });

        When(e => e.Color.HasValue, () =>
        {
            RuleFor(e => e.Color!.Value.A)
                .Equal(byte.MaxValue).WithMessage("Color cannot be transparent");
        });

        When(e => e.SortMode is not null, () =>
        {
            RuleFor(e => e.SortMode)
                .IsInEnum();
        });
    }
}