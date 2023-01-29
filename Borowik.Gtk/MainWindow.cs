using Borowik.Gtk.Widgets;
using Gtk;

namespace Borowik.Gtk;

internal class MainWindow : Adw.ApplicationWindow
{
    
    public MainWindow(Application application, BookshelvesView bookshelvesView)
    {
        Application = application ?? throw new ArgumentNullException(nameof(application));
        DefaultWidth = 800;
        DefaultHeight = 600;

        var box = Box.New(Orientation.Vertical, 0);
        box.Append(Adw.HeaderBar.New());
        box.Append(bookshelvesView);
        SetContent(box);
    }
}