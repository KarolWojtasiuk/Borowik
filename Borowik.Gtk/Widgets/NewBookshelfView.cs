using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class NewBookshelfView : Box
{
    public event EventHandler<Bookshelf>? Created;

    private readonly ICommander _commander;
    private readonly Entry _entry;

    public NewBookshelfView(ICommander commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));

        Orientation = Orientation.Vertical;
        Spacing = 5;
        Halign = Align.Center;
        Valign = Align.Center;
        Hexpand = true;

        _entry = Entry.New();
        var button = Button.NewWithLabel("Create");
        button.OnClicked += Test;

        Append(_entry);
        Append(button);
    }

    private async void Test(Button sender, EventArgs args)
    {
        var name = _entry.GetText();
        var bookshelf = await _commander.SendCommandAsync(new CreateBookshelfCommand(name));
        Created?.Invoke(this, bookshelf);
    }
}