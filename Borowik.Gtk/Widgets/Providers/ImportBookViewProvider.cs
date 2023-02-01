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

    public ImportBookViewProvider(ICommander commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
    }

    public ImportBookView CreateFor(Bookshelf bookshelf)
    {
        return new ImportBookView(bookshelf, _commander);
    }
}