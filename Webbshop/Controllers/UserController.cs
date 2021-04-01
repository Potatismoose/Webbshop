using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
