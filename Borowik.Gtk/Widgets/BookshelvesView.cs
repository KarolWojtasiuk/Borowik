using Borowik.Books.Queries;
using Borowik.Queries;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BookshelvesView : Stack
{
    private readonly IQuerier _querier;
    private readonly Box _contentBox;

    public BookshelvesView(IQuerier querier)
    {
        _querier = querier ?? throw new ArgumentNullException(nameof(querier));

        Halign = Align.Center;
        Vexpand = true;

        var spinner = Spinner.New();
        spinner.SetSizeRequest(100, 100);
        spinner.Spinning = true;

        AddNamed(spinner, "loading");
        AddNamed(_contentBox = Box.New(Orientation.Horizontal, 0), "content");

        _ = LoadBookshelves();
    }

    private async Task LoadBookshelves()
    {
        var bookshelves = await _querier.SendQueryAsync(new GetAllBookshelvesQuery(), CancellationToken.None);

        var stack = Stack.New();
        foreach (var bookshelf in bookshelves)
            stack.AddTitled(
                Label.New($"ID: {bookshelf.Id}, Name: {bookshelf.Name}"),
                bookshelf.Id.ToString(),
                bookshelf.Name);

        var stackSidebar = StackSidebar.New();
        stackSidebar.Stack = stack;

        _contentBox.Append(stackSidebar);
        _contentBox.Append(Separator.New(Orientation.Horizontal));
        _contentBox.Append(stack);

        SetVisibleChildName("content");
    }
}