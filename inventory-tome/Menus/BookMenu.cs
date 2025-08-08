using inventory_tome.Core.Services.Interfaces;

namespace inventory_tome.Menus
{
    public static class BookMenu
    {
        public static void Show(ILibraryService libraryService)
        {
            while (true)
            {
                Console.WriteLine("\n=== Book Menu ===");
                Console.WriteLine("1. Add a Book");
                Console.WriteLine("2. Display All Books");
                Console.WriteLine("3. Find Books by Title");
                Console.WriteLine("4. Get Book by ID");
                Console.WriteLine("5. Update Book");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Choice: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddBook(libraryService);
                        break;

                    case "2":
                        DisplayAllBooks(libraryService);
                        break;

                    case "3":
                        FindBooks(libraryService);
                        break;

                    case "4":
                        GetBookById(libraryService);
                        break;

                    case "5":
                        UpdateBook(libraryService);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private static void AddBook(ILibraryService service)
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Author: ");
            var author = Console.ReadLine();
            service.AddBook(title, author);
            Console.WriteLine("Book added.");
        }

        private static void DisplayAllBooks(ILibraryService service)
        {
            var books = service.GetAllBooks();
            foreach (var b in books)
                Console.WriteLine($"{b.Id}: {b.Title} by {b.Author} | Available: {b.Status}");
        }

        private static void FindBooks(ILibraryService service)
        {
            Console.Write("Enter part of the book title: ");
            var title = Console.ReadLine();
            var results = service.FindBooks(title);
            if (!results.Any())
                Console.WriteLine("No books found.");
            else
            {
                foreach (var b in results)
                    Console.WriteLine($"{b.Id}: {b.Title} by {b.Author} | Available: {b.Status}");
            }
        }

        private static void GetBookById(ILibraryService service)
        {
            Console.Write("Enter book ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var book = service.GetBookById(id);
                if (book == null)
                    Console.WriteLine("Book not found.");
                else
                    Console.WriteLine($"{book.Id}: {book.Title} by {book.Author} | Available: {book.Status}");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private static void UpdateBook(ILibraryService service)
        {
            Console.Write("Enter book ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var book = service.GetBookById(id);
                if (book == null)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }

                Console.Write("New title (leave blank to keep unchanged): ");
                var newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle))
                    book.Title = newTitle;

                Console.Write("New author (leave blank to keep unchanged): ");
                var newAuthor = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newAuthor))
                    book.Author = newAuthor;

                Console.Write("Is available? (yes/no/blank = keep): ");
                var avail = Console.ReadLine()?.Trim().ToLower();
                if (avail == "yes")
                    book.Status = true;
                else if (avail == "no")
                    book.Status = false;

                service.UpdateBook(book);
                Console.WriteLine("Book updated.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }
    }
}
