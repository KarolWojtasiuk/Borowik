using System.Data;
using System.Data.Common;
using System.Text.Json;
using Borowik.Books.Entities;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Borowik.Database.Sqlite;

internal class SqliteBookshelfRepository : IBookshelfRepository
{
    private readonly ISqliteConnectionProvider _connectionProvider;

    public SqliteBookshelfRepository(ISqliteConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
    }

    public async Task<Bookshelf[]> GetAllBookshelvesAsync(CancellationToken cancellationToken)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync(cancellationToken);

        var bookshelves = new List<Bookshelf>();

        await using var bookshelfReader =
            await connection.ExecuteReaderAsync("SELECT ID, NAME, DESCRIPTION, CREATED_AT FROM BOOKSHELVES");
        while (await bookshelfReader.ReadAsync(cancellationToken))
        {
            var id = Guid.Parse((string)bookshelfReader.GetValue("ID"));
            var name = (string)bookshelfReader.GetValue("NAME");
            var description = (string?)bookshelfReader.GetNullableValue("DESCRIPTION");
            var createdAt = (long)bookshelfReader.GetValue("CREATED_AT");

            bookshelves.Add(new Bookshelf(
                id,
                name,
                description,
                await GetBooksFromBookshelf(connection, id, cancellationToken),
                DateTime.SpecifyKind(new DateTime(createdAt), DateTimeKind.Utc)
            ));
        }

        return bookshelves.ToArray();
    }

    public async Task CreateBookAsync(Guid bookshelfId, Book book, BookContent content,
        CancellationToken cancellationToken)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await connection.ExecuteAsync("""
            INSERT INTO BOOKS (ID, BOOKSHELF_ID, NAME, CONTENT, AUTHOR, CREATED_AT, LAST_OPENED_AT)
                VALUES (@Id, @BookshelfId, @Name, @Content, @Author, @CreatedAt, @LastOpenedAt);
        """, new
        {
            Id = book.Id.ToString(),
            BookshelfId = bookshelfId.ToString(),
            Name = book.Metadata.Name,
            Content = JsonSerializer.Serialize(content.RootNode),
            Author = book.Metadata.Author,
            CreatedAt = book.CreatedAt.Ticks,
            LastOpenedAt = book.LastOpenedAt?.Ticks
        });

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task<BookContent> OpenBookAsync(
        Guid bookId,
        DateTime lastOpenedAt,
        CancellationToken cancellationToken)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync(cancellationToken);

        await using var reader = await connection.ExecuteReaderAsync("""
            SELECT CONTENT FROM BOOKS WHERE ID = @Id;
            UPDATE BOOKS SET LAST_OPENED_AT = @LastOpenedAt WHERE ID = @Id;
        """, new { Id = bookId.ToString(), LastOpenedAt = lastOpenedAt.Ticks });

        await reader.ReadAsync(cancellationToken);
        var rootNodeJson = (string)reader.GetValue("CONTENT");
        var rootNode = JsonSerializer.Deserialize<IBookContentNode>(rootNodeJson)
                       ?? throw new InvalidOperationException("Cannot read book content from database");

        return new BookContent(bookId, rootNode);
    }

    public async Task CreateBookshelfAsync(Bookshelf bookshelf, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await connection.ExecuteAsync("""
            INSERT INTO BOOKSHELVES (ID, NAME, DESCRIPTION, CREATED_AT)
                VALUES (@Id, @Name, @Description, @CreatedAt);
        """, new
        {
            Id = bookshelf.Id.ToString(),
            Name = bookshelf.Name,
            Description = bookshelf.Description,
            CreatedAt = bookshelf.CreatedAt.Ticks
        });

        await transaction.CommitAsync(cancellationToken);
    }

    private static async Task<Book[]> GetBooksFromBookshelf(SqliteConnection connection, Guid id,
        CancellationToken cancellationToken)
    {
        await using var bookReader = await connection.ExecuteReaderAsync("""
                SELECT ID, NAME, AUTHOR, CREATED_AT, LAST_OPENED_AT FROM BOOKS
                WHERE BOOKSHELF_ID = @Id;
            """, new { Id = id.ToString() });

        var books = new List<Book>();

        while (await bookReader.ReadAsync(cancellationToken))
            books.Add(ReadBook(bookReader));

        return books.ToArray();
    }

    private static Book ReadBook(DbDataReader bookReader)
    {
        var id = Guid.Parse((string)bookReader.GetValue("ID"));
        var name = (string)bookReader.GetValue("NAME");
        var author = (string?)bookReader.GetNullableValue("AUTHOR");
        var createdAt = (long)bookReader.GetValue("CREATED_AT");
        var lastOpenedAt = (long?)bookReader.GetNullableValue("LAST_OPENED_AT");

        return new Book(
            id,
            new BookMetadata(name, author),
            DateTime.SpecifyKind(new DateTime(createdAt), DateTimeKind.Utc),
            lastOpenedAt is null ? null : DateTime.SpecifyKind(new DateTime(lastOpenedAt.Value), DateTimeKind.Utc)
        );
    }
}