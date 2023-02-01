using Borowik.Books.Commands;
using Borowik.Books.Entities;
using Borowik.Commands;
using Borowik.Gtk.Widgets.Providers;
using GdkPixbuf;
using Gtk;
using GtkLabel = Gtk.Label;

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
        Halign = Align.Center;
        Valign = Align.Start;
        AddCssClass("card");

        var box = Box.New(Orientation.Vertical, 5);
        box.MarginTop = 10;
        box.MarginBottom = 5;
        box.MarginStart = 10;
        box.MarginEnd = 10;
        Append(box);

        var name = GtkLabel.New(_book.Metadata.Name);
        name.AddCssClass("title-2");
        box.Append(name);

        var author = GtkLabel.New(_book.Metadata.Author ?? "Unknown Author");
        box.Append(author);

        SetTooltipMarkup($"""
            <b>Last opened at</b>: {_book.LastOpenedAt?.ToString("D") ?? "Never"}
            <b>Created at</b>: {_book.CreatedAt:D}
        """);
        HasTooltip = true;

        var button = Button.NewWithLabel("Open");
        button.MarginTop = 5;
        button.MarginBottom = 5;
        button.MarginStart = 5;
        button.MarginEnd = 5;
        button.OnClicked += (_, _) => _ = OpenBookAsync();
        box.Append(button);
    }

    private async Task OpenBookAsync()
    {
        var (content, lastOpenedAt) = await _commander.SendCommandAsync(new OpenBookCommand(_book.Id));
        _book = _book with { LastOpenedAt = lastOpenedAt };
        var window = _readBookWindowProvider.CreateFor(_book, content);
        window.Show();
    }
}