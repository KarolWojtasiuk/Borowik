namespace Borowik.Gui.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(TestViewModel testViewModel)
    {
        TestViewModel = testViewModel;
    }

    public string Greeting => "main";
    public object TestViewModel { get; }
}