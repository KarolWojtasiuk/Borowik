using System.Drawing;
using System.Windows.Input;
using Borowik.Books;
using Borowik.Database.LiteDb;
using ReactiveUI;

namespace Borowik.Gui.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IBookshelfManager _bookshelfManager;
    private readonly ICustomLiteDbProvider _customLiteDbProvider;
    public ICommand OpenDatabaseCommand { get; }

    public MainViewModel(IBookshelfManager bookshelfManager, ICustomLiteDbProvider customLiteDbProvider)
    {
        _bookshelfManager = bookshelfManager;
        _customLiteDbProvider = customLiteDbProvider ?? throw new ArgumentNullException(nameof(customLiteDbProvider));
        OpenDatabaseCommand = ReactiveCommand.CreateFromTask(OpenDatabaseAsync);
    }

    private async Task OpenDatabaseAsync(CancellationToken cancellationToken)
    {
        var bookshelf = await _bookshelfManager.CreateBookshelfAsync("name", "desc", Color.Brown, cancellationToken);
        var r = await _bookshelfManager.GetBookshelfAsync(bookshelf.Id, cancellationToken);

        var db = await _customLiteDbProvider.GetLiteDatabase(null!, cancellationToken);
        db.Dispose();

    }
}