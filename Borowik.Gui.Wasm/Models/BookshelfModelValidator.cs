using FluentValidation;

namespace Borowik.Gui.Wasm.Models;

public class BookshelfModelValidator : AbstractValidator<BookshelfModel>
{
    public BookshelfModelValidator()
    {
        RuleFor(e => e.Name)
            .MinimumLength(3)
            .MaximumLength(20);

        RuleFor(e => e.Description)
            .MaximumLength(255);

        RuleFor(e => e.Color)
            .Matches("^#([A-Fa-f0-9]{3}|[A-Fa-f0-9]{6})$");
    }
}