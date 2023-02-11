using Borowik.Books.Entities;
using Borowik.Services;
using Scrutor;

namespace Borowik.Books.Services;

[ServiceDescriptor(typeof(IRawBookTypeParser))]
internal class PlainTextRawBookTypeParser : IRawBookTypeParser
{
    private readonly IGuidProvider _guidProvider;

    public PlainTextRawBookTypeParser(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider;
    }

    public RawBookType SupportedType => RawBookType.PlainText;

    public async Task<(BookContentPage[], BookMetadata)> ParseAsync(Stream stream, CancellationToken cancellationToken)
    {
        using var streamReader = new StreamReader(stream);
        var text = await streamReader.ReadToEndAsync(cancellationToken);

        var node = new BookContentNodes.PlainTextNode(_guidProvider.Generate(), text);
        var pages = new [] { new BookContentPage(new IBookContentNode[] { node }) };

        var metadata = new BookMetadata("Plain Text", string.Empty, null);

        return (pages, metadata);
    }
}