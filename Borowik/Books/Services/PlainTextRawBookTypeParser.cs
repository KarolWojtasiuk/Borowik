using System.Text;
using Borowik.Books.Entities;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Books.Services;

[ServiceDescriptor<IRawBookTypeParser>(ServiceLifetime.Transient)]
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