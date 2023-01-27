using Borowik.Books.Entities;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IBooksViewProvider
{
    public BooksView CreateFor(Bookshelf bookshelf);
}

internal class BooksViewProvider : IBooksViewProvider
{
    private readonly IBookViewProvider _bookViewProvider;
    private readonly INewBookViewProvider _newBookViewProvider;

    public BooksViewProvider(IBookViewProvider bookViewProvider, INewBookViewProvider newBookViewProvider)
    {
        _bookViewProvider = bookViewProvider ?? throw new ArgumentNullException(nameof(bookViewProvider));
        _newBookViewProvider = newBookViewProvider ?? throw new ArgumentNullException(nameof(newBookViewProvider));
    }
    
    public BooksView CreateFor(Bookshelf bookshelf)
    {
        return new BooksView(bookshelf, _bookViewProvider, _newBookViewProvider);
    }
}