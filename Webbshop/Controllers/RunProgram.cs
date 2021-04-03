using System;
using Webbshop.Views;
using webshopAPI;
using System.Threading;
using webshopAPI.Helpers;

namespace Webbshop.Controllers
{
    class RunProgram
    {
        public void StartProgram()
        {
            Seeder.Seed();
            Menu.PrintMainMenu();
        }

        
    }
}
