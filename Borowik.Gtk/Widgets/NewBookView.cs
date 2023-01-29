using System.Text;
using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class NewBookView : Box
{
    public event EventHandler<Book>? Created;

    private readonly Bookshelf _bookshelf;
    private readonly ICommander _commander;
    private readonly Entry _entry;

    public NewBookView(Bookshelf bookshelf, ICommander commander)
    {
        _bookshelf = bookshelf ?? throw new ArgumentNullException(nameof(bookshelf));
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));

        Orientation = Orientation.Vertical;
        Spacing = 5;

        _entry = Entry.New();
        var button = Button.NewWithLabel("Create");
        button.OnClicked += Test;

        Append(_entry);
        Append(button);
    }

    private async void Test(Button sender, EventArgs args)
    {
        var text = _entry.GetText();
        var bytes = Encoding.UTF8.GetBytes(text);
        var book = await _commander.SendCommandAsync(new ImportBookCommand(_bookshelf.Id, RawBookType.PlainText, bytes));

        Created?.Invoke(this, book);
    }
}