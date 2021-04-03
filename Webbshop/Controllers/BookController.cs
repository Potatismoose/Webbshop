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
            BookView.SearchForBook();
            var searchKeyword = SharedController.GetSearchInput();
            var listWithMatchingBooks = api.GetBooks(searchKeyword);
            if (listWithMatchingBooks.Count > 0)
            {
                var continueLoop = true;
                do
                {
                    BookView.ListAllBooks(listWithMatchingBooks);
                    var input = SharedController.GetAndValidateInput();
                    if (input.validatedInput == 0 || input.validatedInput > listWithMatchingBooks.Count)
                    {
                        SharedError.PrintWrongMenuInput();
                    }
                    else
                    {
                        //TODO: Fixa den här delen.
                        Console.ReadKey();
                        continueLoop = false;
                    }

                } while (continueLoop);
            }
            else
            {
                SharedError.NothingFound();
            }
        }


        public static void BookOptions(Book book)
        {
            var continueCode = true;
            do
            {
                BookView.BookOptions(book);
                var input = SharedController.GetAndValidateInput();
                switch (input.validatedInput)
                {

                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 0:
                        if (input.menuInput.ToLower() == "x")
                        {
                            continueCode = false;
                            break;
                        }
                        SharedError.PrintWrongMenuInput();
                        break;
                    default:
                        SharedError.PrintWrongMenuInput();
                        break;
                }
            } while (continueCode);
            
            
        }
    }
}
