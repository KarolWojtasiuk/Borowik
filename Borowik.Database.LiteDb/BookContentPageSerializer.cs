using System.Text.Json;
using Borowik.Books.Entities;

namespace Borowik.Database.LiteDb;

internal class BookContentPageSerializer : IBookContentPageSerializer
{
    private static readonly JsonSerializerOptions Options = CreateSerializerOptions();

    public string Serialize(BookContentPage page)
    {
        return JsonSerializer.Serialize(page, Options);
    }

    public BookContentPage Deserialize(string text)
    {
        return JsonSerializer.Deserialize<BookContentPage>(text, Options)
               ?? throw new InvalidOperationException($"Cannot deserialize {nameof(BookContentPage)}");
    }

    private static JsonSerializerOptions CreateSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            TypeInfoResolver = new PolymorphicTypeResolver()
        };
    }
}