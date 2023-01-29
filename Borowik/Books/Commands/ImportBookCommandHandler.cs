using Borowik.Books.Entities;
using Borowik.Books.Services;
using Borowik.Commands;
using Borowik.Services;

namespace Borowik.Books.Commands;

internal class ImportBookCommandHandler : CommandHandler<ImportBookCommand, Book>
{
    private readonly IBookshelfRepository _bookshelfRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IRawBookParser _rawBookParser;

    public ImportBookCommandHandler(
        IBookshelfRepository bookshelfRepository,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider,
        IRawBookParser rawBookParser)
    {
        _bookshelfRepository = bookshelfRepository ?? throw new ArgumentNullException(nameof(bookshelfRepository));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
        _rawBookParser = rawBookParser ?? throw new ArgumentNullException(nameof(rawBookParser));
    }

    protected override async Task<Book> HandleAsync(ImportBookCommand command, CancellationToken cancellationToken)
    {
        var (node, metadata) = await _rawBookParser.ParseAsync(command.Type, command.Content, cancellationToken);

        var id = _guidProvider.Generate();
        var book = new Book(id, metadata, _dateTimeProvider.GetUtcNew(), null);
        var content = new BookContent(id, node);
        await _bookshelfRepository.CreateBookAsync(command.BookshelfId, book, content, cancellationToken);

        return book;
    }
}