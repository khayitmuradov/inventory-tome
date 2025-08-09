using inventory_tome.Core.Models;
using inventory_tome.Core.Repositories.Interfaces;
using inventory_tome.Core.Services.Interfaces;

namespace inventory_tome.Core.Services.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _books;
        private readonly IMemberRepository _members;
        private readonly IBorrowRecordRepository _borrows;

        public LibraryService(
            IBookRepository books,
            IMemberRepository members,
            IBorrowRecordRepository borrows)
        {
            _books = books;
            _members = members;
            _borrows = borrows;
        }

        public void AddBook(string title, string author)
        {
            var book = new Book
            {
                Title = title,
                Author = author,
                Status = true
            };
            _books.Add(book);
        }
        public IEnumerable<Book> FindBooks(string title)
        {
            return _books.FindByTitle(title);
        }
        public Book? GetBookById(int id)
        {
            return _books.GetById(id);
        }
        public IEnumerable<Book> GetAllBooks() => _books.GetAll();
        public void UpdateBook(Book book) => _books.Update(book);


        public void RegisterMember(string firstName, string lastName)
        {
            var member = new Member
            {
                FirstName = firstName,
                LastName = lastName
            };

            _members.Add(member);
        }
        public Member? GetMemberById(int id)
        {
            return _members.GetById(id);
        }
        public IEnumerable<Member> GetAllMembers()
        {
            return _members.GetAll();
        }


        public bool BorrowBook(int bookId, int memberId, out string errorMessage)
        {
            errorMessage = string.Empty;

            var book = _books.GetById(bookId);
            if (book == null)
            {
                errorMessage = "Book not found.";
                return false;
            }

            if (!book.Status)
            {
                errorMessage = "Book is already borrowed.";
                return false;
            }

            var member = _members.GetById(memberId);
            if (member == null)
            {
                errorMessage = "Member not found.";
                return false;
            }

            book.Status = false;
            _books.Update(book);

            var borrow = new BorrowRecord
            {
                BookId = bookId,
                MemberId = memberId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                ReturnDate = null
            };

            _borrows.Add(borrow);

            return true;
        }

        public bool ReturnBook(int bookId, DateTime returnDate, out decimal fine, out string errorMessage)
        {
            errorMessage = string.Empty;
            fine = 0;

            var book = _books.GetById(bookId);
            if (book == null)
            {
                errorMessage = "Book not found.";
                return false;
            }

            var activeRecord = _borrows.GetActiveByBookId(bookId);
            if (activeRecord == null)
            {
                errorMessage = "This book is not currently borrowed.";
                return false;
            }

            activeRecord.ReturnDate = returnDate;
            _borrows.Update(activeRecord);

            book.Status = true;
            _books.Update(book);

            if (returnDate > activeRecord.DueDate)
            {
                fine = (decimal)(returnDate - activeRecord.DueDate).TotalDays;
            }

            return true;
        }

    }
}
