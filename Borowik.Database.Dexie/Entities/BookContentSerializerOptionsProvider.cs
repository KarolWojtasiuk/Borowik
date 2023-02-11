using System.Text.Json;
using Borowik.Books.Entities;

namespace Borowik.Database.Dexie.Entities;

public static class BookContentSerializerOptionsProvider
{
    private static JsonSerializerOptions? _options;

    public static JsonSerializerOptions Get()
    {
        return _options ??= new JsonSerializerOptions
        {
            TypeInfoResolver = new BookContentNodeTypesResolver()
        };
    }
}