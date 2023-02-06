namespace Borowik.Books.Entities;

public record BookContent
(
    Guid BookId,
    BookContentPage[] Pages
);