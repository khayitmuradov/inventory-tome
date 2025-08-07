using inventory_tome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
