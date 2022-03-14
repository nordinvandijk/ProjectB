using System;
using static System.Console;

namespace CinemaApp
{
    static class ConsoleUtils //Static class can't be instantiated
    {
        //Methods

        public static void WaitForKeyPress()
        {
            WriteLine("Press any key to continue...");
            ReadKey(true);
        }
    }
}