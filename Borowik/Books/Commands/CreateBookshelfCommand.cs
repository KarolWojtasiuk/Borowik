using System.Drawing;
using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Books.Commands;

public record CreateBookshelfCommand(string Name, string Description, Color Color) : ICommand<Bookshelf>;