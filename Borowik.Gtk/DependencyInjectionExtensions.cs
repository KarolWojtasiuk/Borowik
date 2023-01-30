using Gtk;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Gtk;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikGtk(this IServiceCollection services)
    {
        return services
            .AddSingleton<MainWindow>()

            .Scan(s =>
                s.FromAssemblies(typeof(DependencyInjectionExtensions).Assembly)
                    .AddClasses(c =>
                        c.InNamespaces("Borowik.Gtk.Widgets.Providers"))
                    .AsImplementedInterfaces(i => i.Namespace == "Borowik.Gtk.Widgets.Providers")
                    .WithTransientLifetime());
    }
}