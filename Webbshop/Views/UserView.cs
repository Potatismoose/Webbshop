using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshop.Utils;

namespace Webbshop.Views
{
    class UserView
    {
        
        public static void PrintBuyMenuOptions(List<string> buyMenuOptions)
        {
            Console.Clear();
            int menuChoiceCounter = 1;
            SharedView.PrintWithGreenText($"\tVälkommen till bokshoppen");
            for (int i = 0; i < buyMenuOptions.Count; i++)
            {
                if (i == 0)
                {
                    SharedView.PrintWithDarkGreyText(buyMenuOptions[i]);
                    continue;
                }
                Console.WriteLine($"\t{menuChoiceCounter++}. {buyMenuOptions[i]}");
            }            
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
