using Borowik.Books.Contracts;
using Borowik.Books.Entities;
using Borowik.Services;
using MediatR;

namespace Borowik.Books.Handlers;

internal class CreateBookshelfRequestHandler : IRequestHandler<CreateBookshelfRequest, CreateBookshelfResponse>
{
    private readonly IBookshelfRepository _bookshelfRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;

    public CreateBookshelfRequestHandler(
        IBookshelfRepository bookshelfRepository,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider)
    {
        _bookshelfRepository = bookshelfRepository;
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
    }

    public async Task<CreateBookshelfResponse> Handle(CreateBookshelfRequest request, CancellationToken cancellationToken)
    {
        var bookshelf = new Bookshelf(
            _guidProvider.Generate(),
            request.Name,
            request.Description,
            request.Color,
            BooksSortMode.ImportedAtDescending,
            _dateTimeProvider.GetUtcNew());

        await _bookshelfRepository.CreateAsync(bookshelf, cancellationToken);

        return new CreateBookshelfResponse(bookshelf);
    }
}