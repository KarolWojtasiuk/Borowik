using System.Drawing;
using Borowik.Books.Entities;
using LiteDB;

namespace Borowik.Database.LiteDb.Mappings;

internal sealed class LiteDbMapper : BsonMapper
{
    private readonly IBookContentPageSerializer _serializer;

    public LiteDbMapper(IBookContentPageSerializer serializer)
    {
        _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

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

        RegisterType<BookContentPage>(
            v => _serializer.Serialize(v),
            v => _serializer.Deserialize(v));
    }

    private void ConfigureMapperEntities()
    {
        Entity<Bookshelf>()
            .Id(e => e.Id)
            .Ignore(e => e.Books)
            .Ctor(d => new Bookshelf(
                Deserialize<Guid>(d["_id"]),
                Deserialize<string>(d[nameof(Bookshelf.Name)]),
                Deserialize<string?>(d[nameof(Bookshelf.Description)]),
                Deserialize<Color>(d[nameof(Bookshelf.Color)]),
                Array.Empty<Book>(),
                Deserialize<DateTime>(d[nameof(Bookshelf.CreatedAt)])));

        Entity<Book>()
            .Id(e => e.Id)
            .Ctor(d => new Book(
                Deserialize<Guid>(d["_id"]),
                Deserialize<Guid>(d[nameof(Book.BookshelfId)]),
                Deserialize<BookMetadata>(d[nameof(Book.Metadata)]),
                Deserialize<DateTime>(d[nameof(Book.CreatedAt)]),
                Deserialize<DateTime?>(d[nameof(Book.LastOpenedAt)])));

        Entity<BookContent>()
            .Id(e => e.BookId)
            .Ctor(d => new BookContent(
                Deserialize<Guid>(d["_id"]),
                Deserialize<BookContentPage[]>(d[nameof(BookContent.Pages)])));
    }
} 