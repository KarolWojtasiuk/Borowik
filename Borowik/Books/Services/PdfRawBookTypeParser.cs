using Borowik.Books.Entities;
using Borowik.Services;
using Scrutor;
using SixLabors.ImageSharp;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace Borowik.Books.Services;

[ServiceDescriptor(typeof(IRawBookTypeParser))]
internal class PdfRawBookTypeParser : IRawBookTypeParser
{
    private readonly IGuidProvider _guidProvider;

    public PdfRawBookTypeParser(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider;
    }

    public RawBookType SupportedType => RawBookType.Pdf;

    public Task<(BookContentPage[], BookMetadata)> ParseAsync(Stream stream, CancellationToken cancellationToken)
    {
        using var pdf = PdfDocument.Open(stream);
        var metadata = ParseMetadata(pdf);
        var pages = pdf.GetPages().Select(ParsePage).ToArray();

        return Task.FromResult((pages, metadata));
    }

    private BookContentPage ParsePage(Page page)
    {
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

        Console.WriteLine($"Parsed page {page.Number}");

        return new BookContentPage(nodes.ToArray());
    }

    private static BookMetadata ParseMetadata(PdfDocument pdf)
    {
        var title = pdf.Information.Title;
        return new BookMetadata(
            string.IsNullOrEmpty(title) ? "PDF File" : title,
            pdf.Information.Author,
            GetCoverImage(pdf));
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