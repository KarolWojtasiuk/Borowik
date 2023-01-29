using Borowik.Books.Entities;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class ReadWindow : Adw.Window
{
    private readonly Book _book;
    private readonly BookContent _content;

    public ReadWindow(Application application, Book book, BookContent content)
    {
        Application = application ?? throw new ArgumentNullException(nameof(application));
        _book = book ?? throw new ArgumentNullException(nameof(book));
        _content = content ?? throw new ArgumentNullException(nameof(content));

        DefaultWidth = 800;
        DefaultHeight = 600;

        var box = Box.New(Orientation.Vertical, 0);
        box.Append(Adw.HeaderBar.New());
        box.Append(Label.New(nameof(ReadWindow)));
        box.Append(Label.New(_book.Name));
        SetContent(box);
    }
}