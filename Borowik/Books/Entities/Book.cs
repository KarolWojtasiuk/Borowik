namespace Borowik.Books.Entities;

public record Book(string? Name, string? Author, byte[]? Cover, BookContent Content, DateTime CreatedAt, DateTime? LastOpenedAt);