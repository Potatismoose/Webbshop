using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshop.Views
{
    static class StartView
    {
        static readonly List<string> menuOptions = new List<string> { "Logga in", "Registrera", "Avsluta" };
        static int choice = 1;

        public static void Print()
        {
            Console.WriteLine("Bookhandlar´n\n");
            foreach (var menuOption in menuOptions)
            {
                if (menuOption != "Avsluta")
                {
                    Console.WriteLine($"\t{choice}. {menuOption}");
                    choice++;
                    continue;
                }
                Console.WriteLine($"\tQ. {menuOption}");
                choice++;
            }
            choice = 1;
        }
    }
}
