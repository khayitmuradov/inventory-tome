using inventory_tome.Core.Services.Interfaces;

namespace inventory_tome.Menus
{
    public static class BorrowMenu
    {
        public static void Show(ILibraryService libraryService)
        {
            while (true)
            {
                Console.WriteLine("\n=== Borrow Menu ===");
                Console.WriteLine("1. Borrow a Book");
                Console.WriteLine("2. Return a Book");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Choice: ");
                var input = Console.ReadLine();

                if (input == "1")
                {
                    Console.Write("Enter Book ID: ");
                    int.TryParse(Console.ReadLine(), out int bookId);
                    Console.Write("Enter Member ID: ");
                    int.TryParse(Console.ReadLine(), out int memberId);

                    if (libraryService.BorrowBook(bookId, memberId, out string? error))
                        Console.WriteLine("Book borrowed successfully.");
                    else
                        Console.WriteLine($"Error: {error}");
                }
                else if (input == "2")
                {
                    Console.Write("Enter Book ID to return: ");
                    int.TryParse(Console.ReadLine(), out int bookId);
                    var returnDate = DateTime.Now;

                    if (libraryService.ReturnBook(bookId, returnDate, out decimal fine, out string? error))
                    {
                        if (fine > 0)
                            Console.WriteLine($"Book returned. Overdue fine: ${fine}");
                        else
                            Console.WriteLine("Book returned on time. No fine.");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {error}");
                    }
                }
                else if (input == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }
    }
}
