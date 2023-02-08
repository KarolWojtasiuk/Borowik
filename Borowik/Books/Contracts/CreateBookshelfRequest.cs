using System.Drawing;

namespace Borowik.Books.Contracts;

public record CreateBookshelfRequest(string Name, string Description, Color Color) : IBorowikRequest<CreateBookshelfResponse>;