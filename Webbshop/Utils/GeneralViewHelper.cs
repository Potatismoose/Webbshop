using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Webbshop.Utils
{
    class GeneralViewHelper
    {
        internal static void WaitAndClearScreen()
        {
            Thread.Sleep(2000);
            Console.Clear();
        }
    }
}
