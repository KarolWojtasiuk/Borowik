using Borowik.Books.Entities;

namespace Borowik.Database.LiteDb;

internal interface IBookContentPageSerializer
{
    public string Serialize(BookContentPage page);
    public BookContentPage Deserialize(string text);
}