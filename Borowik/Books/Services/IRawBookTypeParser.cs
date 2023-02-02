using Borowik.Books.Entities;

namespace Borowik.Books.Services;

internal interface IRawBookTypeParser
{
    public RawBookType SupportedType { get; }
    public Task<(BookContentPage[], BookMetadata)> ParseAsync(byte[] content, CancellationToken cancellationToken);
}