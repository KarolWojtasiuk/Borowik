using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Scrutor;

[assembly: InternalsVisibleTo("Borowik.Database.Sqlite")]

namespace Borowik;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowik(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(DependencyInjectionExtensions).Assembly)
            .Scan(s =>
                s.FromAssemblies(typeof(DependencyInjectionExtensions).Assembly)
                    .AddClasses(c => c.WithAttribute<ServiceDescriptorAttribute>())
                    .UsingAttributes());
    }
}