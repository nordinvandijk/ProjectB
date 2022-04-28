﻿using System;
using static System.Console;

namespace CinemaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SeatSelector test = new SeatSelector();
            test.Run();
            ConsoleUtils.WaitForKeyPress();
            Console.CursorVisible = false;
            Application App = new Application();
            App.Start();
        }
    }
}