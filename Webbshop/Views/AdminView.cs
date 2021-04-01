using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshop.Views
{
    static class AdminView
    {
        private static int MenuChoiceCounter { get; set; } = 1;
        static List<List<string>> MenuOptions = new List<List<string>>
        {
            new List<string>
            {
            "Användarmeny - Admin",
            "Lägg till en användare",
            "Sök användare",
            "Lista alla användare",
            "Tilldela Adminrättigheter till en användare",
            "Degradera en användare till standardkonto",
            "Aktivera en användare",
            "Inaktivera en användare",

            },

            new List<string>
            {
                "Statistik - Admin",
                "Bästa kunden",
                "Visa intjänade kronor",
                "Lista sålda böcker"
            },

            new List<string>
            {
                "Bokmeny - Admin",
                "Lägg till en bok",
                "Lägg till kategori till en bok",
                "Uppdatera bokinformation",
                "Ta bort en bok",
                "Uppdatera bokinformation",
            }
        };


        public static int Print()
        {
            Console.Clear();
            SharedView.PrintWithDarkGreyText("Administratoralternativ");
            Console.WriteLine("\t1. Adminfunktioner");
            Console.WriteLine("\t2. Handla böcker");
            Console.WriteLine("\t3. Gå tillbaka till huvudmenyn");

            return MenuChoiceCounter;
        }

        public static int PrintAdminFunctions()
        {
            Console.Clear();
            SharedView.PrintWithDarkGreyText("Administratorfunktioner");
            var userMenu = SharedView.GetBuyMenuOptions();

            return MenuChoiceCounter;
        }

        public static int PrintAdminMenu()
        {
            return 0;
        }
    }
}
