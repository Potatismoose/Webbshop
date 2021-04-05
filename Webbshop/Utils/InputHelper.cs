using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshop.Utils
{
    class InputHelper
    {

        public static string AskForMenuInput()
        {
            Console.Write("\n\tChoose a option >");
            return Console.ReadLine();
        }

        public static int ValidateMenuInput(string userInput)
        {
            int number;
            if (int.TryParse(userInput, out number))
            {
                return number;
            }

            return number;
        }

        internal static string InputFreeText()
        {
            return Console.ReadLine();
        }
    }
}
