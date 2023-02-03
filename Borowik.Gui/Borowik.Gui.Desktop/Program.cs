using System;
using Avalonia;
using Avalonia.ReactiveUI;

namespace Borowik.Gui.Desktop;

public static class Program
{
    [STAThread]
    public static void Main(string[] args) =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI()
            .StartWithClassicDesktopLifetime(args);
}