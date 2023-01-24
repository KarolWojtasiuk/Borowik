using Microsoft.Data.Sqlite;

namespace Borowik.Database.Sqlite;

internal interface ISqliteConnectionProvider
{
    public Task<SqliteConnection> CreateConnectionAsync(CancellationToken cancellationToken);
}