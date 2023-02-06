namespace Borowik.Books.Entities;

public record BookMetadata
(
    string Name,
    string? Author,
    byte[]? Cover
);