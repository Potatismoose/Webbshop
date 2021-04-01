using System;
using System.Collections.Generic;

namespace Webbshop.Views
{
    static class SharedView
    {

        static List<string> buyMenuOptions = new List<string>
        {
        "Bokmeny",
        "Köp en bok",
        "Gå tillbaka"

        };

        static List<string> buyBookOptions = new List<string>
        {
        "Bokmeny",
        "Lista böcker i kategori",
        "Sök via kategori",
        "Lista böcker från författare",
        "Sök böcker",
        "Gå tillbaka"

        };

        public static List<string> GetBuyMenuOptions()
        {
            return buyMenuOptions;
        }

        public static List<string> GetBookMenuOptions()
        {
            return buyBookOptions;
        }



        public static void PrintWithRedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintWithGreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintWithDarkGreyText(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
