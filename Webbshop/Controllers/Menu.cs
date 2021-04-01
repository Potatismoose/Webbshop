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
    static class Menu
    {
        static int menuOptionCounter = 1;
        

        public static void PrintMainMenu()
        {
            int userMenuChoice;
            string menuInput = default;
            do
            {
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
                            
                            if (UserController.UserIsNull(user))
                            {
                                SharedError.PrintWrongCredentials(user);
                                Thread.Sleep(2000);
                                Console.Clear();
                                continue;
                            }
                            else if (user.IsAdmin)
                            {
                                AdminController.PrintAdminSelectionMenu(user);
                            }
                            else
                            {
                                

                                UserView.PrintBuyMenuOptions(SharedView.GetBuyMenuOptions());
                                userMenuChoice = InputHelper.ValidateMenuInput(InputHelper.AskForMenuInput());
                                Console.ReadKey();
                            }
                            break;

                        case 2:
                            var registererUserCredentials = UserView.Register();
                            if (!api.Register(registererUserCredentials.username,
                                registererUserCredentials.password,
                                registererUserCredentials.verifyPassword))
                            {
                                SharedError.Failed();
                                Thread.Sleep(2000);
                                Console.Clear();
                                break;
                            }
                            SharedError.Success();
                            Thread.Sleep(2000);
                            Console.Clear();
                            break;
                        case 3:
                            break;
                        case 0:
                            Environment.Exit(1);
                            break;
                        default:
                            break;
                    }
                }
            } while (menuInput.ToLower() != "q");
        }



        
    }
}
