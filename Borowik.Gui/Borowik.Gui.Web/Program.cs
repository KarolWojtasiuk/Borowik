using System.Runtime.Versioning;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Web;
using Borowik.Gui;

[assembly: SupportedOSPlatform("browser")]

AppBuilder.Configure<App>()
    .UseReactiveUI()
    .SetupBrowserApp("out");
