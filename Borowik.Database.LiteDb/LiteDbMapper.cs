using System.Drawing;
using Borowik.Books.Entities;
using LiteDB;

namespace Borowik.Database.LiteDb;

internal sealed class LiteDbMapper : BsonMapper
{
    public LiteDbMapper()
    {
        ConfigureMapperSettings();
        ConfigureMapperTypes();
        ConfigureMapperEntities();
    }
    
    private void ConfigureMapperSettings()
    {
        SerializeNullValues = true;
        EmptyStringToNull = false;
    }

    private void ConfigureMapperTypes()
    {
        RegisterType<Color>(
            v => v.ToArgb(),
            v => Color.FromArgb(v));

        RegisterType<DateTime>(
            v => v.Ticks,
            v => DateTime.SpecifyKind(new DateTime(v), DateTimeKind.Utc));
    }

    private void ConfigureMapperEntities()
    {
        Entity<Bookshelf>()
            .Id(e => e.Id)
            .DbRef(e => e.Books);

        Entity<Book>()
            .Id(e => e.Id);

        Entity<BookContent>()
            .Id(e => e.BookId);
    }
} 