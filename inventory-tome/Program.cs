using inventory_tome.Core.Repositories.Interfaces;
using inventory_tome.Core.Repositories.Repositories;
using inventory_tome.Core.Services.Services;
using inventory_tome.Menus;

namespace inventory_tome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=inventory_tome_db;Uid=root;Pwd=0880;";

            var bookRepo = new BookRepository(connectionString);
            var memberRepo = new MemberRepository(connectionString);
            var borrowRecordRepo = new BorrowRecordRepository(connectionString);
            var libraryService = new LibraryService(bookRepo, memberRepo, borrowRecordRepo);

            while (true)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine("1. Book Menu");
                Console.WriteLine("2. Member Menu");
                Console.WriteLine("3. Borrow Menu");
                Console.WriteLine("0. Exit");
                Console.Write("Choice: ");
                var input = Console.ReadLine();

                if (input == "1")
                    BookMenu.Show(libraryService);
                else if (input == "2")
                    MemberMenu.Show(libraryService);
                else if (input == "3")
                    BorrowMenu.Show(libraryService);
                else if (input == "0")
                    break;
                else
                    Console.WriteLine("Invalid option.");
            }
        }
    }
}
