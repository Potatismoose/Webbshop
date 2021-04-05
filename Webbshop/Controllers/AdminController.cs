using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Webbshop.Utils;
using Webbshop.Views;
using webshopAPI;

namespace Webbshop.Controllers
{
    class AdminController
    {
        private static WebShopApi api = new WebShopApi();

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
                    }
                }
            } while (!logoutUser);
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
                        SearchForUser(admin);
                        break;
                    case 5:
                        SearchForUser(admin);
                        break;
                    case 6:
                        BookController.SearchForBook(admin);
                        break;
                    case 0:
                        continueLoop = SharedController.GoBackIf_X_IsPressedOrPrintErrorMsg(input.menuInput);
                        break;
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
                        break;
                    case 4:
                        break;
                    case 0:
                        if (input.menuInput.ToLower() == "x")
                        {
                            continueLoop = false;
                            break;
                        }
                        SharedError.PrintWrongMenuInput();
                        break;
                    default:
                        SharedError.PrintWrongMenuInput();
                        break;
                }
            } while (continueLoop);
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
                        if (input.menuInput.ToLower() == "x")
                        {
                            continueLoop = false;
                            break;
                        }
                        SharedError.PrintWrongMenuInput();
                        break;
                    default:
                        SharedError.PrintWrongMenuInput();
                        break;
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

        private static void SetAmount(Book book, User admin)
        {
            AdminView.SetAmount();
            var input = SharedController.GetAndValidateInput();
            if (input.validatedInput != 0)
            {
                WebShopApi api = new WebShopApi();
                if (api.SetAmount(admin.Id, book.Id, input.validatedInput))
                {
                    if (book.Amount + input.validatedInput <= 0)
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
    }
}
