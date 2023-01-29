using Borowik;
using Borowik.Database.Sqlite;
using Borowik.Gtk;
using Gtk;
using Microsoft.Extensions.DependencyInjection;

using var application = Adw.Application.New("karolwojtasiuk.borowik.gtk", Gio.ApplicationFlags.DefaultFlags);

application.OnActivate += (app, _) =>
{
    var serviceProvider = new ServiceCollection()
        .AddBorowik()
        .AddBorowikSqlite()
        .AddBorowikGtk()
        .AddSingleton((Application)app)
        .BuildServiceProvider();

    var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
    mainWindow.Show();
};

return application.Run();