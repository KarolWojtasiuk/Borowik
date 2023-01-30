using Borowik.Books.Entities;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Books.Services;

[ServiceDescriptor<IRawBookParser>(ServiceLifetime.Transient)]
internal class RawBookParser : IRawBookParser
{
    private readonly IEnumerable<IRawBookTypeParser> _rawBookTypeParsers;

    public RawBookParser(IEnumerable<IRawBookTypeParser> rawBookTypeParsers)
    {
        _rawBookTypeParsers = rawBookTypeParsers ?? throw new ArgumentNullException(nameof(rawBookTypeParsers));
    }

    public async Task<(IBookContentNode, BookMetadata)> ParseAsync(RawBookType type, byte[] content, CancellationToken cancellationToken)
    {
        var parser = _rawBookTypeParsers.SingleOrDefault(p => p.SupportedType == type)
                     ?? throw new InvalidOperationException("Raw book type '{type}' is not supported");

        return await parser.ParseAsync(content, cancellationToken);
    }
}