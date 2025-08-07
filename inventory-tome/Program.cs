using inventory_tome.Core.Repositories.Interfaces;
using inventory_tome.Core.Repositories.Repositories;
using inventory_tome.Core.Services.Services;

namespace inventory_tome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=inventory_tome_db;Uid=root;Pwd=0880;";

            var bookRepo = new BookRepository(connectionString);

            var libraryService = new LibraryService(bookRepo);

            libraryService.AddBook("The Hobbit", "J.R.R. Tolkien");
            Console.WriteLine("Book added");

            //var found = libraryService.FindBooks("The Hobbit");
            //foreach (var item in found)
            //{
            //    Console.WriteLine($"Book Id: {item.Id} by {item.Author}, Available: {item.Status}");
            //}
        }
    }
}
