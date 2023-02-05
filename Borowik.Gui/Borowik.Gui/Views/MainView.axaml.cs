using Avalonia;
using Avalonia.ReactiveUI;
using Borowik.Gui.Services;
using Borowik.Gui.ViewModels;

namespace Borowik.Gui.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        StorageProviderLocator.SetStorageProvider(this);
    }
}