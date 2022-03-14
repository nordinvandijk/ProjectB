using System;
using static System.Console;
using CinemaApp.Screens; //Using namespace folder Screens

namespace CinemaApp
{
    class Application
    {
        //Fields
        public HomeScreen homeScreen;

        //Constructor
        public Application()
        {
            homeScreen = new HomeScreen(this);
        }

        //Methods
        public void Start()
        {
            homeScreen.run();
            ConsoleUtils.WaitForKeyPress();
        }
    }
}