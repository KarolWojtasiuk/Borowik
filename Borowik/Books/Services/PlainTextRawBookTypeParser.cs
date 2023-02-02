using System.Text;
using Borowik.Books.Entities;
using Borowik.Services;

namespace Borowik.Books.Services;

internal class PlainTextRawBookTypeParser : IRawBookTypeParser
{
    private readonly IGuidProvider _guidProvider;

    public PlainTextRawBookTypeParser(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider;
    }

    public RawBookType SupportedType => RawBookType.PlainText;

    public Task<(BookContentPage[], BookMetadata)> ParseAsync(byte[] content, CancellationToken cancellationToken)
    {
        var node = new BookContentNodes.PlainTextNode(_guidProvider.Generate(), Encoding.UTF8.GetString(content));
        var pages = new [] { new BookContentPage(new BookContentNode[] { node }) };

        var metadata = new BookMetadata("Plain Text", null, null);

        return Task.FromResult((pages, metadata));
    }
}