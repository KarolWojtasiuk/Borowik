using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BookView : Box
{
    private readonly Book _book;
    private readonly ICommander _commander;

    public BookView(Book book, ICommander commander)
    {
        _book = book ?? throw new ArgumentNullException(nameof(book));
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));

        Orientation = Orientation.Vertical;
        Spacing = 5;

        Append(Label.New($"NAME: {book.Name}"));
        Append(Label.New($"AUTHOR: {book.Author}"));
        Append(Label.New($"CREATED_AT: {book.CreatedAt}"));
        Append(Label.New($"LAST_OPENED_AT: {book.LastOpenedAt}"));

        var button = Button.NewWithLabel("Open");
        button.OnClicked += (_, _) => OpenBookAsync();
    }

    private async Task OpenBookAsync()
    {
        var content = await _commander.SendCommandAsync(new OpenBookCommand(_book.Id), CancellationToken.None);
    }
}