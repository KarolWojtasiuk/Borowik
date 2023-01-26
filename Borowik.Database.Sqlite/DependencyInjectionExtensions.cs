using Borowik.Books.Entities;
using Borowik.Database.Sqlite.Migrations;
using Borowik.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Database.Sqlite;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikSqlite(this IServiceCollection services)
    {
        return services
            .AddSingleton<IEntityRepository<Bookshelf, Guid>, SqliteBookshelfRepository>()
            .AddSingleton<ISqliteConnectionProvider, SqliteConnectionProvider>()
            .AddSingleton<IDatabaseMigrator, DatabaseMigrator>()
            .AddMigrations();
    }

    private static IServiceCollection AddMigrations(this IServiceCollection services)
    {
        var migrationTypes = typeof(IMigration).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IMigration)))
            .Where(t => !t.IsAbstract);

        foreach (var migrationType in migrationTypes)
            services.AddTransient(typeof(IMigration), migrationType);

        return services;
    }
}