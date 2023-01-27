using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Books.Commands;

public record CreateBookshelfCommand(string Name) : ICommand<Bookshelf>;