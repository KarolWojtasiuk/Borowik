using System.Text;
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

    public Task<(BookContentPage[], BookMetadata)> ParseAsync(byte[] content, CancellationToken cancellationToken)
    {
        var parts = Encoding.UTF8.GetString(content).Split('|', 3);
        Array.Resize(ref parts, 3);

        var node = new BookContentNodes.PlainTextNode(_guidProvider.Generate(), parts[2]);
        var pages = new[] { new BookContentPage(new IBookContentNode[] { node }) };

        var metadata = new BookMetadata(parts[0], parts[1], null);

        return Task.FromResult((pages, metadata));
    }
}