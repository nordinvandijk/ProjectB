using System;
using System.Collections.Generic;
using System.Linq;
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
            Order currentOrder = App.seatsOverviewScreen.currentOrder;

            // Displaying orderNumber and username
            overviewTable += $"|Algemene Informatie|\n   Ordernummer: {currentOrder.OrderID}\n   Op naam van: {currentOrder.Username}\n";

            // Displaying movie info
            if (App.filmInfoScreen.chosenMovieItem.IsEvent) { overviewTable += $"\n|Informatie Evenement|\n"; }
            else { overviewTable += $"\n|Informatie Film|\n"; }

            overviewTable += $"   Titel: {currentOrder.FilmTitle}\n   Uitvoering: {currentOrder.Format}\n";
            overviewTable += $"   Locatie: {currentOrder.LocationName}\n";
            overviewTable += $"   Datum: {currentOrder.StartTimeString.Substring(0, 10)}\n   Tijd: {currentOrder.StartTimeString.Substring(11)} - {currentOrder.EndTimeString.Substring(11)}\n";

            // Displaying all seats and their cost
            overviewTable += "\n|Stoelen|\n";
            foreach (Seat seat in currentOrder.Seats)
            {
                overviewTable += $"   Stoel (Rij: {seat.Row} Stoelnummer: {seat.SeatNumber}) Prijs: {String.Format("{0:0.00}",seat.Price)} euro\n";
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
                    overviewTable += $"   {addableItem.Name} (Hoeveelheid: {amountOfItem}) Prijs: {String.Format("{0:0.00}",addableItem.Price * amountOfItem)} euro\n";
                    // 'addableItemName' wordt toegevoegd aan already found
                    alreadyFound.Add(addableItem);
                }
                // Voor elke 'addableItemName' in current order wordt de prijs toegevoegd aan totalPrice
                totalPrice += addableItem.Price;
            }

            // Total price
            overviewTable += new String('=', 50) + "\n";
            overviewTable += $"Totale prijs: {String.Format("{0:0.00}",totalPrice)} euro\n";
            overviewTable += new String('=', 50) + "\n";

            return overviewTable;
        }
        public override void run()
        {
            Clear();
            string text = CreateOverview();
            string[] options = { "Terug naar hoofdmenu" };
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