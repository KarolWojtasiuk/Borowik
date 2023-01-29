namespace Borowik.Books.Entities;

public record Book(Guid Id, BookMetadata Metadata, DateTime CreatedAt, DateTime? LastOpenedAt);