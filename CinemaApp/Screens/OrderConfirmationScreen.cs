using System;
using static System.Console;
using static CinemaApp.ConsoleUtils;
using System.Linq;
using System.Collections.Generic;

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
            Order currentOrder = App.seatsOverviewScreen.currentOrder;
            Omzet currentOmzet = App.seatsOverviewScreen.currentOmzet;

            // Displaying movie info
            if (App.filmInfoScreen.chosenMovieItem.IsEvent) { overviewTable += $"|Informatie Evenement|\n"; }
            else { overviewTable += $"|Informatie Film|\n"; }

            overviewTable += $"   Titel: {currentOrder.FilmTitle}\n   Uitvoering: {currentOrder.Format}\n";
            overviewTable += $"   Locatie: {currentOrder.LocationName}\n";
            overviewTable += $"   Datum: {currentOrder.StartTimeString.Substring(0, 10)}\n   Tijd: {currentOrder.StartTimeString.Substring(11)} - {currentOrder.EndTimeString.Substring(11)}\n";

            // Displaying all seats and their cost
            overviewTable += "\n|Stoelen|\n";
            foreach(Seat seat in currentOrder.Seats)
            {
                overviewTable += $"   Stoel (Rij: {seat.Row} Stoel Nummer: {seat.SeatNumber}) Prijs: {@"€"} {String.Format("{0:0.00}", seat.Price)}\n";
                totalPrice += seat.Price;
            }

            //displaying all addableItems and their cost
            overviewTable += "\n|Extra's|\n";
            List<AddableItem> alreadyFound = new List<AddableItem>();
            foreach (AddableItem addableItem in currentOrder.AddableItems)
            {
                // Als 'addableItemName' niet in de lijst alreadyFound staat
                if (!(alreadyFound.Exists(x => x.Name == addableItem.Name)))
                {
                    // Hoevaak 'addableItemName' aanwezig in de current order wordt opgeslagen in een int
                    int amountOfItem = currentOrder.AddableItems.Where(x => x.Name == addableItem.Name).Count();
                    // 'addableItemName' de hoeveelheid en de prijs wordt gedisplayt
                    overviewTable += $"   {addableItem.Name} (Hoeveelheid: {amountOfItem}) Prijs: {@"€"} {String.Format("{0:0.00}",addableItem.Price * amountOfItem)}\n";
                    // 'addableItemName' wordt toegevoegd aan already found
                    alreadyFound.Add(addableItem);
                }
                // Voor elke 'addableItemName' in current order wordt de prijs toegevoegd aan totalPrice
                totalPrice += addableItem.Price;
            }

            // Total price
            overviewTable += new String('=', 50) + "\n";
            overviewTable += $"Totale prijs: {@"€"} {String.Format("{0:0.00}",totalPrice)}\n";
            overviewTable += new String('=', 50) + "\n";

            return overviewTable;
        }
        public override void run()
        {
            Menu orderConfirmationMenu;
            Order currentOrder = App.seatsOverviewScreen.currentOrder;
            User currentUser = App.userManager.currentUser;
            Omzet currentOmzet = App.seatsOverviewScreen.currentOmzet;

            // Als iemand vanuit de order confirmation inlogd wordt de username toegevoegd aan de currentOrder
            if (currentUser != null)
            {
                currentOrder.Username = currentUser.Username;
            }

            // Als er iemand is ingelogd hoeft hij alleen maar op bevestigen te klikken
            if (currentOrder.Username != null)
            {
                App.orderManager.ApplySubscriptionDiscount(currentUser,currentOrder);
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
                        currentOmzet.AddableItems = currentOrder.AddableItems;
                        App.orderManager.orders.Add(currentOrder); 
                        App.omzetManager.totaalOmzet.Add(currentOmzet);
                        App.orderManager.UpdateJson();
                        App.omzetManager.UpdateJson();

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