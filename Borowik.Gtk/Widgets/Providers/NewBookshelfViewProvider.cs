using Borowik.Commands;

namespace Borowik.Gtk.Widgets.Providers;

internal interface INewBookshelfViewProvider
{
    public NewBookshelfView Create();
}

internal class NewBookshelfViewProvider : INewBookshelfViewProvider
{
    private readonly ICommander _commander;

    public NewBookshelfViewProvider(ICommander commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
    }

    public NewBookshelfView Create()
    {
        return new NewBookshelfView(_commander);
    }
}