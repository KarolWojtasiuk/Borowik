using System.Drawing;
using System.Text;
using Borowik;
using Borowik.Books;
using Borowik.Books.Entities;
using Borowik.Database.LiteDb;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddBorowik()
    .AddBorowikLiteDb<LiteDbProvider>()
    .BuildServiceProvider();

var manager = serviceProvider.GetRequiredService<IBookshelfManager>();
var bookshelf = await manager.CreateBookshelfAsync("test name", "siema", Color.Red, CancellationToken.None);

var book1 = await manager.ImportBookAsync(bookshelf.Id, RawBookType.PlainText, "Hakuna Matata"u8.ToArray(), CancellationToken.None);
var book2 = await manager.ImportBookAsync(bookshelf.Id, RawBookType.PlainText, "test"u8.ToArray(), CancellationToken.None);
var (content, lastOpenedAt) = await manager.OpenBookAsync(book1.Id, CancellationToken.None);


var remoteBookshelf = await manager.GetBookshelfAsync(bookshelf.Id, CancellationToken.None);
Console.WriteLine();


internal class LiteDbProvider : ICustomLiteDbProvider
{
    public Task<LiteDatabase> GetLiteDatabase(BsonMapper mapper, CancellationToken cancellationToken)
    {
        return Task.FromResult(new LiteDatabase("database.db", mapper));
    }
}