using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Borowik.Gtk.Widgets.Providers;
using Gtk;

namespace Borowik.Gtk.Widgets;

internal class BookView : Box
{
    private readonly ICommander _commander;
    private readonly IReadBookWindowProvider _readBookWindowProvider;

    private Book _book;

    public BookView(Book book, ICommander commander, IReadBookWindowProvider readBookWindowProvider)
    {
        _book = book ?? throw new ArgumentNullException(nameof(book));
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
        _readBookWindowProvider = readBookWindowProvider ?? throw new ArgumentNullException(nameof(readBookWindowProvider));

        BuildWidget();
    }

    private void BuildWidget()
    {
        Orientation = Orientation.Vertical;
        Spacing = 5;

        Append(Label.New($"NAME: {_book.Metadata.Name}"));
        Append(Label.New($"AUTHOR: {_book.Metadata.Author}"));
        Append(Label.New($"CREATED_AT: {_book.CreatedAt}"));
        Append(Label.New($"LAST_OPENED_AT: {_book.LastOpenedAt}"));

        var button = Button.NewWithLabel("Open");
        button.OnClicked += (_, _) => _ = OpenBookAsync();
        Append(button);
    }

    private async Task OpenBookAsync()
    {
        var (content, lastOpenedAt) = await _commander.SendCommandAsync(new OpenBookCommand(_book.Id));
        _book = _book with { LastOpenedAt = lastOpenedAt };
        var window = _readBookWindowProvider.CreateFor(_book, content);
        window.Show();
    }
}