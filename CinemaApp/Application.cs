using System;
using static System.Console;
using CinemaApp.Screens; //Using namespace folder Screens

namespace CinemaApp
{
    class Application
    {
        //Fields
        public HomeScreen homeScreen;
        public CinemasScreen cinemasScreen;
        //Constructor
        public Application()
        {
            homeScreen = new HomeScreen(this);
            cinemasScreen = new CinemasScreen(this);
        }

        //Methods
        public void Start()
        {
            homeScreen.run();
            ConsoleUtils.WaitForKeyPress();
            cinemasScreen.run();
            ConsoleUtils.WaitForKeyPress();
        }
    }
}