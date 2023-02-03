using System.Diagnostics;
using Avalonia.ReactiveUI;
using Borowik.Gui.ViewModels;
using ReactiveUI;

namespace Borowik.Gui.Views;

public partial class TestView : ReactiveUserControl<TestViewModel>
{
    public TestView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            Debug.Assert(ViewModel is not null);
            ViewModel.StorageProvider = this.GetStorageProvider();
        });
    }
}