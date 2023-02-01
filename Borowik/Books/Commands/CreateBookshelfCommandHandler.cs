using Borowik.Books.Entities;
using Borowik.Commands;
using Borowik.Services;

namespace Borowik.Books.Commands;

internal class CreateBookshelfCommandHandler : CommandHandler<CreateBookshelfCommand, Bookshelf>
{
    private readonly IGuidProvider _guidProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBookshelfRepository _bookshelfRepository;

    public CreateBookshelfCommandHandler(
        IGuidProvider guidProvider,
        IDateTimeProvider dateTimeProvider,
        IBookshelfRepository bookshelfRepository)
    {
        _guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _bookshelfRepository = bookshelfRepository ?? throw new ArgumentNullException(nameof(bookshelfRepository));
    }

    protected override async Task<Bookshelf> HandleAsync(CreateBookshelfCommand command, CancellationToken cancellationToken)
    {
        var bookshelf = new Bookshelf(
            _guidProvider.Generate(),
            command.Name,
            command.Description,
            Array.Empty<Book>(),
            _dateTimeProvider.GetUtcNew());

        await _bookshelfRepository.CreateBookshelfAsync(bookshelf, cancellationToken);

        return bookshelf;
    }
}