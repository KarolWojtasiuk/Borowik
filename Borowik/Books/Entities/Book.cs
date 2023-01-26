namespace Borowik.Books.Entities;

public record Book(string? Name, string? Author, byte[]? Cover, DateTime CreatedAt, DateTime? LastOpenedAt);