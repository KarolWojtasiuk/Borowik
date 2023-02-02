using Borowik.Books.Entities;

namespace Borowik.Books.Services;

internal class RawBookParser : IRawBookParser
{
    private readonly IEnumerable<IRawBookTypeParser> _rawBookTypeParsers;

    public RawBookParser(IEnumerable<IRawBookTypeParser> rawBookTypeParsers)
    {
        _rawBookTypeParsers = rawBookTypeParsers ?? throw new ArgumentNullException(nameof(rawBookTypeParsers));
    }

    public async Task<(BookContentPage[], BookMetadata)> ParseAsync(RawBookType type, byte[] content, CancellationToken cancellationToken)
    {
        var parser = _rawBookTypeParsers.SingleOrDefault(p => p.SupportedType == type)
                     ?? throw new InvalidOperationException("Raw book type '{type}' is not supported");

        return await parser.ParseAsync(content, cancellationToken);
    }
}