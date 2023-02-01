using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class ImportBookView : Button
{
    public event EventHandler<Book>? Created;

    private readonly Bookshelf _bookshelf;
    private readonly ICommander _commander;

    public ImportBookView(Bookshelf bookshelf, ICommander commander)
    {
        _bookshelf = bookshelf ?? throw new ArgumentNullException(nameof(bookshelf));
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));

        BuildWidget();
    }

    private void BuildWidget()
    {
        Halign = Align.Fill;
        Valign = Align.Start;

        SetIconName("list-add-symbolic");
        OnClicked += OpenImportBookWindowAsync;
    }

    private async void OpenImportBookWindowAsync(Button sender, EventArgs args)
    {
        // file dialog
    }
}