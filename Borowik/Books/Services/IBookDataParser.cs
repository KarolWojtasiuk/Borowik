using Borowik.Books.Entities;

namespace Borowik.Books.Services;

internal interface IBookDataParser
{
    public Task<BookContent> ParseAsync(BookType type, byte[] data, CancellationToken cancellationToken);
    public Task<BookMetadata> ExtractMetadataAsync(BookType type, byte[] data, CancellationToken cancellationToken);
}