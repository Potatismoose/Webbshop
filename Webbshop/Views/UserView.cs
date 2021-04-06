using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshop.Utils;
using webshopAPI;

namespace Webbshop.Views
{
    class UserView
    {
        
        public static void PrintBuyMenuOptions()
        {
            Console.Clear();
            SharedView.PrintWithGreenText($"\tVälkommen till bokshoppen");
            Console.WriteLine("\t1. Köp böcker");
            Console.WriteLine("\tX. Logga ut");
        }

        public static (string username, string password, string verifyPassword) Register()
        {
            Console.Clear();
            SharedView.PrintWithDarkGreyText("Registrera ett konto");
            Console.Write("\tAnge användarnamn> ");
            var username = Console.ReadLine();
            Console.Write("\tAnge lösenord> ");
            var password = Console.ReadLine();
            Console.Write("\tVerifiera lösenord> ");
            var verifiedPassword = Console.ReadLine();

            return (username,password,verifiedPassword);
        }
    }
}
