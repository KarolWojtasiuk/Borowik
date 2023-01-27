using Borowik.Books.Entities;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IBookViewProvider
{
    public BookView CreateFor(Book book);
}

internal class BookViewProvider : IBookViewProvider
{
    public BookView CreateFor(Book book)
    {
        return new BookView(book);
    }
}