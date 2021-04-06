using System;
using System.Collections.Generic;
using System.Threading;
using webshopAPI.Models;

namespace Webbshop.Views
{
    static class SharedView
    {
        public static void PrintWithDarkGreyText(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintWithGreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintWithRedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        internal static void BuyBook()
        {
            SharedView.PrintWithDarkGreyText("Köp en bok");
            Console.WriteLine("\tLeta reda på boken du vill köpa på följande sätt");
            Console.WriteLine("\t1. Sök efter bok (nyckelord)");
            Console.WriteLine("\t2. Sök efter författare");
            Console.WriteLine("\t3. Sök efter kategori");
            Console.WriteLine("\t4. Lista samtliga kategorier");
            Console.WriteLine("\tX. Backa ett steg");
        }

        public static void ListCategories(List<BookCategory> categories)
        {
            if (categories.Count > 0)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {categories[i]}");
                }
            }
            else
            {
                Console.WriteLine("Inga kategorier funna. Lägg till en kategori först.");
                Thread.Sleep(2500);
            }
        }
    }
}
