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
            string overviewTable = new String('-', 30) + "\n";
            
            // Displaying all seats and their cost
            foreach(Seat seat in App.seatsOverviewScreen.currentOrder.seats)
            {
                overviewTable += $"Row: {seat.Row} SeatNumber: {seat.SeatNumber} Price: {seat.Price}\n";
            }

            //displaying all snacks and their cost
            foreach(string snack in App.seatsOverviewScreen.currentOrder.snacks)
            {
                overviewTable += "Hier komen snacks\n";
            }

            // Total price

            overviewTable += new String('-', 30) + "\n";

            return overviewTable;
        }
        public override void run()
        {
            Menu orderConfirmationMenu;
            
            // Als iemand vanuit de order confirmation inlogd wordt de username toegevoegd aan de currentOrder
            if (App.userManager.currentUser != null)
            {
                App.seatsOverviewScreen.currentOrder.username = App.userManager.currentUser.Username;
            }

            // Als er iemand is ingelogd hoeft hij alleen maar op bevestiggen te klikken
            if (App.seatsOverviewScreen.currentOrder.username != null)
            {
                string text = CreateOverview();
                string[] options = { "Bevestiggen", "Terug" };
                orderConfirmationMenu = new Menu(options,text,0);
                int chosenOption = orderConfirmationMenu.Run();

                switch (chosenOption)
                {
                    case 0:
                        // Order wordt opgeslagen in json
                        foreach (Seat seat in App.seatsOverviewScreen.currentOrder.seats)
                        {
                            seat.Availability = "occupied";
                        }
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
                string text = CreateOverview() + "\nOm uw order te kunnen bevestiggen moet u inloggen of een account aanmaken";
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