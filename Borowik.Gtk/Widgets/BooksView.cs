using Borowik.Books.Entities;
using Borowik.Gtk.Widgets.Providers;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BooksView : Box
{
    private readonly IBookViewProvider _bookViewProvider;
    private readonly INewBookViewProvider _newBookViewProvider;

    public BooksView(
        Bookshelf bookshelf,
        IBookViewProvider bookViewProvider,
        INewBookViewProvider newBookViewProvider)
    {
        _bookViewProvider = bookViewProvider ?? throw new ArgumentNullException(nameof(bookViewProvider));
        _newBookViewProvider = newBookViewProvider ?? throw new ArgumentNullException(nameof(newBookViewProvider));

        Orientation = Orientation.Horizontal;
        Spacing = 5;

        foreach (var book in bookshelf.Books)
            Append(_bookViewProvider.CreateFor(book));

        Append(_newBookViewProvider.CreateFor(bookshelf));
    }
}