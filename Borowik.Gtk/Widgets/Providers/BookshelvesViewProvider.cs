using Borowik.Queries;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IBookshelvesViewProvider
{
    public BookshelvesView Create();
}

internal class BookshelvesViewProvider : IBookshelvesViewProvider
{
    private readonly IQuerier _querier;
    private readonly IBookshelfViewProvider _bookshelfViewProvider;
    private readonly INewBookshelfViewProvider _newBookshelfViewProvider;

    public BookshelvesViewProvider(
        IQuerier querier,
        IBookshelfViewProvider bookshelfViewProvider,
        INewBookshelfViewProvider newBookshelfViewProvider)
    {
        _querier = querier ?? throw new ArgumentNullException(nameof(querier));
        _bookshelfViewProvider = bookshelfViewProvider ?? throw new ArgumentNullException(nameof(bookshelfViewProvider));
        _newBookshelfViewProvider = newBookshelfViewProvider ?? throw new ArgumentNullException(nameof(newBookshelfViewProvider));
    }

    public BookshelvesView Create()
    {
        return new BookshelvesView(_querier, _bookshelfViewProvider, _newBookshelfViewProvider);
    }
}