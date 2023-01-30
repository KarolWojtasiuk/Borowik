using Borowik.Books.Entities;
using Borowik.Gtk.Widgets.Providers;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BooksView : FlowBox
{
    public event EventHandler? Updated;

    private readonly Bookshelf _bookshelf;
    private readonly IBookViewProvider _bookViewProvider;
    private readonly IImportBookViewProvider _importBookViewProvider;

    public BooksView(
        Bookshelf bookshelf,
        IBookViewProvider bookViewProvider,
        IImportBookViewProvider importBookViewProvider)
    {
        _bookshelf = bookshelf ?? throw new ArgumentNullException(nameof(bookshelf));
        _bookViewProvider = bookViewProvider ?? throw new ArgumentNullException(nameof(bookViewProvider));
        _importBookViewProvider = importBookViewProvider ?? throw new ArgumentNullException(nameof(importBookViewProvider));

        BuildWidget();
    }

    private void BuildWidget()
    {
        Orientation = Orientation.Horizontal;
        SelectionMode = SelectionMode.None;

        foreach (var book in _bookshelf.Books)
            Append(_bookViewProvider.CreateFor(book));

        var newBookView = _importBookViewProvider.CreateFor(_bookshelf);
        newBookView.Created += (_, _) => Updated?.Invoke(this, EventArgs.Empty);
        Append(newBookView);
    }
}