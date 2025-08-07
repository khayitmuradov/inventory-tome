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
        //private readonly IBorrowRecordRepository _borrows;

        //,
        //    IBorrowRecordRepository borrows
        public LibraryService(
            IBookRepository books,
            IMemberRepository members)
        {
            _books = books;
            _members = members;
            _members = members;
            //_borrows = borrows;
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
    }
}
