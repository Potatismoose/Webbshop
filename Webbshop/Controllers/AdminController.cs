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
    }
}
