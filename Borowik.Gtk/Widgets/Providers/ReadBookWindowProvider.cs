using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IReadBookWindowProvider
{
    public ReadBookWindow CreateFor(Book book, BookContent content);
}

internal class ReadBookWindowProvider : IReadBookWindowProvider
{
    private readonly Application _application;

    public ReadBookWindowProvider(Application application)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
    }

    public ReadBookWindow CreateFor(Book book, BookContent content)
    {
        return new ReadBookWindow(_application, book, content);
    }
}