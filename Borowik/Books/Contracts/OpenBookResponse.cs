using Borowik.Books.Entities;

namespace Borowik.Books.Contracts;

public record OpenBookResponse(Book Book, BookContent Content);