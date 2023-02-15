namespace Borowik.Books;

public static class BookExceptions
{
    public class BookshelfNotFoundException : Exception
    {
        public BookshelfNotFoundException(Guid bookshelfId)
            : base($"Bookshelf with Id '{bookshelfId}' not found")
        {
        }
    }

    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(Guid bookId)
            : base($"Book with Id '{bookId}' not found")
        {
        }
    }

    public class BookDataNotFoundException : Exception
    {
        public BookDataNotFoundException(Guid bookId)
            : base($"Data for book with Id '{bookId}' not found")
        {
        }
    }
}