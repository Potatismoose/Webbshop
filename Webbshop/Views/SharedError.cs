using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Webbshop.Utils;
using webshopAPI;

namespace Webbshop.Views
{
    static class SharedError
    {
        public static void Failed()
        {
            SharedView.PrintWithRedText("\tMisslyckades.");
            GeneralViewHelper.WaitAndClearScreen();
        }

        public static void PrintWrongCredentials(User user)
        {
            if (user == null)
            {
                SharedView.PrintWithRedText("\tFelaktigt användarnamn eller lösenord.");
                GeneralViewHelper.WaitAndClearScreen();
            }
        }

        public static void PrintWrongMenuInput()
        {
            SharedView.PrintWithRedText("\tFelaktigt menyval, försök igen.");
            GeneralViewHelper.WaitAndClearScreen();
        } 
        public static void Success()
        {
            SharedView.PrintWithGreenText("\tLyckades!");
            GeneralViewHelper.WaitAndClearScreen();
        }

        internal static void EmptyInput()
        {
            SharedView.PrintWithRedText("\tTom inmatning, vänligen ange något.");
            Thread.Sleep(2000);
        }

        internal static void NothingFound()
        {
            SharedView.PrintWithRedText("\tInget hittades");
            Thread.Sleep(2000);
        }
        internal static void PrintWrongInput()
        {
            SharedView.PrintWithRedText("\tFelaktig inmatning");
            Thread.Sleep(2000);
        }

        internal static void BooksStillInCategory(int books)
        {
            SharedView.PrintWithRedText($"\tKan ej ta bort kategori. {books} böcker finns kvar i kategorin");
            Console.WriteLine("\tTryck enter för att fortsätta");
            Console.ReadKey();
        }

        internal static void UnChanged()
        {
            SharedView.PrintWithRedText("\tIngen ändring har gjorts.");
            Thread.Sleep(2000);
        }
    }
}
