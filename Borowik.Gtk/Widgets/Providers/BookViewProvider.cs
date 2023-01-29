using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IBookViewProvider
{
    public BookView CreateFor(Book book);
}

internal class BookViewProvider : IBookViewProvider
{
    private readonly ICommander _commander;
    private readonly IReadWindowProvider _readWindowProvider;

    public BookViewProvider(ICommander commander, IReadWindowProvider readWindowProvider)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
        _readWindowProvider = readWindowProvider ?? throw new ArgumentNullException(nameof(readWindowProvider));
    }

    public BookView CreateFor(Book book)
    {
        return new BookView(book, _commander, _readWindowProvider);
    }
}