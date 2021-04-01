using System;

namespace Webbshop.Views
{
    static class LoginView
    {

        public static (string userName, string password) PrintLoginPage()
        {
            Console.WriteLine("Logga in\n");
            Console.Write("\tAnvändarnamn> ");
            var userName = Console.ReadLine();
            Console.Write("\tLösenord> ");
            var password = Console.ReadLine();

            return (userName, password);
        }
    }
}
