using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Gtk.Widgets.Providers;

internal interface INewBookViewProvider
{
    public NewBookView CreateFor(Bookshelf bookshelf);
}

internal class NewBookViewProvider : INewBookViewProvider
{
    private readonly ICommander _commander;

    public NewBookViewProvider(ICommander commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
    }

    public NewBookView CreateFor(Bookshelf bookshelf)
    {
        return new NewBookView(bookshelf, _commander);
    }
}