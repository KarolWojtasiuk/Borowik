using Borowik.Books.Entities;

namespace Borowik.Books.Contracts;

public record GetBookshelvesResponse(Bookshelf[] Bookshelves);