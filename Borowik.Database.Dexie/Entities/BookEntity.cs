using Borowik.Books.Entities;
using DexieNET;

namespace Borowik.Database.Dexie.Entities;

public record BookEntity
(
    [property: Index(IsPrimary = true, IsUnique = true)]
    Guid Id,
    [property: Index] Guid BookshelfId,
    [property: Index] string Name,
    [property: Index] string Author,
    [property: ByteIndex] byte[]? Cover,
    [property: Index] DateTime CreatedAt,
    [property: Index] DateTime? LastOpenedAt
) : IBorowikEntityStore, IEntity<BookEntity, Book>
{
    public Book Map()
    {
        return new Book(
            Id,
            BookshelfId,
            new BookMetadata(Name, Author, Cover),
            CreatedAt,
            LastOpenedAt);
    }

    public static BookEntity Map(Book baseEntity)
    {
        return new BookEntity(
            baseEntity.Id,
            baseEntity.BookshelfId,
            baseEntity.Metadata.Name,
            baseEntity.Metadata.Author,
            baseEntity.Metadata.Cover,
            baseEntity.ImportedAt,
            baseEntity.LastOpenedAt);
    }
}