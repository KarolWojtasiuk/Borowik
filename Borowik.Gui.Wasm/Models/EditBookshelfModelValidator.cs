using FluentValidation;

namespace Borowik.Gui.Wasm.Models;

public class EditBookshelfModelValidator : AbstractValidator<EditBookshelfModel>
{
    public EditBookshelfModelValidator()
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

        When(e => e.Color is not null, () =>
        {
            RuleFor(e => e.Color)
                .Matches("^#([A-Fa-f0-9]{3}|[A-Fa-f0-9]{6})$");
        });
    }
}