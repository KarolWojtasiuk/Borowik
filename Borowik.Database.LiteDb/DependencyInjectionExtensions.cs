using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Database.LiteDb;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikLiteDb<TCustomLiteDbProvider>(this IServiceCollection services)
        where TCustomLiteDbProvider : class, ICustomLiteDbProvider
    {
        const string singletonSuffix = "Provider";
        var assembly = typeof(DependencyInjectionExtensions).Assembly;
        var coreAssembly = typeof(Borowik.DependencyInjectionExtensions).Assembly;

        return services
            .AddSingleton<ICustomLiteDbProvider, TCustomLiteDbProvider>()

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