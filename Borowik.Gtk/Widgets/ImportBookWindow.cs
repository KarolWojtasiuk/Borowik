using System.Text;
using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class ImportBookWindow : Adw.Window
{
    public event EventHandler<Book>? BookImported;

    private readonly Bookshelf _bookshelf;
    private readonly ICommander _commander;
    private readonly Entry _nameEntry = Entry.New();

    public ImportBookWindow(
        Bookshelf bookshelf,
        Application application,
        ICommander commander)
    {
        _bookshelf = bookshelf ?? throw new ArgumentNullException(nameof(bookshelf));
        Application = application ?? throw new ArgumentNullException(nameof(application));
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));

        BuildWidget();
    }

    private void BuildWidget()
    {
        DefaultWidth = 800;
        DefaultHeight = 600;

        var button = Button.NewWithLabel("Import");
        button.OnClicked += ImportBook;

        var box = Box.New(Orientation.Vertical, 0);
        box.Append(Adw.HeaderBar.New());
        box.Append(button);
        SetContent(box);
    }

    private async void ImportBook(Button sender, EventArgs args)
    {
        var content = _nameEntry.GetText();
        var bytes = Encoding.UTF8.GetBytes(content);
        var book = await _commander.SendCommandAsync(new ImportBookCommand(_bookshelf.Id, RawBookType.PlainText, bytes));

        BookImported?.Invoke(this, book);
        Close();
    }
}