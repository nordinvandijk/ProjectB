using System;
using static System.Console;
using static CinemaApp.ConsoleUtils;

namespace CinemaApp.Screens
{
    class OrderConfirmationScreen : Screen
    {
        //Fields

        //Constructor
        public OrderConfirmationScreen(Application app) : base(app) // Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            Clear();
            Console.WriteLine("Confirm order");
            WaitForKeyPress();
            App.orderOverviewScreen.run();

        }
    }   
}