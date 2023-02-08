using Borowik.Database.Dexie.Entities;
using DexieNET;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Database.Dexie;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikDexie(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjectionExtensions).Assembly;
        var coreAssembly = typeof(Borowik.DependencyInjectionExtensions).Assembly;

        return services
            .AddDexieNET<BorowikEntityStoreDB>()

            .Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.WithAttribute<ServiceDescriptorAttribute>())
                .AsImplementedInterfaces(i => i.Assembly == coreAssembly || i.Assembly == assembly)
                .UsingAttributes());
    }
}