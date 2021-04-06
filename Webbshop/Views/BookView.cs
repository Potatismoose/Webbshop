using System;
using System.Collections.Generic;
using System.Threading;
using webshopAPI;

namespace Webbshop.Views

{
    internal class BookView
    {
        internal static void ChangedNumberOfBooks(Book book)
        {
            Console.WriteLine($"\tAntalet böcker är nu {book.Amount}");
            Thread.Sleep(2500);
        }

        internal static void ListAllBooks(List<Book> listWithBooks)
        {
            SharedView.PrintWithDarkGreyText("Lista med alla böcker som matchar");

            for (int i = 0; i < listWithBooks.Count; i++)
            {
                Console.WriteLine($"\t{i + 1}. {listWithBooks[i].Title} av författaren {listWithBooks[i].Author}");
            }
        }

        internal static void SearchForBook()
        {
            SharedView.PrintWithDarkGreyText("Sök efter en bok - X + enter för att avbryta");
            Console.Write("\tSök >");
        }

        internal static void ShowInfoAboutBook(Book book)
        {
            SharedView.PrintWithDarkGreyText("Information om bok");
            Console.WriteLine($"\tBoktitel: {book.Title}");
            Console.WriteLine($"\tFörfattare: {book.Author}");
            Console.WriteLine($"\tKategori: {book.Category.Name}");
            Console.WriteLine($"\tPris: {book.Price}");
            Console.WriteLine($"\tAntal tillgängliga böcker: {book.Amount}");

            Console.WriteLine();
            Console.Write("\tVill du köpa boken? j/n >");
        }

        internal static void SearchBooksFromAuthor()
        {
            SharedView.PrintWithDarkGreyText("Sök efter författare - X + enter för att avbryta");
            Console.Write("\tSök >");
        }
    }
}