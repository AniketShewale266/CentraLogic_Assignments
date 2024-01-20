using Library_Management.Entities;

namespace Library_Management.Interface
{
    public interface IBookInterface
    {
        Task<BookEntity> AddBook(BookEntity book);
        Task<BookEntity> ReadBookById(string bookId);
        Task<IEnumerable<BookEntity>> ReadAllBooks();
        Task<BookEntity> UpdateBook(string bookId, BookEntity updatedBook);
        Task<bool> DeleteBookByUid(string uid);

        Task<BookEntity> IssueBook(string userId, string bookId);
    }
}
