using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Gtk.Widgets.Providers;

internal interface IImportBookViewProvider
{
    public ImportBookView CreateFor(Bookshelf bookshelf);
}

internal class ImportBookViewProvider : IImportBookViewProvider
{
    private readonly ICommander _commander;
    private readonly IImportBookWindowProvider _importBookWindowProvider;

    public ImportBookViewProvider(ICommander commander, IImportBookWindowProvider importBookWindowProvider)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
        _importBookWindowProvider = importBookWindowProvider ?? throw new ArgumentNullException(nameof(importBookWindowProvider));
    }

    public ImportBookView CreateFor(Bookshelf bookshelf)
    {
        return new ImportBookView(bookshelf, _importBookWindowProvider);
    }
}