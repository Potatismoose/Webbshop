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
    class AdminController
    {
        private static WebShopApi api = new WebShopApi();

        public static void BookOptions(Book book, User admin)
        {
            var continueLoop = true;
            do
            {
                Console.Clear();
                if (book.Amount == 0)
                {
                    continueLoop = false;
                    continue;
                }
                AdminView.UpdateBook(book);
                var input = SharedController.GetAndValidateInput();
                switch (input.validatedInput)
                {
                    case 1:
                        Console.Clear();
                        SetAmount(book, admin);
                        break;
                    case 2:
                        Console.Clear();
                        UpdateBookInfo(book, admin);
                        break;
                    case 3:
                        Console.Clear();
                        ChangeCategory(book, admin);
                        break;
                    case 4:
                        DeleteBook(book, admin);
                        break;
                    case 0:
                        continueLoop = SharedController.GoBackIf_X_IsPressedOrPrintErrorMsg(input.menuInput);
                        break;
                    default:
                        SharedError.PrintWrongMenuInput();
                        break;
                }
            } while (continueLoop);
        }

        public static void PrintAdminSelectionMenu(User admin)
        {
            SharedView.PrintWithGreenText($"\tVälkommen {admin.Name}");
            var logoutUser = false;
            do
            {
                GeneralViewHelper.WaitAndClearScreen();
                AdminView.Print();
                string menuInput = InputHelper.AskForMenuInput();
                int validatedInput = InputHelper.ValidateMenuInput(menuInput);
                logoutUser = SharedController.LogoutIf_X_IsPressed(admin, menuInput, validatedInput);
                if (logoutUser)
                {
                    continue;
                }
                else
                {
                    switch (validatedInput)
                    {
                        case 1:
                            AdminFunctions(admin);
                            break;
                        case 2:
                            SharedController.BuyBookMenu(admin);
                            break;
                    }
                }
            } while (!logoutUser);
        }

        private static void AddBook(User admin)
        {
            var bookInformation = AdminView.AddBook();

            if (api.AddBook(
                admin.Id,
                Convert.ToInt32(bookInformation["Antal"]),
                bookInformation["Titel"],
                bookInformation["Författare"],
                Convert.ToInt32(bookInformation["Pris"])))
            {
                var book = api.GetBooks(bookInformation["Titel"])[0];
                ChangeCategory(book, admin);
            }
            else
            {
                SharedError.Failed();
            }

        }

        private static void AddUser(User admin)
        {
            var userCredentials = AdminView.AddUser();
            if (api.AddUser(admin.Id, userCredentials.userName, userCredentials.password))
            {
                SharedError.Success();
            }
            else
            {
                SharedError.Failed();
            }
        }

        private static void AdminFunctions(User admin)
        {
            var continueLoop = true;
            do
            {
                AdminView.PrintAdminFunctions();
                var input = SharedController.GetAndValidateInput();
                switch (input.validatedInput)
                {
                    case 1:
                        AddUser(admin);
                        break;
                    case 2:
                        ListAllUsers(admin);
                        break;
                    case 3:
                        SearchForUser(admin);
                        break;
                    case 4:
                        AddBook(admin);
                        break;
                    case 5:
                        GetBookAndEnterBookOptions(admin);
                        break;
                    case 6:
                        GetBookAndEnterBookOptions(admin);
                        break;
                    case 7:
                        GetAllBooksAndEnterBookOptions(admin);
                        break;
                    case 8:
                        AddCategory(admin);
                        break;
                    case 9:
                        DeleteCategory(admin);
                        break;
                    case 10:
                        UpdateCategory(admin);
                        break;
                    case 11:
                        SoldBooks(admin);
                        break;
                    case 12:
                        Earnings(admin);
                        break;
                    case 13:
                        BestCustomer(admin);
                        break;
                    case 0:
                        continueLoop = SharedController.GoBackIf_X_IsPressedOrPrintErrorMsg(input.menuInput);
                        break;
                }
            } while (continueLoop);

        }

        private static void BestCustomer(User admin)
        {
            AdminView.BestCustomer(api.BestCustomer(admin.Id));
        }

        private static void Earnings(User admin)
        {
            AdminView.Earnings(api.MoneyEarned(admin.Id));
        }

        private static void SoldBooks(User admin)
        {
            var listWithSoldBooks = api.SoldItems(admin.Id);
            AdminView.SoldBooks(listWithSoldBooks);
        }

        private static void UpdateCategory(User admin)
        {
            var continueLoop = true;
            do
            {
                Console.Clear();
                AdminView.UpdateCategory();
                var categories = api.GetCategories();
                SharedView.ListCategories(categories);
                var input = SharedController.GetAndValidateInput();
                if (input.validatedInput > 0
                    && input.validatedInput <= categories.Count)
                {
                    UpdateCategory(admin, categories[input.validatedInput - 1]);
                }
                else
                {
                    if (input.validatedInput == 0
                        && input.menuInput.ToLower() == "x")
                    {
                        continueLoop = false;
                    }
                    else
                    {
                        SharedError.PrintWrongInput();
                    }
                }

            } while (continueLoop);
        }

        private static void UpdateCategory(User admin, BookCategory bookCategory)
        {
            AdminView.UpdateCategory(bookCategory.Name);
            var input = SharedController.GetSearchInput();

            if (SharedController.CheckIfNullOrEmptyOrWhiteSpace(input))
            {
                SharedError.UnChanged();
            }
            else
            {
                if (api.UpdateCategory(admin.Id, bookCategory.Id, input))
                {
                    SharedError.Success();
                }
                else
                {
                    SharedError.Failed();
                }
            }
        }

        private static void DeleteCategory(User admin)
        {
            var continueLoop = true;
            do
            {
                Console.Clear();
                AdminView.DeleteCategory();
                var categories = api.GetCategories();
                SharedView.ListCategories(categories);
                var input = SharedController.GetAndValidateInput();
                if (input.validatedInput > 0
                    && input.validatedInput <= categories.Count)
                {
                    if (api.DeleteCategory(
                        admin.Id,
                        categories[input.validatedInput - 1].Id))
                    {
                        SharedError.Success();
                    }
                    else
                    {
                        var books = api.GetBooksInCategory(categories[input.validatedInput - 1].Id);
                        SharedError.BooksStillInCategory(books.Count);              
                    }
                }
                else
                {
                    if (input.validatedInput == 0
                        && input.menuInput.ToLower() == "x")
                    {
                        continueLoop = false;
                    }
                    else
                    {
                        SharedError.PrintWrongInput();
                    }
                }

            } while (continueLoop);
        }

        private static void AddCategory(User admin)
        {
            var continueLoop = true;
            do
            {
                Console.Clear();
                AdminView.AddCategory();
                var input = SharedController.GetSearchInput();
                if (SharedController.CheckIfNullOrEmptyOrWhiteSpace(input))
                {
                    SharedError.PrintWrongInput();
                    continueLoop = true;
                }
                else
                {
                    if (api.AddCategory(admin.Id, input))
                    {
                        SharedError.Success();
                    }
                    else 
                    {
                        SharedError.Failed();
                    }
                    continueLoop = false;
                }
            } while (continueLoop);
        }

        private static void ChangeAuthor(User admin, Book book)
        {
            var continueLoop = true;
            do
            {
                AdminView.ChangeAuthor();
                var input = InputHelper.InputFreeText();
                if (string.IsNullOrEmpty(input))
                {
                    SharedError.EmptyInput();
                    continue;
                }
                if (book.Author != input)
                {
                    book.Author = input;
                    api.UpdateBook(admin.Id, book.Id, book.Title, book.Author, book.Price);
                    continueLoop = false;
                }

            } while (continueLoop);
        }

        private static void ChangeCategory(Book book, User admin)
        {
            var categories = api.GetCategories();
            var continueLoop = true;
            do
            {
                Console.Clear();
                AdminView.ChangeCategory(book, categories);
                if (categories.Count() > 0)
                {
                    var input = SharedController.GetAndValidateInput();
                    if (input.validatedInput > 0
                        && input.validatedInput <= categories.Count())
                    {
                        var success = api.AddBookToCategory(
                                        admin.Id,
                                        book.Id,
                                        categories[input.validatedInput - 1].Id);
                        continueLoop = false;
                        if (success)
                        {
                            SharedError.Success();
                        }
                        else
                        {
                            SharedError.Failed();
                        }
                    }
                    else
                    {
                        SharedError.PrintWrongInput();
                    }
                }
            } while (continueLoop);
        }

        private static void ChangePrice(User admin, Book book)
        {
            var continueLoop = true;
            do
            {
                AdminView.ChangePrice();
                var input = InputHelper.InputFreeText();
                if (string.IsNullOrEmpty(input))
                {
                    SharedError.EmptyInput();
                    continue;
                }
                bool convertedSuccess = int.TryParse(input, out int price);
                if (convertedSuccess && price >= 0)
                {
                    if (book.Price != price)
                    {
                        book.Price = price;
                        api.UpdateBook(admin.Id, book.Id, book.Title, book.Author, book.Price);
                        continueLoop = false;
                    }
                }
                else
                {
                    SharedError.PrintWrongInput();
                }
            } while (continueLoop);
        }

        private static void ChangeTitle(User admin, Book book)
        {
            var continueLoop = true;
            do
            {
                AdminView.ChangeTitle();
                var input = InputHelper.InputFreeText();
                if (string.IsNullOrEmpty(input))
                {
                    SharedError.EmptyInput();
                    continue;
                }
                if (book.Title != input)
                {
                    book.Title = input;
                    api.UpdateBook(admin.Id, book.Id, book.Title, book.Author, book.Price);
                    continueLoop = false;
                }

            } while (continueLoop);
        }

        private static void DeleteBook(Book book, User admin)
        {
            bool continueLoop = true;
            do
            {
                Console.Clear();
                AdminView.DeleteBook(book);
                var input = SharedController.GetAndValidateInput();
                if (input.validatedInput > 0)
                {
                    var originalBookAmount = book.Amount;
                    if (api.DeleteBook(admin.Id, book.Id, input.validatedInput))
                    {
                        SharedError.Success();
                        if (book.Amount == originalBookAmount
                            && book.Amount - input.validatedInput <= 0)
                        {
                            book.Amount = 0;
                        }
                    }
                    else
                    {
                        SharedError.Failed();
                    }
                    continueLoop = false;


                }
                else
                {
                    SharedError.PrintWrongInput();
                }
            } while (continueLoop);


        }

        private static void EditUser(User admin, User editUser)
        {
            var continueLoop = true;
            do
            {
                AdminView.EditUser(editUser);
                var input = SharedController.GetAndValidateInput();
                switch (input.validatedInput)
                {
                    case 1:
                        if (api.Promote(admin.Id, editUser.Id))
                        {
                            SharedError.Success();
                            break;
                        }
                        SharedError.Failed();
                        break;

                    case 2:
                        if (api.Demote(admin.Id, editUser.Id))
                        {
                            SharedError.Success();
                            break;
                        }
                        SharedError.Failed();
                        break;
                    case 3:
                        if (api.ActivateUser(admin.Id, editUser.Id))
                        {
                            SharedError.Success();
                            break;
                        }
                        SharedError.Failed();
                        break;
                    case 4:
                        if (api.InactivateUser(admin.Id, editUser.Id))
                        {
                            SharedError.Success();
                            break;
                        }
                        SharedError.Failed();
                        break;
                    case 0:
                        continueLoop = SharedController.GoBackIf_X_IsPressedOrPrintErrorMsg(input.menuInput);
                        break;
                    default:
                        SharedError.PrintWrongMenuInput();
                        break;
                }
            } while (continueLoop);

        }

        private static void GetAllBooksAndEnterBookOptions(User admin)
        {
            var availibleBooks = api.GetAvailibleBooks();
            BookView.ListAllBooks(availibleBooks);
            var input = SharedController.GetAndValidateInput();
            if (input.validatedInput != 0
                && input.validatedInput <= availibleBooks.Count)
            {
                AdminController.BookOptions(availibleBooks[input.validatedInput - 1], admin);
            }
            else
            {
                SharedError.PrintWrongInput();
            }
        }

        private static void GetBookAndEnterBookOptions(User admin)
        {
            var book = BookController.SearchForBook();
            if (book != null)
            {
                BookOptions(book, admin);
            }
        }
        private static void ListAllUsers(User admin)
        {
            var listWithUsers = api.ListUsers(admin.Id);
            var continueLoop = true;
            do
            {
                Console.Clear();
                AdminView.ListAllUsers(listWithUsers);
                var input = SharedController.GetAndValidateInput();
                if (input.validatedInput == 0 || input.validatedInput > listWithUsers.Count)
                {
                    SharedError.PrintWrongMenuInput();
                }
                else
                {
                    EditUser(admin, listWithUsers[input.validatedInput - 1]);
                    continueLoop = false;
                }
            } while (continueLoop);

        }

        private static void SearchForUser(User admin)
        {
            AdminView.SearchForUser(admin);
            var searchKeyword = SharedController.GetSearchInput();
            var listWithUsers = api.FindUser(admin.Id, searchKeyword);
            if (listWithUsers.Count > 0)
            {
                var continueLoop = true;
                do
                {
                    AdminView.ListAllUsers(listWithUsers);
                    var input = SharedController.GetAndValidateInput();
                    if (input.validatedInput == 0 || input.validatedInput > listWithUsers.Count)
                    {
                        SharedError.PrintWrongMenuInput();
                    }
                    else
                    {
                        EditUser(admin, listWithUsers[input.validatedInput - 1]);
                        continueLoop = false;
                    }

                } while (continueLoop);
            }
            else
            {

                SharedError.NothingFound();
            }
        }
        private static void SetAmount(Book book, User admin)
        {
            AdminView.SetAmount();
            var input = SharedController.GetAndValidateInput();
            if (input.validatedInput != 0)
            {
                WebShopApi api = new WebShopApi();
                if (api.SetAmount(admin.Id, book.Id, input.validatedInput))
                {
                    if (book.Amount <= 0)
                    {
                        book.Amount = 0;
                    }
                    SharedError.Success();
                    BookView.ChangedNumberOfBooks(book);
                }
                else
                {
                    SharedError.Failed();
                    BookView.ChangedNumberOfBooks(book);
                }
            }
            else
            {
                SharedError.PrintWrongMenuInput();
            }

            //TODO fixa det här, metoden ej klar.
        }

        private static void UpdateBookInfo(Book book, User admin)
        {
            var continueLoop = true;
            do
            {
                Console.Clear();
                AdminView.UpdateBookInfo(book);
                var input = SharedController.GetAndValidateInput();
                switch (input.validatedInput)
                {
                    case 1:
                        Console.Clear();
                        ChangeTitle(admin, book);
                        break;
                    case 2:
                        Console.Clear();
                        ChangeAuthor(admin, book);
                        break;
                    case 3:
                        Console.Clear();
                        ChangePrice(admin, book);
                        break;
                    case 4:
                        Console.Clear();
                        //ChangeCategory(book);
                        break;
                    case 0:
                        continueLoop = SharedController.GoBackIf_X_IsPressedOrPrintErrorMsg(input.menuInput);
                        break;
                    default:
                        SharedError.PrintWrongMenuInput();
                        break;
                }
            } while (continueLoop);
        }
    }
}
