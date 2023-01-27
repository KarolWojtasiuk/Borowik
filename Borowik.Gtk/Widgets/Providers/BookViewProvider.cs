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

    public BookViewProvider(ICommander commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
    }

    public BookView CreateFor(Book book)
    {
        return new BookView(book, _commander);
    }
}