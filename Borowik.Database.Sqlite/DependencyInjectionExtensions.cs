using Borowik.Books.Entities;
using Borowik.Database.Sqlite.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Database.Sqlite;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikSqlite(this IServiceCollection services)
    {
        return services
            .AddSingleton<ISqliteConnectionProvider, SqliteConnectionProvider>()
            .AddSingleton<IDatabaseMigrator, DatabaseMigrator>()
            .AddTransient<IBookshelfRepository, SqliteBookshelfRepository>()

            .Scan(s => s.FromAssemblyOf<IMigration>()
                .AddClasses(c => c.AssignableTo<IMigration>())
                .AsImplementedInterfaces(i => i == typeof(IMigration))
                .WithTransientLifetime());
    }
}