using Borowik.Books.Entities;

namespace Borowik.Database.Sqlite;

internal class SqliteBookshelfRepository : IBookshelfRepository
{
    private readonly ISqliteConnectionProvider _connectionProvider;

    public SqliteBookshelfRepository(ISqliteConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
    }

    public async Task<Bookshelf?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync(cancellationToken);

        throw new NotImplementedException();
    }

    public async Task CreateAsync(Bookshelf entity, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        throw new NotImplementedException();

        await transaction.CommitAsync(cancellationToken);
    }
}