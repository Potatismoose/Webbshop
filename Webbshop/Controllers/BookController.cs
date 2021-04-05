using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshop.Views;
using webshopAPI;

namespace Webbshop.Controllers
{
    class BookController
    {
        private static WebShopApi api = new WebShopApi();
        public static Book SearchForBook()
        {
            WebShopApi api = new WebShopApi();
            Console.Clear();
            BookView.SearchForBook();
            var searchKeyword = SharedController.GetSearchInput();
            if (searchKeyword.ToLower() == "x")
            {
                return null;
            }
            var listWithMatchingBooks = api.GetBooks(searchKeyword);
            
            if (listWithMatchingBooks.Count > 0)
            {
                Console.Clear();
                BookView.ListAllBooks(listWithMatchingBooks);
                var input = SharedController.GetAndValidateInput();
                if (input.validatedInput != 0
                    && input.validatedInput <= listWithMatchingBooks.Count)
                {
                    return api.GetBook(listWithMatchingBooks[input.validatedInput - 1].Id);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                SharedError.NothingFound();
                return null;
            }
            
            

        }

        internal static void BuyBook()
        {
            throw new NotImplementedException();
        }

        internal static void ShowInfoAboutBook(User user, Book book)
        {
            var continueLoop = true;
            do
            {
                BookView.ShowInfoAboutBook(book);
                var input = SharedController.GetSearchInput();
                switch (input.ToLower())
                {
                    case "j":
                        if (api.BuyBook(user.Id, book.Id))
                        {
                            SharedError.Success();
                        }
                        else
                        {
                            SharedError.Failed();
                        }
                        continueLoop = false;
                        break;
                    case "n":
                        continueLoop = false;
                        break;
                    default:
                        SharedError.PrintWrongInput();
                        continueLoop = true;
                        break;
                }

            } while (continueLoop);
        }
    }
}
