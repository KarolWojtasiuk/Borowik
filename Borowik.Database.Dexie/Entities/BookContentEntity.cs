using System.Text.Json;
using Borowik.Books.Entities;
using DexieNET;

namespace Borowik.Database.Dexie.Entities;

public record BookContentEntity
(
    [property: Index(IsPrimary = true, IsUnique = true)] Guid BookId,
    [property: Index] string Pages
) : IBorowikEntityStore, IEntity<BookContentEntity, BookContent>
{
    public BookContent Map()
    {
        var pages = JsonSerializer.Deserialize<BookContentPage[]>(Pages, BookContentSerializerOptionsProvider.Get())
                    ?? throw new InvalidOperationException("Cannot deserialize book content pages");
        return new BookContent(BookId, pages);
    }

    public static BookContentEntity Map(BookContent baseEntity)
    {
        var pages = JsonSerializer.Serialize(baseEntity.Pages, BookContentSerializerOptionsProvider.Get());
        return new BookContentEntity(baseEntity.BookId, pages);
    }
}