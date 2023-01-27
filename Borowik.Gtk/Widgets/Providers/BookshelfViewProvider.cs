using Borowik.Books.Entities;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IBookshelfViewProvider
{
    public BookshelfView CreateFor(Bookshelf bookshelf);
}

internal class BookshelfViewProvider : IBookshelfViewProvider
{
    private readonly IBooksViewProvider _booksViewProvider;

    public BookshelfViewProvider(IBooksViewProvider booksViewProvider)
    {
        _booksViewProvider = booksViewProvider ?? throw new ArgumentNullException(nameof(booksViewProvider));
    }

    public BookshelfView CreateFor(Bookshelf bookshelf)
    {
        return new BookshelfView(bookshelf, _booksViewProvider);
    }
}