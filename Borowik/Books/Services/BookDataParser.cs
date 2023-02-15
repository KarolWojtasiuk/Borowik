using System.Diagnostics;
using Borowik.Books.Entities;
using Scrutor;

namespace Borowik.Books.Services;

[ServiceDescriptor(typeof(IBookDataParser))]
internal class BookDataParser : IBookDataParser
{
    private readonly IEnumerable<IBookDataTypeParser> _rawBookTypeParsers;

    public BookDataParser(IEnumerable<IBookDataTypeParser> rawBookTypeParsers)
    {
        _rawBookTypeParsers = rawBookTypeParsers ?? throw new ArgumentNullException(nameof(rawBookTypeParsers));
    }

    public async Task<BookContentPage[]> ParseAsync(BookType type, byte[] data, CancellationToken cancellationToken)
    {
        var parser = _rawBookTypeParsers.SingleOrDefault(p => p.SupportedType == type)
                     ?? throw new InvalidOperationException("Raw book type '{type}' is not supported");

        return await parser.ParseAsync(data, cancellationToken);
    }

    public async Task<BookMetadata> ExtractMetadataAsync(BookType type, byte[] data, CancellationToken cancellationToken)
    {
        var parser = _rawBookTypeParsers.SingleOrDefault(p => p.SupportedType == type)
                     ?? throw new InvalidOperationException("Raw book type '{type}' is not supported");

        var metadata = await parser.ExtractMetadataAsync(data, cancellationToken);
        Debug.Assert(metadata.Type == type);
        return metadata;
    }
}