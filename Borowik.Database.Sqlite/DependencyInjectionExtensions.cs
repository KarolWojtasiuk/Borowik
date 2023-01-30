using Borowik.Database.Sqlite.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Database.Sqlite;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikSqlite(this IServiceCollection services)
    {
        return services
            .Scan(s =>
                s.FromAssemblies(typeof(DependencyInjectionExtensions).Assembly)
                    .AddClasses(c => c.WithAttribute<ServiceDescriptorAttribute>())
                    .UsingAttributes())

            .Scan(s => s.FromAssemblyOf<IMigration>()
                .AddClasses(c => c.AssignableTo<IMigration>())
                .AsImplementedInterfaces(i => i == typeof(IMigration))
                .WithTransientLifetime());
    }
}