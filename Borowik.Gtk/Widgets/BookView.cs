using Borowik.Books.Entities;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BookView : Box
{
    public BookView(Book book)
    {
        Append(Label.New(book.Name));
    }
}