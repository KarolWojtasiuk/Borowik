using System.Runtime.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Database.IndexedDb;

[SupportedOSPlatform("browser")]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikLiteDb(this IServiceCollection services)
    {
        const string singletonSuffix = "Provider";
        var assembly = typeof(DependencyInjectionExtensions).Assembly;
        var coreAssembly = typeof(Borowik.DependencyInjectionExtensions).Assembly;

        return services
            .Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.Where(t => t.Name.EndsWith(singletonSuffix)))
                .AsImplementedInterfaces(i => i.Assembly == assembly || i.Assembly == coreAssembly)
                .WithSingletonLifetime())

            .Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.Where(t => !t.Name.EndsWith(singletonSuffix)))
                .AsImplementedInterfaces(i => i.Assembly == assembly || i.Assembly == coreAssembly)
                .WithTransientLifetime());
    }
}