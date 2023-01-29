using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IReadWindowProvider
{
    public ReadWindow CreateFor(Book book, BookContent content);
}

internal class ReadWindowProvider : IReadWindowProvider
{
    private readonly Application _application;

    public ReadWindowProvider(Application application)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
    }

    public ReadWindow CreateFor(Book book, BookContent content)
    {
        return new ReadWindow(_application, book, content);
    }
}