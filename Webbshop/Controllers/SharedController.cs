using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshop.Utils;
using Webbshop.Views;
using webshopAPI;

namespace Webbshop.Controllers
{
    class SharedController
    {
        public static bool LogoutIf_X_IsPressed(User user, string menuInput, int validatedInput)
        {
            
            var logoutUser = false;
            if (validatedInput == 0 && menuInput.ToLower() == "x")
            {
                WebShopApi api = new WebShopApi();
                api.Logout(user.Id);
                logoutUser = true;
                Console.Clear();
            }

            return logoutUser;
        }

        public static bool GoBackIf_X_IsPressedOrPrintErrorMsg(string menuInput)
        {
            var continueLoop = true;
            if (menuInput.ToLower() == "x")
            {
                continueLoop = false;
            }
            else
            {
                SharedError.PrintWrongMenuInput();
            }

            return continueLoop;
        }

        public static string GetSearchInput()
        {
            return Console.ReadLine();
        }

        public static (string menuInput, int validatedInput) GetAndValidateInput()
        {
            var menuInput = InputHelper.AskForMenuInput();
            var validatedInput = InputHelper.ValidateMenuInput(menuInput);
            return (menuInput, validatedInput);
        }
    }
}
