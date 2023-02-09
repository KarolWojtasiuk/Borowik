namespace Borowik.Books.Entities;

public record Book
(
    Guid Id,
    Guid BookshelfId,
    BookMetadata Metadata,
    DateTime ImportedAt,
    DateTime? LastOpenedAt
);