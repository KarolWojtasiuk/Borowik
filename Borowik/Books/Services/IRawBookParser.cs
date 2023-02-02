using Borowik.Books.Entities;

namespace Borowik.Books.Services;

internal interface IRawBookParser
{
    public Task<(BookContentPage[], BookMetadata)> ParseAsync(RawBookType type, byte[] content, CancellationToken cancellationToken);
}