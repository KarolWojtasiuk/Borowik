using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Borowik.Database.LiteDb")]

namespace Borowik;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowik(this IServiceCollection services)
    {
        const string singletonSuffix = "Provider";
        var assembly = typeof(DependencyInjectionExtensions).Assembly;

        return services
            .Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.Where(t => t.Name.EndsWith(singletonSuffix)))
                .AsImplementedInterfaces(i => i.Assembly == assembly)
                .WithSingletonLifetime())

            .Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.Where(t => !t.Name.EndsWith(singletonSuffix)))
                .AsImplementedInterfaces(i => i.Assembly == assembly)
                .WithTransientLifetime());
    }
}
