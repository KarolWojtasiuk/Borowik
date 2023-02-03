using Avalonia.ReactiveUI;
using Borowik.Gui.ViewModels;

namespace Borowik.Gui.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }
}