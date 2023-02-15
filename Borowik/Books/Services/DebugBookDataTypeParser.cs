using System.Text;
using Borowik.Books.Entities;
using Borowik.Services;
using Scrutor;

namespace Borowik.Books.Services;

[ServiceDescriptor(typeof(IBookDataTypeParser))]
internal class DebugBookDataTypeParser : IBookDataTypeParser
{
    private readonly IGuidProvider _guidProvider;

    public DebugBookDataTypeParser(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider;
    }

    public BookType SupportedType => BookType.Debug;

    public Task<BookMetadata> ExtractMetadataAsync(byte[] data, CancellationToken cancellationToken)
    {
        var text = Encoding.UTF8.GetString(data);
        var parts = text.Split('|', 3);
        Array.Resize(ref parts, 3);

        return Task.FromResult(new BookMetadata(parts[0], parts[1], null, BookType.Debug));
    }

    public Task<BookContentPage[]> ParseAsync(byte[] data, CancellationToken cancellationToken)
    {
        var text = Encoding.UTF8.GetString(data);
        var parts = text.Split('|', 3);
        Array.Resize(ref parts, 3);

        var node = new BookContentNodes.PlainTextNode(_guidProvider.Generate(), parts[2]);
        var pages = new[] { new BookContentPage(new IBookContentNode[] { node }) };

        return Task.FromResult(pages);
    }
}