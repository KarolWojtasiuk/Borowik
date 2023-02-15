using Borowik.Books.Entities;

namespace Borowik.Books.Services;

internal interface IBookDataTypeParser
{
    public BookType SupportedType { get; }
    public Task<BookMetadata> ExtractMetadataAsync(byte[] data, CancellationToken cancellationToken);
    public Task<BookContentPage[]> ParseAsync(byte[] data, CancellationToken cancellationToken);
}