using System;
using static System.Console;
using CinemaApp.Screens; //Using namespace folder Screens

namespace CinemaApp
{
    class Application
    {
        //Fields
        public HomeScreen MyHomeScreen;

        //Constructor
        public Application()
        {
            MyHomeScreen = new HomeScreen(this);
        }

        //Methods
        public void Start()
        {
            WriteLine("The Application is starting");
            ConsoleUtils.WaitForKeyPress();
        }
    }
}