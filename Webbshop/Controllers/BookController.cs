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

        public static void SearchForBook(User user)
        {
            WebShopApi api = new WebShopApi();
            Console.Clear();
            BookView.SearchForBook();
            var searchKeyword = SharedController.GetSearchInput();
            var listWithMatchingBooks = api.GetBooks(searchKeyword);
            ListBooksAndAskForInput(user, listWithMatchingBooks);
        }

        public static void ListBooksAndAskForInput(User user, List<Book> listWithMatchingBooks)
        {
            if (listWithMatchingBooks.Count > 0)
            {
                var continueLoop = true;
                do
                {
                    Console.Clear();
                    BookView.ListAllBooks(listWithMatchingBooks);
                    var input = SharedController.GetAndValidateInput();
                    if (input.validatedInput == 0 || input.validatedInput > listWithMatchingBooks.Count)
                    {
                        SharedError.PrintWrongMenuInput();
                    }
                    else
                    {
                        if (user.IsAdmin)
                        {
                            AdminController.BookOptions(listWithMatchingBooks[input.validatedInput - 1], user);

                        }
                        else
                        {
                            //TODO Här ska köpmeny för användare kallas på
                        }
                        continueLoop = false;
                    }

                } while (continueLoop);
            }
            else
            {
                SharedError.NothingFound();
            }
        }
    }
}
