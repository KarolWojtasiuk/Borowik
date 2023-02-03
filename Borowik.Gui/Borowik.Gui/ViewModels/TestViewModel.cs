using System.Windows.Input;
using Avalonia.Platform.Storage;
using ReactiveUI;

namespace Borowik.Gui.ViewModels;

public class TestViewModel : ViewModelBase
{
    public IStorageProvider? StorageProvider { get; set; }

    public ICommand TestCommand { get; }

    public TestViewModel()
    {
        TestCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (StorageProvider is null)
                return;

            await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions());
        });
    }
}