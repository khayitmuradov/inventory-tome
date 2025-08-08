using inventory_tome.Core.Models;
using inventory_tome.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_tome.Menus
{
    public static class MemberMenu
    {
        public static void Show(ILibraryService libraryService)
        {
            while (true)
            {
                System.Console.WriteLine("\n=== Member Menu ===");
                System.Console.WriteLine("1. Register Member");
                System.Console.WriteLine("2. View All Members");
                System.Console.WriteLine("3. Get Member by ID");
                System.Console.WriteLine("0. Back to Main Menu");
                System.Console.Write("Choice: ");
                var input = System.Console.ReadLine();

                if (input == "1")
                {
                    System.Console.Write("First name: ");
                    var firstName = System.Console.ReadLine();
                    System.Console.Write("Last name: ");
                    var lastName = System.Console.ReadLine();
                    libraryService.RegisterMember(firstName, lastName);
                    System.Console.WriteLine("Member registered.");
                }
                else if (input == "2")
                {
                    var members = libraryService.GetAllMembers();
                    foreach (var m in members)
                        System.Console.WriteLine($"{m.Id}: {m.FirstName} {m.LastName}");
                }
                else if (input == "3")
                {
                    System.Console.Write("Member ID: ");
                    if (int.TryParse(System.Console.ReadLine(), out int id))
                    {
                        var member = libraryService.GetMemberById(id);
                        if (member != null)
                            System.Console.WriteLine($"{member.Id}: {member.FirstName} {member.LastName}");
                        else
                            System.Console.WriteLine("Member not found.");
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid ID.");
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
