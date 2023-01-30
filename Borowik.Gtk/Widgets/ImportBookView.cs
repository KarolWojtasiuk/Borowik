using System.Text;
using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Borowik.Gtk.Widgets.Providers;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class ImportBookView : Box
{
    public event EventHandler<Book>? Created;

    private readonly Bookshelf _bookshelf;
    private readonly IImportBookWindowProvider _importBookWindowProvider;

    public ImportBookView(Bookshelf bookshelf, IImportBookWindowProvider importBookWindowProvider)
    {
        _bookshelf = bookshelf ?? throw new ArgumentNullException(nameof(bookshelf));
        _importBookWindowProvider = importBookWindowProvider ?? throw new ArgumentNullException(nameof(importBookWindowProvider));

        BuildWidget();
    }

    private void BuildWidget()
    {
        Orientation = Orientation.Vertical;
        Spacing = 5;

        var button = Button.NewWithLabel("Import");
        button.Vexpand = true;
        button.OnClicked += OpenImportBookWindow;
        Append(button);
    }

    private async void OpenImportBookWindow(Button sender, EventArgs args)
    {
        var window = _importBookWindowProvider.CreateFor(_bookshelf);

        var taskSource = new TaskCompletionSource<Book?>();
        window.OnCloseRequest += (_, _) => taskSource.TrySetResult(null);
        window.BookImported += (_, book) => taskSource.TrySetResult(book);
        window.Show();

        var book = await taskSource.Task;

        if (book is not null)
            Created?.Invoke(this, book);
    }
}