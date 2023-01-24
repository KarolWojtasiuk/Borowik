using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Books.Commands;

public record CreateNewBookshelfCommand(string Name) : ICommand<Bookshelf>;