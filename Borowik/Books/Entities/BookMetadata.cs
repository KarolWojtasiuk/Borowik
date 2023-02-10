namespace Borowik.Books.Entities;

public record BookMetadata
(
    string Title,
    string Author,
    byte[]? Cover
);