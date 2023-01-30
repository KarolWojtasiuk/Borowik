using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class NewBookshelfView : Box
{
    public event EventHandler<Bookshelf>? Created;

    private readonly ICommander _commander;
    private readonly Entry _nameEntry = Entry.New();

    public NewBookshelfView(ICommander commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));

        BuildWidget();
    }

    private void BuildWidget()
    {
        Orientation = Orientation.Vertical;
        Spacing = 5;

        var button = Button.NewWithLabel("Create");
        button.OnClicked += Test;

        Append(_nameEntry);
        Append(button);
    }

    private async void Test(Button sender, EventArgs args)
    {
        var name = _nameEntry.GetText();
        var bookshelf = await _commander.SendCommandAsync(new CreateBookshelfCommand(name));
        Created?.Invoke(this, bookshelf);
    }
}