using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshop.Views;
using webshopAPI;
using webshopAPI.Models;

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
                    SharedError.PrintWrongInput();
                    return null;
                }
            }
            else
            {
                SharedError.NothingFound();
                return null;
            }
        }

        internal static void BuyBook(User user, Book book)
        {
            if (api.BuyBook(user.Id, book.Id))
            {
                SharedError.Success();
            }
            else
            {
                SharedError.Failed();
            }
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
                        BuyBook(user, book);
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

        internal static void BuyByChooseByCategory(User user)
        {
            var categories = FindAndListCategories();
            var chosenCategory = ChooseCategoryToView(categories);
            var books = api.GetBooksInCategory(chosenCategory.Id);
            var book = ListAndChooseBook(books);
            BuyBook(user, book);
            
        }

        private static List<BookCategory> FindAndListCategories()
        {
            var categories = api.GetCategories();
            Console.Clear();
            SharedView.ListCategories(categories);
            return categories;
        }

        private static BookCategory ChooseCategoryToView(List<BookCategory> categories)
        {
            var continueLoop = true;
            Tuple<string, int> input;
            do
            {
                input = SharedController.GetAndValidateInput().ToTuple();

                if (input.Item2 > 0
                   && input.Item2 <= categories.Count)
                {
                    continueLoop = false;
                }
                else
                {
                    SharedError.PrintWrongInput();
                    continueLoop = true;
                }
            } while (continueLoop);

            return categories[input.Item2 - 1];
        }

        internal static void BuyBySearchByAuthor(User user)
        {
            var tuple = BookController.SearchBooksFromAuthor();
            if (tuple.message == "Avbrutet")
            {
            }
            else
            {
                var book = BookController.ListAndChooseBook(tuple.listWithBooks);
                if (book != null)
                {
                    BookController.ShowInfoAboutBook(user, book);
                }
            }
        }

        internal static void BuyBySearchByBook(User user)
        {
                var book = BookController.SearchForBook();
                if (book != null)
                {
                    BookController.ShowInfoAboutBook(user, book);
                }
            
        }

        internal static (string message, List<Book> listWithBooks) SearchBooksFromAuthor()
        {
            List<Book> listOfBooksFromAuthor = new List<Book>();
            WebShopApi api = new WebShopApi();
            Console.Clear();
            BookView.SearchBooksFromAuthor();
            var searchKeyword = SharedController.GetSearchInput();
            if (searchKeyword.ToLower() == "x")
            {
                return ("Avbrutet", listOfBooksFromAuthor);
            }
            listOfBooksFromAuthor = api.GetBooksByAuthor(searchKeyword);
            return ("Sökresultat",listOfBooksFromAuthor);
        }

        internal static Book ListAndChooseBook(List<Book> listWithMatchingBooks)
        {
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
                    SharedError.PrintWrongInput();
                    return null;
                }
            }
            else
            {
                SharedError.NothingFound();
                return null;
            }
        }
    }
}
