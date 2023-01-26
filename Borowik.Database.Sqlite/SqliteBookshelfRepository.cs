using System.Data;
using System.Data.Common;
using System.Drawing;
using Borowik.Books.Entities;
using Dapper;

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

        await using var bookshelfReader = await connection.ExecuteReaderAsync("""
            SELECT ID, NAME, DESCRIPTION, COLOR, CREATED_AT FROM BOOKSHELVES
            WHERE ID = @Id;
        """, new { Id = id.ToString() });

        if (!await bookshelfReader.ReadAsync(cancellationToken))
            return null;

        await using var bookReader = await connection.ExecuteReaderAsync("""
            SELECT NAME, AUTHOR, COVER, CONTENT, CREATED_AT, LAST_OPENED_AT FROM BOOKS
            WHERE BOOKSHELF_ID = @Id;
        """, new { Id = id.ToString() });

        var books = new List<Book>();

        while (await bookReader.ReadAsync(cancellationToken))
            books.Add(ReadBook(bookReader));

        return ReadBookshelf(bookshelfReader, books.ToArray());
    }

    public async Task CreateAsync(Bookshelf bookshelf, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await connection.ExecuteAsync("""
            INSERT INTO BOOKSHELVES (ID, NAME, DESCRIPTION, COLOR, CREATED_AT)
                VALUES (@Id, @Name, @Description, @Color, @CreatedAt);
        """, new
        {
            Id = bookshelf.Id.ToString(),
            Name = bookshelf.Name,
            Description = bookshelf.Description,
            Color = bookshelf.Color.ToArgb(),
            CreatedAt = bookshelf.CreatedAt.Ticks
        });

        await transaction.CommitAsync(cancellationToken);
    }

    private static Book ReadBook(DbDataReader bookReader)
    {
        var name = (string?)bookReader.GetNullableValue("NAME");
        var author = (string?)bookReader.GetNullableValue("AUTHOR");
        var cover = (string?)bookReader.GetNullableValue("COVER");
        var createdAt = (long)bookReader.GetValue("CREATED_AT");
        var lastOpenedAt = (long?)bookReader.GetNullableValue("LAST_OPENED_AT");

        return new Book(
            name,
            author,
            cover is null ? null : Convert.FromBase64String(cover),
            DateTime.SpecifyKind(new DateTime(createdAt), DateTimeKind.Utc),
            lastOpenedAt is null ? null : DateTime.SpecifyKind(new DateTime(lastOpenedAt.Value), DateTimeKind.Utc)
        );
    }

    private static Bookshelf ReadBookshelf(DbDataReader bookReader, Book[] books)
    {
        var id = Guid.Parse((string)bookReader.GetValue("ID"));
        var name = (string)bookReader.GetValue("NAME");
        var description = (string?)bookReader.GetNullableValue("DESCRIPTION");
        var color = (int)(long)bookReader.GetValue("COLOR");
        var createdAt = (long)bookReader.GetValue("CREATED_AT");

        return new Bookshelf(
            id,
            name,
            description,
            books,
            Color.FromArgb(color),
            DateTime.SpecifyKind(new DateTime(createdAt), DateTimeKind.Utc)
        );
    }
}