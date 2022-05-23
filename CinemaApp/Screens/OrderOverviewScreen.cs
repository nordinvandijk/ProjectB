using System;
using static System.Console;
using static CinemaApp.ConsoleUtils;

namespace CinemaApp.Screens
{
    class OrderOverviewScreen : Screen
    {
        //Fields

        //Constructor
        public OrderOverviewScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            Clear();
            Console.WriteLine("Order overview");
            WaitForKeyPress();
            App.homeScreen.run();
        }
    }   
}