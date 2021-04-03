using System;
using System.Collections.Generic;
using webshopAPI;

namespace Webbshop.Views

{
    internal class BookView
    {
        internal static void SearchForBook()
        {
            SharedView.PrintWithDarkGreyText("Sök efter en bok");
            Console.Write("\tSök >");
        }

        internal static void ListAllBooks(List<Book> listWithBooks)
        {
            SharedView.PrintWithDarkGreyText("Lista med alla böcker som matchar");
            
                for (int i = 0; i < listWithBooks.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {listWithBooks[i].Title} av författaren {listWithBooks[i].Author}");
                }
        }

        internal static void BookOptions(Book book)
        {
            SharedView.PrintWithDarkGreyText($"Bokalternativ för {book.Title}");
            Console.WriteLine("\t1.Lägg till fler av denna bok");
            Console.WriteLine("\t2. Uppdatera bokinformation");
            Console.WriteLine("\t3. Lägg till kategori till bok");
            Console.WriteLine("\t4. Ta bort bok");
        }
    }
}