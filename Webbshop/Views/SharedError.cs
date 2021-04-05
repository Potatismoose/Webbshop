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
            Console.WriteLine("\tFelaktig inmatning");
            Thread.Sleep(2000);
        }
    }
}
