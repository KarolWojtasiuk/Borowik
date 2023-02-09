using FluentValidation;

namespace Borowik.Books.Contracts.Validators;

public class SetBooksSortModeRequestValidator : AbstractValidator<SetBooksSortModeRequest>
{
    public SetBooksSortModeRequestValidator()
    {
        RuleFor(e => e.SortMode)
            .IsInEnum();
    }
}