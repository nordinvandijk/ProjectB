using System;
using static System.Console;

namespace CinemaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Application App = new Application();
            App.Start();
        }
    }
}