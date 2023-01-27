namespace Borowik.Books.Entities;

public record Book(Guid Id, string Name, string? Author, byte[]? Cover, DateTime CreatedAt, DateTime? LastOpenedAt);