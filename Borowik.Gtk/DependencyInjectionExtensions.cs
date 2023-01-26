using Gtk;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Gtk;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikGtk(this IServiceCollection services)
    {
        return services
            .AddWidgets();
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
}