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
        public string CreateOverview()
        {
            float totalPrice = 0;
            string overviewTable = new String('=', 50) + "\n";

            // Displaying all seats and their cost
            overviewTable += "|Stoelen|\n";
            foreach(Seat seat in App.seatsOverviewScreen.currentOrder.Seats)
            {
                overviewTable += $"   Stoel (Rij: {seat.Row} Stoel Nummer: {seat.SeatNumber}) Prijs: {seat.Price} Euro\n";
                totalPrice += seat.Price;
            }

            //displaying all addableItems and their cost
            overviewTable += "\n|Accessoires|\n";
            foreach(string snack in App.seatsOverviewScreen.currentOrder.AddableItems)
            {
                overviewTable += "Hier komen snacks\n";
            }

            // Total price
            overviewTable += new String('=', 50) + "\n";
            overviewTable += $"Totale prijs: {totalPrice} euro\n";
            overviewTable += new String('=', 50) + "\n";

            return overviewTable;
        }
        public override void run()
        {
            Menu orderConfirmationMenu;
            Order currentOrder = App.seatsOverviewScreen.currentOrder;
            User currentUser = App.userManager.currentUser;

            // Als iemand vanuit de order confirmation inlogd wordt de username toegevoegd aan de currentOrder
            if (currentUser != null)
            {
                currentOrder.Username = currentUser.Username;
            }

            // Als er iemand is ingelogd hoeft hij alleen maar op bevestigen te klikken
            if (currentOrder.Username != null)
            {
                string text = CreateOverview();
                string[] options = { "Bevestigen", "Terug" };
                orderConfirmationMenu = new Menu(options,text,0);
                int chosenOption = orderConfirmationMenu.Run();

                switch (chosenOption)
                {
                    case 0:
                        // Order wordt opgeslagen in json
                        foreach (Seat seat in currentOrder.Seats)
                        {
                            seat.Availability = "occupied";
                        }

                        // Order wordt toegevoegd aan json
                        App.orderManager.orders.Add(currentOrder);
                        App.orderManager.UpdateJson();

                        // Geselecteerde stoelen worden op occupied gezet
                        foreach(Seat seat in App.seatsOverviewScreen.seatSelector.selectedSeats)
                        {
                            seat.Availability = "occupied";
                        }
                        App.filmAgenda.UpdateJson();

                        // Order is bevestigd dus het orderOverviewScreen wordt gerunt
                        App.orderOverviewScreen.run();
                        break;
                    
                    case 1:
                        App.addToOrderScreen.run();
                        break;
                }
                    
            }
            // Als er niemand is ingelogd moet er nu een account aangemaakt worden of worden ingelogd
            else
            {
                string text = CreateOverview() + "\nOm uw order te kunnen bevestigen moet u inloggen of een account aanmaken";
                string[] options = { "Inloggen", "Aanmelden", "Terug" };
                orderConfirmationMenu = new Menu(options, text, 0);
                int chosenOption = orderConfirmationMenu.Run();

                switch (chosenOption)
                {
                    case 0:
                        App.logInScreen.RunFromOrderConfirmation();
                        break;
                    case 1:
                        App.accountCreationScreen.RunFromOrderConfirmation();
                        break;
                    case 2:
                        App.addToOrderScreen.run();
                        break;
                }
                
            }
            
           

        }
    }   
}