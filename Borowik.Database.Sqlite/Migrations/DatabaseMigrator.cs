using Borowik.Services;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Borowik.Database.Sqlite.Migrations;

internal class DatabaseMigrator : IDatabaseMigrator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMigration[] _migrations;
    private readonly int _latestVersion;
    private readonly SemaphoreSlim _semaphore = new (1);

    private int? _currentVersion;

    public DatabaseMigrator(
        IEnumerable<IMigration> migrations,
        IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _migrations = migrations.OrderBy(m => m.Version).ToArray();
        _latestVersion = _migrations.Last().Version;
    }

    public async Task EnsureMigratedAsync(SqliteConnection connection, CancellationToken cancellationToken)
    {
        _currentVersion ??= await GetCurrentVersionOrInitializeAsync(connection, cancellationToken);

        if (_currentVersion > _latestVersion)
            throw new InvalidOperationException("Current version is newer than latest supported version");

        while (_currentVersion < _latestVersion)
        {
            var nextMigration = _migrations.First(m => m.Version > _currentVersion);
            await MigrateAsync(connection, nextMigration, cancellationToken);
            _currentVersion = nextMigration.Version;
        }
    }

    private async Task<int?> GetCurrentVersionOrInitializeAsync(SqliteConnection connection, CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            // _currentVersion may be assigned after entering semaphore
            if (_currentVersion is not null)
                return _currentVersion;

            return await connection.ExecuteScalarAsync<int>("""
                CREATE TABLE IF NOT EXISTS MIGRATIONS(
                    VERSION INT PRIMARY KEY,
                    MIGRATED_AT INT NOT NULL
                );

                SELECT IFNULL(MAX(VERSION), 0) FROM MIGRATIONS;
            """);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task MigrateAsync(SqliteConnection connection, IMigration migration,
        CancellationToken cancellationToken)
    {
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await migration.MigrateAsync(connection, cancellationToken);
        await connection.ExecuteAsync("""
            INSERT INTO MIGRATIONS(VERSION, MIGRATED_AT)
            VALUES (@Version, @MigratedAt);
         """, new
        {
            Version = migration.Version,
            MigratedAt = _dateTimeProvider.GetUtcNew().Ticks
        });

        await transaction.CommitAsync(cancellationToken);
    }
}