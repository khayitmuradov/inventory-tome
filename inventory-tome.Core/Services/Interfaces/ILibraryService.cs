using inventory_tome.Core.Models;

namespace inventory_tome.Core.Services.Interfaces
{
    public interface ILibraryService
    {
        void AddBook(string title, string author);
        IEnumerable<Book> FindBooks(string title);
        Book? GetBookById(int id);
        IEnumerable<Book> GetAllBooks();
        void UpdateBook(Book book);

        void RegisterMember(string firstName, string lastName);
        Member? GetMemberById(int id);
        IEnumerable<Member> GetAllMembers();

        bool BorrowBook(int bookId, int memberId, out string errorMessage);
        bool ReturnBook(int bookId, DateTime returnDate, out decimal fine, out string errorMessage);
    }
}
