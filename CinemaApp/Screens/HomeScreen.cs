using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class HomeScreen : Screen
    {
        //Fields

        //Constructor
        public HomeScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            WriteLine("HomeScreen is running");
            ConsoleUtils.WaitForKeyPress();
        }
    }   
}