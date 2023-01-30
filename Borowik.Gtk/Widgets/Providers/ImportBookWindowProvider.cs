using Borowik.Books.Entities;
using Borowik.Commands;
using Gtk;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IImportBookWindowProvider
{
    public ImportBookWindow CreateFor(Bookshelf bookshelf);
}

internal class ImportBookWindowProvider : IImportBookWindowProvider
{
    private readonly Application _application;
    private readonly ICommander _commander;

    public ImportBookWindowProvider(Application application, ICommander commander)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
    }

    public ImportBookWindow CreateFor(Bookshelf bookshelf)
    {
        return new ImportBookWindow(bookshelf, _application, _commander);
    }
}