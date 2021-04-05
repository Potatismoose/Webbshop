﻿using System;
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
    static class Menu
    {
        static string abortChar = "x";

        public static void PrintMainMenu()
        {
            int userMenuChoice;
            string menuInput = default;
            do
            {
                Console.Clear();
                StartView.Print();
                menuInput = InputHelper.AskForMenuInput();
                userMenuChoice = InputHelper.ValidateMenuInput(menuInput);

                if (userMenuChoice == 0 && menuInput.ToLower() != "q")
                {
                    SharedError.PrintWrongMenuInput();
                }
                else
                {
                    var api = new WebShopApi();
                    switch (userMenuChoice)
                    {
                        case 1:
                            var userCredentials = LoginView.PrintLoginPage();
                            var user = api.Login(userCredentials.userName, userCredentials.password);

                            if (UserController.UserIsNull(user)
                                && !userCredentials.userName.Contains(abortChar))
                            {
                                SharedError.PrintWrongCredentials(user);
                                
                                
                                continue;
                            }
                            else if (userCredentials.userName.Contains(abortChar))
                            { 
                                break; 
                            }
                            else if (user.IsAdmin)
                            {
                               
                                AdminController.PrintAdminSelectionMenu(user);
                            }
                            else
                            {
                                //TODO fixa
                                //UserView.PrintBuyMenuOptions(SharedView.GetBuyMenuOptions());
                                userMenuChoice = InputHelper.ValidateMenuInput(InputHelper.AskForMenuInput());
                                
                            }
                            break;

                        case 2:
                            var registererUserCredentials = UserView.Register();
                            if (!api.Register(registererUserCredentials.username,
                                registererUserCredentials.password,
                                registererUserCredentials.verifyPassword))
                            {
                                SharedError.Failed();
                                break;
                            }
                            SharedError.Success();
                            break;
                        case 0:
                            Environment.Exit(1);
                            break;
                        default:
                            SharedError.PrintWrongMenuInput();
                            break;
                    }
                }
            } while (menuInput.ToLower() != "q");
        }



        
    }
}
