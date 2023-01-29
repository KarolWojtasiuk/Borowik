using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Borowik.Gtk.Widgets.Providers;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BookView : Box
{
    private readonly Book _book;
    private readonly ICommander _commander;
    private readonly IReadWindowProvider _readWindowProvider;

    public BookView(Book book, ICommander commander, IReadWindowProvider readWindowProvider)
    {
        _book = book ?? throw new ArgumentNullException(nameof(book));
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
        _readWindowProvider = readWindowProvider ?? throw new ArgumentNullException(nameof(readWindowProvider));

        Orientation = Orientation.Vertical;
        Spacing = 5;

        Append(Label.New($"NAME: {book.Metadata.Name}"));
        Append(Label.New($"AUTHOR: {book.Metadata.Author}"));
        Append(Label.New($"CREATED_AT: {book.CreatedAt}"));
        Append(Label.New($"LAST_OPENED_AT: {book.LastOpenedAt}"));

        var button = Button.NewWithLabel("Open");
        button.OnClicked += (_, _) => OpenBookAsync();
        Append(button);
    }

    private async Task OpenBookAsync()
    {
        var content = await _commander.SendCommandAsync(new OpenBookCommand(_book.Id));
        var window = _readWindowProvider.CreateFor(_book, content);
        window.Show();
    }
}