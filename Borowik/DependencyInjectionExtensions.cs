using System.Runtime.CompilerServices;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

[assembly: InternalsVisibleTo("Borowik.Database.Dexie")]

namespace Borowik;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowik(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjectionExtensions).Assembly;

        return services
            .AddValidatorsFromAssembly(assembly, ServiceLifetime.Singleton)
            .AddMediatR(assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.WithAttribute<ServiceDescriptorAttribute>())
                .UsingAttributes());
    }
}
