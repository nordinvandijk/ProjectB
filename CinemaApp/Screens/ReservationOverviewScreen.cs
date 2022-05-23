using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class ReservationOverviewScreen : Screen
    {
        //Fields

        //Constructor
        public ReservationOverviewScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public string CreateOverview(Order order)
        {
            float totalPrice = 0;
            string overviewTable = new String('=', 50) + "\n";

            // Displaying orderNumber and username
            overviewTable += $"|Info|\n   Ordernummer: {order.orderID}\n   Op naam van: {order.username}\n";

            // Displaying all seats and their cost
            overviewTable += "|Stoelen|\n";
            foreach (Seat seat in order.seats)
            {
                overviewTable += $"   Stoel (Rij: {seat.Row} Stoel Nummer: {seat.SeatNumber}) Prijs: {seat.Price} Euro\n";
                totalPrice += seat.Price;
            }

            //displaying all accessoires and their cost
            overviewTable += "\n|Accessoires|\n";
            foreach (string snack in order.accessoires)
            {
                overviewTable += "Hier komen snacks\n";
            }

            //displaying all snacks and thier cost
            overviewTable += "\n|Snacks|\n";
            foreach (string accesoire in order.snacks)
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
            string textToDisplay = "";
            string[] options = { "Terug" };

            foreach(Order order in App.orderManager.orders)
            {
                if(order.username == App.userManager.currentUser.Username)
                {
                    textToDisplay += CreateOverview(order) + "\n";
                }
            }

            Menu reservationOverviewMenu = new Menu(options, textToDisplay, 0);
            int chosenOption = reservationOverviewMenu.Run();

            switch (chosenOption)
            {
                case 0:
                    App.homeScreen.run();
                    break;
            }
        }
    }   
}