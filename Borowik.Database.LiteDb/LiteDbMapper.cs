using Borowik.Books.Entities;
using LiteDB;

namespace Borowik.Database.LiteDb;

internal static class LiteDbMapper
{
    public static void RegisterMappings()
    {
        BsonMapper.Global.Entity<Bookshelf>()
            .Id(e => e.Id)
            .DbRef(e => e.Books);

        BsonMapper.Global.Entity<Book>()
            .Id(e => e.Id);

        BsonMapper.Global.Entity<BookContent>()
            .Id(e => e.BookId);
    }
}