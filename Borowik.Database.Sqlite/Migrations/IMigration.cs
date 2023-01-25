using Microsoft.Data.Sqlite;

namespace Borowik.Database.Sqlite.Migrations;

internal interface IMigration
{
    public int Version { get; }
    public Task MigrateAsync(SqliteConnection connection, CancellationToken cancellationToken);
}