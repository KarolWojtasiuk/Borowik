using Borowik.Books.Entities;
using Borowik.Gtk.Widgets.Providers;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BookshelfView : Box
{
    public event EventHandler? Updated;

    private readonly Bookshelf _bookshelf;
    private readonly IBooksViewProvider _booksViewProvider;

    public BookshelfView(
        Bookshelf bookshelf,
        IBooksViewProvider booksViewProvider)
    {
        _bookshelf = bookshelf ?? throw new ArgumentNullException(nameof(bookshelf));
        _booksViewProvider = booksViewProvider ?? throw new ArgumentNullException(nameof(booksViewProvider));

        BuildWidget();
    }

    private void BuildWidget()
    {
        Orientation = Orientation.Vertical;
        Spacing = 5;

        var title = Label.New(_bookshelf.Name);
        title.AddCssClass("title-1");
        Append(title);

        var description = Label.New(_bookshelf.Description);
        description.AddCssClass("title-3");
        Append(description);

        var booksView = _booksViewProvider.CreateFor(_bookshelf);
        booksView.Updated += (_, _) => Updated?.Invoke(this, EventArgs.Empty);

        var scrollBox = ScrolledWindow.New();
        scrollBox.Vexpand = true;
        scrollBox.Hexpand = true;
        scrollBox.SetChild(booksView);
        Append(scrollBox);
    }
}