using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Borowik.Database.LiteDb;
using Borowik.Gui.Services;
using Borowik.Gui.ViewModels;
using Borowik.Gui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Gui;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceProvider = CreateServiceProvider();
        var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static ServiceProvider CreateServiceProvider()
    {
        return new ServiceCollection()
            .AddBorowik()
            .AddBorowikLiteDb<AvaloniaLiteDbProvider>()

            .Scan(s => s.FromAssemblyOf<ViewModelBase>()
                .AddClasses(c => c.Where(t => t.IsAssignableTo(typeof(ViewModelBase))))
                .AsSelf()
                .WithTransientLifetime())

            .BuildServiceProvider();
    }
}