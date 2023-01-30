using Borowik.Books.Entities;
using Borowik.Commands;
using Borowik.Services;

namespace Borowik.Books.Commands;

internal class OpenBookCommandHandler : CommandHandler<OpenBookCommand, (BookContent, DateTime)>
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

    protected override async Task<(BookContent, DateTime)> HandleAsync(OpenBookCommand command, CancellationToken cancellationToken)
    {
        var lastOpenedAt = _dateTimeProvider.GetUtcNew();
        var content = await _bookshelfRepository.OpenBookAsync(command.Id, lastOpenedAt, cancellationToken);
        return (content, lastOpenedAt);
    }
}