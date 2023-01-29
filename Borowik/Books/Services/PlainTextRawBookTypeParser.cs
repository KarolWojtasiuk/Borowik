using System.Text;
using Borowik.Books.Entities;

namespace Borowik.Books.Services;

internal class PlainTextRawBookTypeParser : IRawBookTypeParser
{
    public RawBookType SupportedType => RawBookType.PlainText;

    public Task<(IBookContentNode, BookMetadata)> ParseAsync(byte[] content, CancellationToken cancellationToken)
    {
        var node = new BookContentNodes.PlainTextNode(Encoding.UTF8.GetString(content));
        var metadata = new BookMetadata("Plain Text", null, null);
        return Task.FromResult<(IBookContentNode, BookMetadata)>((node, metadata));
    }
}