using Borowik.Books.Entities;
using Borowik.Books.Queries;
using Borowik.Gtk.Widgets.Providers;
using Borowik.Queries;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BookshelvesView : Stack
{
    private readonly IQuerier _querier;
    private readonly IBookshelfViewProvider _bookshelfViewProvider;
    private readonly INewBookshelfViewProvider _newBookshelfViewProvider;

    private readonly Spinner _spinner = Spinner.New();
    private Box? _contentBox;

    public BookshelvesView(
        IQuerier querier,
        IBookshelfViewProvider bookshelfViewProvider,
        INewBookshelfViewProvider newBookshelfViewProvider)
    {
        _querier = querier ?? throw new ArgumentNullException(nameof(querier));
        _bookshelfViewProvider = bookshelfViewProvider ?? throw new ArgumentNullException(nameof(bookshelfViewProvider));
        _newBookshelfViewProvider = newBookshelfViewProvider ?? throw new ArgumentNullException(nameof(newBookshelfViewProvider));

        BuildWidget();
    }

    private void BuildWidget()
    {
        Vexpand = true;
        MarginEnd = 5;

        _spinner.Halign = Align.Center;
        _spinner.SetSizeRequest(100, 100);

        AddNamed(_spinner, "loading");

        _ = LoadBookshelvesAsync();
    }

    private async Task LoadBookshelvesAsync()
    {
        _spinner.Spinning = true;
        SetVisibleChildName("loading");

        var bookshelves = await _querier.SendQueryAsync(new GetAllBookshelvesQuery());
        BuildContent(bookshelves);

        SetVisibleChildName("content");
        _spinner.Spinning = false;
    }

    private void BuildContent(Bookshelf[] bookshelves)
    {
        var stack = Stack.New();
        foreach (var bookshelf in bookshelves)
        {
            var bookshelfView = _bookshelfViewProvider.CreateFor(bookshelf);
            bookshelfView.Updated += (_, _) => _ = LoadBookshelvesAsync();
            stack.AddTitled(bookshelfView, bookshelf.Id.ToString(), bookshelf.Name);
        }

        var newBookshelfView = _newBookshelfViewProvider.Create();
        newBookshelfView.Created += (_, _) => _ = LoadBookshelvesAsync();

        var clamp = Adw.Clamp.New();
        clamp.MaximumSize = 300;
        clamp.SetChild(newBookshelfView);

        stack.AddTitled(clamp, "new", "<New Bookshelf>");

        var stackSidebar = StackSidebar.New();
        stackSidebar.Stack = stack;

        if (_contentBox is not null)
            Remove(_contentBox);

        AddNamed(_contentBox = Box.New(Orientation.Horizontal, 5), "content");
        _contentBox.Append(stackSidebar);
        _contentBox.Append(Separator.New(Orientation.Horizontal));
        _contentBox.Append(stack);
    }
}