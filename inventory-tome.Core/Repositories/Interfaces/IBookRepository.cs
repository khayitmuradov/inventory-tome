using inventory_tome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
