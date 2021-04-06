using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webbshop.Utils;
using Webbshop.Views;
using webshopAPI;

namespace Webbshop.Controllers
{
    class UserController
    {

        public static bool UserIsNull(User user)
        {
            if (user == null)
            {
                return true;
            }
            
            return false;    
        }

        internal static void UserSelectionMenu(User user)
        {
            var continueLoop = true;
            do
            {
                UserView.PrintBuyMenuOptions();
                var input = SharedController.GetAndValidateInput();
                switch (input.validatedInput)
                {
                    case 1:
                        SharedController.BuyBookMenu(user);
                        break;
                    case 0:
                        if (SharedController.LogoutIf_X_IsPressed(
                            user,
                            input.menuInput,
                            input.validatedInput))
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
    }
}
