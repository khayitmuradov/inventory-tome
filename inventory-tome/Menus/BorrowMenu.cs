using inventory_tome.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_tome.Menus
{
    public static class BorrowMenu
    {
        public static void Show(ILibraryService libraryService)
        {
            while (true)
            {
                System.Console.WriteLine("\n=== Borrow Menu ===");
                System.Console.WriteLine("1. Borrow a Book");
                System.Console.WriteLine("2. Return a Book");
                System.Console.WriteLine("0. Back to Main Menu");
                System.Console.Write("Choice: ");
                var input = System.Console.ReadLine();

                if (input == "1")
                {
                    System.Console.Write("Enter Book ID: ");
                    int.TryParse(System.Console.ReadLine(), out int bookId);
                    System.Console.Write("Enter Member ID: ");
                    int.TryParse(System.Console.ReadLine(), out int memberId);

                    if (libraryService.BorrowBook(bookId, memberId, out string? error))
                        System.Console.WriteLine("Book borrowed successfully.");
                    else
                        System.Console.WriteLine($"Error: {error}");
                }
                else if (input == "2")
                {
                    System.Console.Write("Enter Book ID to return: ");
                    int.TryParse(System.Console.ReadLine(), out int bookId);
                    var returnDate = DateTime.Now;

                    if (libraryService.ReturnBook(bookId, returnDate, out decimal fine, out string? error))
                    {
                        if (fine > 0)
                            System.Console.WriteLine($"Book returned. Overdue fine: ${fine}");
                        else
                            System.Console.WriteLine("Book returned on time. No fine.");
                    }
                    else
                    {
                        System.Console.WriteLine($"Error: {error}");
                    }
                }
                else if (input == "0")
                {
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid option.");
                }
            }
        }
    }
}
