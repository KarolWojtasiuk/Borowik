using Borowik.Database.Sqlite.Migrations;
using Microsoft.Data.Sqlite;

namespace Borowik.Database.Sqlite;

internal class SqliteConnectionProvider : ISqliteConnectionProvider
{
    private readonly IDatabaseMigrator _databaseMigrator;

    public SqliteConnectionProvider(IDatabaseMigrator databaseMigrator)
    {
        _databaseMigrator = databaseMigrator ?? throw new ArgumentNullException(nameof(databaseMigrator));
    }

    public async Task<SqliteConnection> CreateConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = new SqliteConnection("Data Source=database.sqlite");

        await connection.OpenAsync(cancellationToken);
        await _databaseMigrator.EnsureMigratedAsync(connection, cancellationToken);

        return connection;
    }
}