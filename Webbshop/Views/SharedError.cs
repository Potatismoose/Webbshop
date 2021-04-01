using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshopAPI;

namespace Webbshop.Views
{
    static class SharedError
    {
        public static void PrintWrongMenuInput()
        {
            SharedView.PrintWithRedText("\tFelaktigt menyval, försök igen.");
        }

        public static void PrintWrongCredentials(User user)
        {
            if (user == null)
            {
                SharedView.PrintWithRedText("\tFelaktigt användarnamn eller lösenord.");
            }
        }

        public static void Failed()
        {
            SharedView.PrintWithRedText("\tMisslyckades.");
        }

        public static void Success()
        {
            SharedView.PrintWithGreenText("\tLyckades!");
        }
    }
}
