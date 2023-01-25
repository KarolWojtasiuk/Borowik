using Borowik;
using Borowik.Books.Commands;
using Borowik.Commands;
using Borowik.Database.Sqlite;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddBorowik()
    .AddBorowikSqlite()
    .BuildServiceProvider();


var commander = serviceProvider.GetRequiredService<ICommander>();
var bookshelf = await commander.SendCommandAsync(new CreateNewBookshelfCommand("Name"), CancellationToken.None);

Console.WriteLine();