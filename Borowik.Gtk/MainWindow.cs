using Borowik.Gtk.Widgets.Providers;
using Gtk;

namespace Borowik.Gtk;

internal class MainWindow : Adw.ApplicationWindow
{
    
    public MainWindow(
        Application application,
        IBookshelvesViewProvider bookshelvesViewProvider)
    {
        Application = application ?? throw new ArgumentNullException(nameof(application));

        BuildWidget(bookshelvesViewProvider);
    }

    private void BuildWidget(IBookshelvesViewProvider bookshelvesViewProvider)
    {
        DefaultWidth = 800;
        DefaultHeight = 600;

        var box = Box.New(Orientation.Vertical, 5);
        box.Append(Adw.HeaderBar.New());
        box.Append(bookshelvesViewProvider.Create());
        SetContent(box);
    }
}