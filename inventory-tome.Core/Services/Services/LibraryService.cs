using inventory_tome.Core.Models;
using inventory_tome.Core.Repositories.Interfaces;
using inventory_tome.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool BorrowBook(int bookId, int memberId, out string errorMessage)
        {
            errorMessage = string.Empty;

            var book = _books.GetById(bookId);
            if (book == null)
            {
                errorMessage = "Book not found";
                return false;
            }

            if (!book.Status)
            {
                errorMessage = "Book is already borrowed";
            }

            var member = _members.GetById(memberId);
            if (member == null) 
            {
                errorMessage = "Member not found";
                return false;
            }

            book.Status = false;
            _books.Update(book);

            var borrowRecord = new BorrowRecord
            {
                BookId = bookId,
                MemberId = memberId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14),
                ReturnDate = null
            };
            _borrows.Add(borrowRecord);

            return true;
        }
    }
}
