using Borowik.Books.Entities;
using Scrutor;
using SixLabors.ImageSharp;
using VersOne.Epub;

namespace Borowik.Books.Services;

[ServiceDescriptor(typeof(IBookDataTypeParser))]
internal class EpubBookDataTypeParser : IBookDataTypeParser
{
    public BookType SupportedType => BookType.Epub;

    public async Task<BookMetadata> ExtractMetadataAsync(byte[] data, CancellationToken cancellationToken)
    {
        var memoryStream = new MemoryStream(data);
        using var epub = await EpubReader.OpenBookAsync(memoryStream);

        var cover = await epub.ReadCoverAsync();

        return new BookMetadata(
            epub.Title,
            epub.Author,
            CreateBookImage(cover),
            BookType.Epub);
    }

    public async Task<BookContent> ParseAsync(byte[] data, CancellationToken cancellationToken)
    {
        var memoryStream = new MemoryStream(data);
        using var epub = await EpubReader.OpenBookAsync(memoryStream);

        var pages = new List<BookContentPage>();

        foreach (var file in await epub.GetReadingOrderAsync())
        {
            var content = await file.ReadContentAsync();
            pages.Add(ParsePage(content));

        }

        return new BookContent(pages.ToArray());
    }

    private BookContentPage ParsePage(string content)
    {
        //TODO
        return new BookContentPage(new IBookContentNode[] { new BookContentNodes.ParagraphNode(Guid.Empty, content) });
    }

    private static BookImage? CreateBookImage(byte[]? data)
    {
        if (data is null)
            return null;

        var imageFormat = Image.DetectFormat(data);
        if (imageFormat is not null)
            return new BookImage(data, imageFormat.DefaultMimeType);

        Console.WriteLine("Cannot detect format of image");
        return null;
    }
}