using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Books.Commands;

internal class OpenBookCommandHandler : CommandHandler<OpenBookCommand, BookContent>
{
    protected override Task<BookContent> HandleAsync(OpenBookCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}