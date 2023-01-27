using Borowik.Books.Entities;
using Borowik.Gtk.Widgets.Providers;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BookshelfView : Box
{
    private readonly Bookshelf _bookshelf;
    private readonly IBooksViewProvider _booksViewProvider;

    public BookshelfView(
        Bookshelf bookshelf,
        IBooksViewProvider booksViewProvider)
    {
        _bookshelf = bookshelf ?? throw new ArgumentNullException(nameof(bookshelf));
        _booksViewProvider = booksViewProvider ?? throw new ArgumentNullException(nameof(booksViewProvider));

        Orientation = Orientation.Vertical;
        Spacing = 5;

        Append(Label.New($"ID: {_bookshelf.Id}"));
        Append(Label.New($"NAME: {_bookshelf.Name}"));
        Append(Label.New($"COLOR: {_bookshelf.Color}"));
        Append(Label.New($"DESCRIPTION: {_bookshelf.Description}"));
        Append(Label.New($"CREATED_AT: {_bookshelf.CreatedAt}"));
        Append(Separator.New(Orientation.Horizontal));
        Append(Label.New($"BOOKS: {_bookshelf.Books.Length}"));
        Append(_booksViewProvider.CreateFor(_bookshelf));
    }
}