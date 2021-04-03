using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshop.Controllers;
using webshopAPI;

namespace Webbshop.Views
{
    static class AdminView
    {
        private static int MenuChoiceCounter { get; set; } = 1;
       
        public static int Print()
        {
            ResetMenuCounter();
            Console.Clear();
            SharedView.PrintWithDarkGreyText("Administratoralternativ");
            Console.WriteLine("\t1. Adminfunktioner");
            Console.WriteLine("\t2. Handla böcker");
            Console.WriteLine("\tX. Logga ut");

            return MenuChoiceCounter;
        }

        private static void ResetMenuCounter()
        {
            MenuChoiceCounter = 1;
        }

        public static void PrintAdminFunctions()
        {
            
            Console.Clear();
            SharedView.PrintWithDarkGreyText("Administratorfunktioner\n");
            Console.WriteLine();
            SharedView.PrintWithDarkGreyText($"Användaralternativ");
            Console.WriteLine($"\t1. Lägg till användare");
            Console.WriteLine($"\t2. Lista alla användare");
            Console.WriteLine($"\t3. Sök en användare");

            SharedView.PrintWithDarkGreyText($"Bokmeny - Admin");
            Console.WriteLine($"\t4. Lägg till en bok");
            Console.WriteLine($"\t5. Ta bort en bok");
            Console.WriteLine($"\t6. Sök efter en bok");
            Console.WriteLine();
            Console.WriteLine($"\tX. Backa ett steg");

            /*
                 ,
                 "Lägg till kategori till en bok",
                 "Uppdatera bokinformation",
                 "Uppdatera bokinformation",

                 "Statistik - Admin",
                 "Bästa kunden",
                 "Visa intjänade kronor",
                 "Lista sålda böcker"
            */
        }

        internal static (string userName, string password) AddUser()
        {
            Console.Clear();
            SharedView.PrintWithDarkGreyText("Lägg till en användare");
            Console.Write("\tAnge användarnamn >");
            var userName = Console.ReadLine();
            Console.Write("\tAnge lösenord >");
            var password = Console.ReadLine();
            return (userName, password);
        }

        public static int PrintAdminMenu()
        {
            ResetMenuCounter();
            return 0;
        }

        internal static void ListAllUsers(List<User> listWithUsers)
        {
            SharedView.PrintWithDarkGreyText("Lista med alla användare");
            
                for (int i = 0; i < listWithUsers.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {listWithUsers[i]}");
                }
        }

        internal static void SearchForUser(User admin)
        {
            Console.Clear();
            WebShopApi api = new WebShopApi();
            SharedView.PrintWithDarkGreyText(api.Ping(admin.Id));
            SharedView.PrintWithDarkGreyText("Sök efter användare");
            Console.Write("\tSökord >");
            
        }

        internal static void EditUser(User editUser)
        {
            Console.Clear();
            SharedView.PrintWithDarkGreyText($"Edit user {editUser.Name}");
            Console.WriteLine($"\t {editUser.Name} har administratörsrättigheter: {editUser.IsAdmin}");
            Console.WriteLine($"\t {editUser.Name} är aktiv: {editUser.IsActive}");
            Console.WriteLine();
            Console.WriteLine("\t1. Ge användaren administratorrättigheter");
            Console.WriteLine("\t2. Gör användaren till standardanvändare");
            Console.WriteLine("\t3. Aktivera användaren");
            Console.WriteLine("\t4. Inaktivera användaren");
            Console.WriteLine();
            Console.WriteLine("\tX. Backa ett steg");
        }
    }
}
