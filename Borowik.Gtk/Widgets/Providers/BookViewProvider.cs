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
    private readonly IReadBookWindowProvider _readBookWindowProvider;

    public BookViewProvider(ICommander commander, IReadBookWindowProvider readBookWindowProvider)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
        _readBookWindowProvider = readBookWindowProvider ?? throw new ArgumentNullException(nameof(readBookWindowProvider));
    }

    public BookView CreateFor(Book book)
    {
        return new BookView(book, _commander, _readBookWindowProvider);
    }
}