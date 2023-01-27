using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class NewBookView : Box
{
    public event EventHandler<Book>? Created;

    private readonly ICommander _commander;
    private readonly Entry _entry;

    public NewBookView(Bookshelf bookshelf, ICommander commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));

        _entry = Entry.New();
        var button = Button.NewWithLabel("Create");
        button.OnClicked += Test;

        Append(_entry);
        Append(button);
    }

    private async void Test(Button sender, EventArgs args)
    {
        var name = _entry.GetText();

        throw new NotImplementedException();
    }
}