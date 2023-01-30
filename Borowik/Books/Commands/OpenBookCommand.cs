using Borowik.Books.Entities;
using Borowik.Commands;

namespace Borowik.Books.Commands;

public record OpenBookCommand(Guid Id) : ICommand<(BookContent, DateTime)>;