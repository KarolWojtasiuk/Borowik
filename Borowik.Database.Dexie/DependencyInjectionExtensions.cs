using System.Runtime.Versioning;
using Borowik.Database.Dexie.Entities;
using DexieNET;
using Microsoft.Extensions.DependencyInjection;

[assembly: SupportedOSPlatform("browser")]
namespace Borowik.Database.Dexie;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikDexie(this IServiceCollection services)
    {
        const string singletonSuffix = "Provider";
        var assembly = typeof(DependencyInjectionExtensions).Assembly;
        var coreAssembly = typeof(Borowik.DependencyInjectionExtensions).Assembly;

        return services
            .AddDexieNET<BorowikEntityStoreDB>()

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