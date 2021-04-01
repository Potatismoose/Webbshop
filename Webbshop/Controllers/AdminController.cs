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
        public static void PrintAdminSelectionMenu(User user)
        {
            SharedView.PrintWithGreenText($"\tVälkommen {user.Name}");
            var exitAdmin = false;
            do
            {
                AdminView.Print();
                string menuInput = InputHelper.AskForMenuInput();
                
            } while (!exitAdmin);
            ;

            
        }
    }
}
