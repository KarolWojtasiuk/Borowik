using Borowik;
using Borowik.Books.Commands;
using Borowik.Books.Queries;
using Borowik.Commands;
using Borowik.Database.Sqlite;
using Borowik.Queries;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddBorowik()
    .AddBorowikSqlite()
    .BuildServiceProvider();


var commander = serviceProvider.GetRequiredService<ICommander>();
var querier = serviceProvider.GetRequiredService<IQuerier>();

var bookshelf = await commander.SendCommandAsync(new CreateNewBookshelfCommand("Name"), CancellationToken.None);
var remoteBookshelf = await querier.SendQueryAsync(new GetBookshelfQuery(bookshelf.Id), CancellationToken.None);

var same = bookshelf == remoteBookshelf;

Console.WriteLine();