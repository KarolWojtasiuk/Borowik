using Borowik.Books.Entities;
using Borowik.Commands;
using Borowik.Services;

namespace Borowik.Books.Commands;

internal class OpenBookCommandHandler : CommandHandler<OpenBookCommand, BookContent>
{
    private readonly IBookshelfRepository _bookshelfRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OpenBookCommandHandler(
        IBookshelfRepository bookshelfRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _bookshelfRepository = bookshelfRepository ?? throw new ArgumentNullException(nameof(bookshelfRepository));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    protected override Task<BookContent> HandleAsync(OpenBookCommand command, CancellationToken cancellationToken)
    {
        var lastOpenedAt = _dateTimeProvider.GetUtcNew();
        return _bookshelfRepository.OpenBookAsync(command.Id, lastOpenedAt, cancellationToken);
    }
}