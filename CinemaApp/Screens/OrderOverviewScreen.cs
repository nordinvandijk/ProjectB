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
        public string CreateOverview()
        {
            float totalPrice = 0;
            string overviewTable = new String('=', 50) + "\n";

            // Displaying orderNumber and username
            overviewTable += $"|Info|\n   Ordernummer: {App.seatsOverviewScreen.currentOrder.orderID}\n   Op naam van: {App.seatsOverviewScreen.currentOrder.username}\n";
            
            // Displaying all seats and their cost
            overviewTable += "|Stoelen|\n";
            foreach (Seat seat in App.seatsOverviewScreen.currentOrder.seats)
            {
                overviewTable += $"   Stoel (Rij: {seat.Row} Stoel Nummer: {seat.SeatNumber}) Prijs: {seat.Price} Euro\n";
                totalPrice += seat.Price;
            }

            //displaying all accessoires and their cost
            overviewTable += "\n|Accessoires|\n";
            foreach (string snack in App.seatsOverviewScreen.currentOrder.accessoires)
            {
                overviewTable += "Hier komen snacks\n";
            }

            //displaying all snacks and thier cost
            overviewTable += "\n|Snacks|\n";
            foreach (string accesoire in App.seatsOverviewScreen.currentOrder.snacks)
            {
                overviewTable += "Hier komen accessoires\n";
            }

            // Total price
            overviewTable += new String('=', 50) + "\n";
            overviewTable += $"Totale prijs: {totalPrice} euro\n";
            overviewTable += new String('=', 50) + "\n";

            return overviewTable;
        }
        public override void run()
        {
            Clear();
            string text = CreateOverview();
            string[] options = { "Terug naar homescreen" };
            Menu orderOverviewMenu = new Menu(options, text, 0);
            int chosenOption = orderOverviewMenu.Run();

            switch (chosenOption)
            {
                case 0:
                    App.homeScreen.run();
                    break;
            }
        }
    }   
}