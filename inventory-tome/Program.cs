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

            while (true)
            {
                Console.WriteLine("\n=== Library Menu ===");
                Console.WriteLine("1. Add a Book");
                Console.WriteLine("2. Display All Books");
                Console.WriteLine("3. Find Books by Title");
                Console.WriteLine("4. Get Book by ID");
                Console.WriteLine("5. Update Book");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();

                    Console.Write("Enter author: ");
                    string author = Console.ReadLine();

                    libraryService.AddBook(title, author);
                    Console.WriteLine($"Book \"{title}\" by {author} added!");
                    break;
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\nCurrent books in library:");
                    var books = libraryService.GetAllBooks(); // Use only LibraryService!
                    foreach (var book in books)
                    {
                        Console.WriteLine($"{book.Id}: {book.Title} by {book.Author} | Available: {book.Status}");
                    }
                    break;
                }
                else if (choice == "3")
                {
                    Console.Write("Enter part of the book title to search: ");
                    string searchTitle = Console.ReadLine();
                    var foundBooks = libraryService.FindBooks(searchTitle);
                    if (!foundBooks.Any())
                    {
                        Console.WriteLine("No books found.");
                    }
                    else
                    {
                        foreach (var book in foundBooks)
                        {
                            Console.WriteLine($"{book.Id}: {book.Title} by {book.Author} | Available: {book.Status}");
                        }
                    }

                }
                else if (choice == "4")
                {
                    Console.Write("Enter book ID: ");
                    if (int.TryParse(Console.ReadLine(), out int bookId))
                    {
                        var book = libraryService.GetBookById(bookId);
                        if (book == null)
                        {
                            Console.WriteLine("Book not found.");
                        }
                        else
                        {
                            Console.WriteLine($"{book.Id}: {book.Title} by {book.Author} | Available: {book.Status}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID.");
                    }

                }
                else if (choice == "5")
                {
                    Console.Write("Enter book ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int updateBookId))
                    {
                        var bookToUpdate = libraryService.GetBookById(updateBookId);
                        if (bookToUpdate == null)
                        {
                            Console.WriteLine("Book not found.");
                        }
                        else
                        {
                            Console.Write("Enter new title (leave blank to keep unchanged): ");
                            string newTitle = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newTitle)) bookToUpdate.Title = newTitle;

                            Console.Write("Enter new author (leave blank to keep unchanged): ");
                            string newAuthor = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newAuthor)) bookToUpdate.Author = newAuthor;

                            Console.Write("Is the book available? (yes/no, leave blank to keep unchanged): ");
                            string isAvailableInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(isAvailableInput))
                                bookToUpdate.Status = isAvailableInput.Trim().ToLower() == "yes";

                            libraryService.UpdateBook(bookToUpdate);
                            Console.WriteLine("Book updated successfully.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID.");
                    }

                }
                else if (choice == "0")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }
        }
    }
}
