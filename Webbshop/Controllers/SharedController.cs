using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Webbshop.Utils;
using Webbshop.Views;
using webshopAPI;
using webshopAPI.Models;

namespace Webbshop.Controllers
{
    class SharedController
    {
        private static WebShopApi api = new WebShopApi();
        public static (string menuInput, int validatedInput) GetAndValidateInput()
        {
            var menuInput = InputHelper.AskForMenuInput();
            var validatedInput = InputHelper.ValidateMenuInput(menuInput);
            return (menuInput, validatedInput);
        }

        public static string GetSearchInput()
        {
            return Console.ReadLine();
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

        internal static void AskForMenuInput()
        {
            throw new NotImplementedException();
        }

        internal static bool CheckIfNullOrEmptyOrWhiteSpace(string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
        }

        internal static void BuyBookMenu(User user)
        {
            var continueLoop = true;
            do
            {
                Console.Clear();
                SharedView.BuyBook();
                var input = GetAndValidateInput();
                switch (input.validatedInput)
                {
                    case 1:
                        BookController.BuyBySearchByBook(user);
                        break;
                    case 2:
                        BookController.BuyBySearchByAuthor(user);
                        break;
                    case 3:
                        break;
                    case 4:
                        BookController.BuyByChooseByCategory(user);


                        break;
                    case 0:
                        if (input.menuInput.ToLower() == "x")
                        {
                            continueLoop = false;
                        }
                        else
                        {
                            SharedError.PrintWrongMenuInput();
                        }
                        break;
                }

            } while (continueLoop);
        }

        
    }
}
