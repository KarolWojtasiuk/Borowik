using System.Text;
using Borowik.Books.Entities;
using Borowik.Services;
using Scrutor;

namespace Borowik.Books.Services;

[ServiceDescriptor(typeof(IBookDataTypeParser))]
internal class PlainTextBookDataTypeParser : IBookDataTypeParser
{
    private readonly IGuidProvider _guidProvider;

    public PlainTextBookDataTypeParser(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider;
    }

    public BookType SupportedType => BookType.PlainText;

    public Task<BookMetadata> ExtractMetadataAsync(byte[] data, CancellationToken cancellationToken)
    {
        return Task.FromResult(new BookMetadata("Plain Text", string.Empty, null, BookType.PlainText));
    }

    public Task<BookContent> ParseAsync(byte[] data, CancellationToken cancellationToken)
    {
        var text = Encoding.UTF8.GetString(data);
        var node = new BookContentNodes.PlainTextNode(_guidProvider.Generate(), text);
        var pages = new [] { new BookContentPage(new IBookContentNode[] { node }) };

        return Task.FromResult(new BookContent(pages));
    }
}