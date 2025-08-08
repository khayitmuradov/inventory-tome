using inventory_tome.Core.Models;

namespace inventory_tome.Core.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Book? GetById(int id);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> FindByTitle(string title);
        void Add(Book book);
        void Update(Book book);
    }
}
