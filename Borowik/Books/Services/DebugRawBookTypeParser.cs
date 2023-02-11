using Borowik.Books.Entities;
using Borowik.Services;
using Scrutor;

namespace Borowik.Books.Services;

[ServiceDescriptor(typeof(IRawBookTypeParser))]
internal class DebugRawBookTypeParser : IRawBookTypeParser
{
    private readonly IGuidProvider _guidProvider;

    public DebugRawBookTypeParser(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider;
    }

    public RawBookType SupportedType => RawBookType.Debug;

    public async Task<(BookContentPage[], BookMetadata)> ParseAsync(Stream stream, CancellationToken cancellationToken)
    {
        using var streamReader = new StreamReader(stream);
        var text = await streamReader.ReadToEndAsync(cancellationToken);
        var parts = text.Split('|', 3);
        Array.Resize(ref parts, 3);

        var node = new BookContentNodes.PlainTextNode(_guidProvider.Generate(), parts[2]);
        var pages = new[] { new BookContentPage(new IBookContentNode[] { node }) };

        var metadata = new BookMetadata(parts[0], parts[1], null);

        return (pages, metadata);
    }
}