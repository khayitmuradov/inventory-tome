using inventory_tome.Core.Services.Interfaces;

namespace inventory_tome.Menus
{
    public static class MemberMenu
    {
        public static void Show(ILibraryService libraryService)
        {
            while (true)
            {
                Console.WriteLine("\n=== Member Menu ===");
                Console.WriteLine("1. Register Member");
                Console.WriteLine("2. View All Members");
                Console.WriteLine("3. Get Member by ID");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Choice: ");
                var input = Console.ReadLine();

                if (input == "1")
                {
                    Console.Write("First name: ");
                    var firstName = System.Console.ReadLine();
                    Console.Write("Last name: ");
                    var lastName = System.Console.ReadLine();
                    libraryService.RegisterMember(firstName, lastName);
                    Console.WriteLine("Member registered.");
                }
                else if (input == "2")
                {
                    var members = libraryService.GetAllMembers();
                    foreach (var m in members)
                        Console.WriteLine($"{m.Id}: {m.FirstName} {m.LastName}");
                }
                else if (input == "3")
                {
                    Console.Write("Member ID: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var member = libraryService.GetMemberById(id);
                        if (member != null)
                            Console.WriteLine($"{member.Id}: {member.FirstName} {member.LastName}");
                        else
                            Console.WriteLine("Member not found.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID.");
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
