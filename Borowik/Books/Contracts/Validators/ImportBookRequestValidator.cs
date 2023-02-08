using FluentValidation;

namespace Borowik.Books.Contracts.Validators;

public class ImportBookRequestValidator : AbstractValidator<ImportBookRequest>
{
    public ImportBookRequestValidator()
    {
        RuleFor(e => e.Type)
            .IsInEnum();
    }
}