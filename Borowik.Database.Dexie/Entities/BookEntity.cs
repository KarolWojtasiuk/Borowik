using Borowik.Books.Entities;
using DexieNET;

namespace Borowik.Database.Dexie.Entities;

public record BookEntity
(
    [property: Index(IsPrimary = true, IsUnique = true)]
    Guid Id,
    [property: Index] Guid BookshelfId,
    [property: Index] string Title,
    [property: Index] string Author,
    [property: ByteIndex] byte[]? CoverData,
    [property: Index] string? CoverMimeType,
    [property: Index] DateTime CreatedAt,
    [property: Index] DateTime? LastOpenedAt
) : IBorowikEntityStore, IEntity<BookEntity, Book>
{
    public Book Map()
    {
        var cover = CoverData is null || CoverMimeType is null
            ? null
            : new BookImage(CoverData, CoverMimeType);

        return new Book(
            Id,
            BookshelfId,
            new BookMetadata(Title, Author, cover),
            CreatedAt,
            LastOpenedAt);
    }

    public static BookEntity Map(Book baseEntity)
    {
        return new BookEntity(
            baseEntity.Id,
            baseEntity.BookshelfId,
            baseEntity.Metadata.Title,
            baseEntity.Metadata.Author,
            baseEntity.Metadata.Cover?.Data,
            baseEntity.Metadata.Cover?.MimeType,
            baseEntity.ImportedAt,
            baseEntity.LastOpenedAt);
    }
}