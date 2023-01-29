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

    private readonly Spinner _spinner;
    private Box? _contentBox;

    public BookshelvesView(
        IQuerier querier,
        IBookshelfViewProvider bookshelfViewProvider,
        INewBookshelfViewProvider newBookshelfViewProvider)
    {
        _querier = querier ?? throw new ArgumentNullException(nameof(querier));
        _bookshelfViewProvider = bookshelfViewProvider ?? throw new ArgumentNullException(nameof(bookshelfViewProvider));
        _newBookshelfViewProvider =
            newBookshelfViewProvider ?? throw new ArgumentNullException(nameof(newBookshelfViewProvider));

        Vexpand = true;

        _spinner = Spinner.New();
        _spinner.Halign = Align.Center;
        _spinner.SetSizeRequest(100, 100);

        AddNamed(_spinner, "loading");

        _ = LoadBookshelvesAsync();
    }

    private async Task LoadBookshelvesAsync()
    {
        _spinner.Spinning = true;
        SetVisibleChildName("loading");

        await Task.Delay(TimeSpan.FromSeconds(1)); //TODO
        var bookshelves = await _querier.SendQueryAsync(new GetAllBookshelvesQuery());

        var stack = Stack.New();
        foreach (var bookshelf in bookshelves)
            stack.AddTitled(
                _bookshelfViewProvider.CreateFor(bookshelf),
                bookshelf.Id.ToString(),
                bookshelf.Name);

        var newBookshelfView = _newBookshelfViewProvider.Create();
        stack.AddTitled(newBookshelfView, "new", "<New Bookshelf>");
        newBookshelfView.Created += (_, _) => LoadBookshelvesAsync();

        var stackSidebar = StackSidebar.New();
        stackSidebar.Stack = stack;

        if (_contentBox is not null)
            Remove(_contentBox);

        AddNamed(_contentBox = Box.New(Orientation.Horizontal, 5), "content");
        _contentBox.Append(stackSidebar);
        _contentBox.Append(Separator.New(Orientation.Horizontal));
        _contentBox.Append(stack);

        SetVisibleChildName("content");
        _spinner.Spinning = false;
    }
}