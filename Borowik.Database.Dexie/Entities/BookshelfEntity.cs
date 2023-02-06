using Borowik.Books.Entities;
using DexieNET;

namespace Borowik.Database.Dexie.Entities;

public record BookshelfEntity
(
    [property: Index(IsPrimary = true, IsUnique = true)] Guid Id,
    [property: Index] string Name,
    [property: Index] string? Description,
    [property: Index] int Color,
    [property: Index] DateTime CreatedAt
) : IBorowikEntityStore, IEntity<BookshelfEntity, Bookshelf>
{
    public Bookshelf Map()
    {
        return new Bookshelf(
            Id,
            Name,
            Description,
            System.Drawing.Color.FromArgb(Color),
            Array.Empty<Book>(),
            CreatedAt);
    }

    public static BookshelfEntity Map(Bookshelf baseEntity)
    {
        return new BookshelfEntity(
            baseEntity.Id,
            baseEntity.Name,
            baseEntity.Description,
            baseEntity.Color.ToArgb(),
            baseEntity.CreatedAt);
    }
}