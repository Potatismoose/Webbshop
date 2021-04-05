using System;
using System.Collections.Generic;
using System.Threading;
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

        internal static void ChangedNumberOfBooks(Book book)
        {
            Console.WriteLine($"\tAntalet böcker är nu {book.Amount}");
            Thread.Sleep(2500);
        }
    }
}