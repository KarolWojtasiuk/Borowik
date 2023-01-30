using Borowik.Books.Entities;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IBooksViewProvider
{
    public BooksView CreateFor(Bookshelf bookshelf);
}

internal class BooksViewProvider : IBooksViewProvider
{
    private readonly IBookViewProvider _bookViewProvider;
    private readonly IImportBookViewProvider _importBookViewProvider;

    public BooksViewProvider(IBookViewProvider bookViewProvider, IImportBookViewProvider importBookViewProvider)
    {
        _bookViewProvider = bookViewProvider ?? throw new ArgumentNullException(nameof(bookViewProvider));
        _importBookViewProvider = importBookViewProvider ?? throw new ArgumentNullException(nameof(importBookViewProvider));
    }
    
    public BooksView CreateFor(Bookshelf bookshelf)
    {
        return new BooksView(bookshelf, _bookViewProvider, _importBookViewProvider);
    }
}