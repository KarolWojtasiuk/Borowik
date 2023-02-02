using System.Drawing;
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
await manager.CreateBookshelfAsync("test name empty", "", Color.Black, CancellationToken.None);
await manager.CreateBookshelfAsync("test name null", null, default, CancellationToken.None);

var book = await manager.ImportBookAsync(bookshelf.Id, RawBookType.PlainText, Array.Empty<byte>(), CancellationToken.None);
var (content, lastOpenedAt) = await manager.OpenBookAsync(book.Id, CancellationToken.None);

Console.WriteLine();


internal class LiteDbProvider : ICustomLiteDbProvider
{
    public Task<LiteDatabase> GetLiteDatabase(BsonMapper mapper, CancellationToken cancellationToken)
    {
        return Task.FromResult(new LiteDatabase("database.db", mapper));
    }
}