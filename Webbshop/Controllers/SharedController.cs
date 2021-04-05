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

        public static void ListCategories(List<BookCategory> categories)
        {
            if (categories.Count > 0)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {categories[i]}");
                }
            }
            else
            {
                Console.WriteLine("Inga kategorier funna. Lägg till en kategori först.");
                Thread.Sleep(2500);
            }
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

        internal static void BuyBook(User user)
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
                        var book = BookController.SearchForBook();
                        BookController.ShowInfoAboutBook(user, book);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        ChooseCategoryToView();
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

        private static void ChooseCategoryToView()
        {
            var categories = api.GetCategories();
            Console.Clear();
            ListCategories(categories);
            var input = SharedController.GetAndValidateInput();

            if (input.validatedInput > 0
               && input.validatedInput <= categories.Count)
            {
                var books = api.GetBooksInCategory(categories[input.validatedInput - 1].Id);
                BookView.ListAllBooks(books);
                Console.ReadKey();
            }
        }
    }
}
