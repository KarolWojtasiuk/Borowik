using Microsoft.Data.Sqlite;

namespace Borowik.Database.Sqlite.Migrations;

internal interface IDatabaseMigrator
{
    public Task EnsureMigratedAsync(SqliteConnection connection, CancellationToken cancellationToken);
}