using System.Text;
using Borowik.Books.Entities;
using Borowik.Services;
using Scrutor;
using SixLabors.ImageSharp;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace Borowik.Books.Services;

[ServiceDescriptor(typeof(IBookDataTypeParser))]
internal class PdfBookDataTypeParser : IBookDataTypeParser
{
    private readonly IGuidProvider _guidProvider;

    public PdfBookDataTypeParser(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider;
    }

    public BookType SupportedType => BookType.Pdf;

    public Task<BookMetadata> ExtractMetadataAsync(byte[] data, CancellationToken cancellationToken)
    {
        using var pdf = PdfDocument.Open(data);
        return Task.FromResult(ParseMetadata(pdf));
    }

    public Task<BookContent> ParseAsync(byte[] data, CancellationToken cancellationToken)
    {
        using var pdf = PdfDocument.Open(data);
        var pages = pdf.GetPages()
            .Select(ParsePage)
            .Where(p => p.Nodes.Length > 0)
            .ToArray();

        return Task.FromResult(new BookContent(pages));
    }

    private BookContentPage ParsePage(Page page)
    {
        Console.WriteLine(page.Number);
        var nodes = new List<IBookContentNode>();

        if (!string.IsNullOrWhiteSpace(page.Text))
            nodes.Add(new BookContentNodes.PlainTextNode(_guidProvider.Generate(), page.Text));

        if (page.NumberOfImages > 0)
        {
            var images = page
                .GetImages()
                .Select(GetBookImageFromPdfImage)
                .Where(i => i is not null)
                .Select(i => new BookContentNodes.ImageNode(_guidProvider.Generate(), i!));

            nodes.AddRange(images);
        }

        return new BookContentPage(nodes.ToArray());
    }

    private static BookMetadata ParseMetadata(PdfDocument pdf)
    {
        var title = pdf.Information.Title;
        var author = pdf.Information.Author;
        return new BookMetadata(
            string.IsNullOrWhiteSpace(title) ? "PDF File" : title,
            string.IsNullOrWhiteSpace(author) ? string.Empty : author,
            GetCoverImage(pdf),
            BookType.Pdf);
    }

    private static BookImage? GetCoverImage(PdfDocument pdf)
    {
        if (pdf.NumberOfPages == 0)
            return null;

        var pdfImage = pdf.GetPage(1).GetImages().FirstOrDefault();
        return pdfImage is null
            ? null
            : GetBookImageFromPdfImage(pdfImage);
    }

    private static BookImage? GetBookImageFromPdfImage(IPdfImage pdfImage)
    {
        var imageFormat = Image.DetectFormat(pdfImage.RawBytes.ToArray());
        if (imageFormat is not null)
            return new BookImage(pdfImage.RawBytes.ToArray(), imageFormat.DefaultMimeType);

        Console.WriteLine("Cannot detect format of image");
        return null;
    }
}