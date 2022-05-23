using System;
using static System.Console;
using static CinemaApp.ConsoleUtils;

namespace CinemaApp.Screens
{
    class AddToOrderScreen : Screen
    {
        //Fields

        //Constructor
        public AddToOrderScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            //App.seatsOverviewScreen.currentOrder.snacks.Add("Test");
            Clear();
            Console.WriteLine("Hier moet eten gekocht kunnen worden");
            WaitForKeyPress();
            App.orderConfirmationScreen.run();

        }
    }   
}