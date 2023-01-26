using System.Drawing;
using Borowik.Books.Entities;
using Borowik.Commands;
using Borowik.Entities;
using Borowik.Services;

namespace Borowik.Books.Commands;

internal class CreateNewBookshelfCommandHandler : CommandHandler<CreateNewBookshelfCommand, Bookshelf>
{
    private readonly IGuidProvider _guidProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEntityRepository<Bookshelf, Guid> _bookshelfRepository;

    public CreateNewBookshelfCommandHandler(
        IGuidProvider guidProvider,
        IDateTimeProvider dateTimeProvider,
        IEntityRepository<Bookshelf, Guid> bookshelfRepository)
    {
        _guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _bookshelfRepository = bookshelfRepository ?? throw new ArgumentNullException(nameof(bookshelfRepository));
    }

    protected override async Task<Bookshelf> HandleAsync(CreateNewBookshelfCommand command, CancellationToken cancellationToken)
    {
        var bookshelf = new Bookshelf(
            _guidProvider.Generate(),
            command.Name,
            string.Empty,
            Array.Empty<Book>(),
            Color.FromArgb(Color.White.ToArgb()), // Without this C# interprets color as KnownColor
            _dateTimeProvider.GetUtcNew());

        await _bookshelfRepository.CreateAsync(bookshelf, cancellationToken);

        return bookshelf;
    }
}