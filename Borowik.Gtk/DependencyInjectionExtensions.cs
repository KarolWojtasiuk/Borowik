using Borowik.Gtk.Widgets.Providers;
using Gtk;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Gtk;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikGtk(this IServiceCollection services)
    {
        return services
            .AddWidgets()
            .AddWidgetProviders();
    }

    private static IServiceCollection AddWidgets(this IServiceCollection services)
    {
        var widgetTypes = typeof(DependencyInjectionExtensions).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(Widget)))
            .Where(t => !t.IsAbstract);

        foreach (var widgetType in widgetTypes)
            services.AddTransient(widgetType);

        return services;
    }

    private static IServiceCollection AddWidgetProviders(this IServiceCollection services)
    {
        var providerTypes = typeof(DependencyInjectionExtensions).Assembly
            .GetTypes()
            .Where(t => t.Namespace == "Borowik.Gtk.Widgets.Providers")
            .Where(t => !t.IsAbstract)
            .Where(t => t.GetInterfaces().Length == 1);

        foreach (var providerType in providerTypes)
            services.AddTransient(providerType.GetInterfaces().First(), providerType);

        return services;
    }
}