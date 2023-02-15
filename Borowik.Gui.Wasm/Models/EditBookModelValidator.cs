using FluentValidation;

namespace Borowik.Gui.Wasm.Models;

public class EditBookModelValidator : AbstractValidator<EditBookModel>
{
    public EditBookModelValidator()
    {
        When(e => e.Title is not null, () =>
        {
            RuleFor(e => e.Title)
                .MaximumLength(50);
        });

        When(e => e.Author is not null, () =>
        {
            RuleFor(e => e.Author)
                .MaximumLength(50);
        });

        RuleFor(e => e.Cover)
            .Null(); //TODO: at least for now
    }
}