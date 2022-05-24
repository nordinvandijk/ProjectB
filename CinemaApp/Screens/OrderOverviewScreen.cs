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
            overviewTable += $"|Algemene Informatie|\n   Ordernummer: {App.seatsOverviewScreen.currentOrder.OrderID}\n   Op naam van: {App.seatsOverviewScreen.currentOrder.Username}\n";

            // Displaying movie info
            overviewTable += $"\n|Informatie Film|\n";
            overviewTable += $"   Titel: {currentOrder.FilmTitle}\n   Uitvoering: {currentOrder.Format}\n";
            overviewTable += $"   Datum: {currentOrder.StartTimeString.Substring(0, 8)}\n   Tijd: {currentOrder.StartTimeString.Substring(11)} - {currentOrder.EndTimeString.Substring(11)}\n";

            // Displaying all seats and their cost
            overviewTable += "\n|Stoelen|\n";
            foreach (Seat seat in currentOrder.Seats)
            {
                overviewTable += $"   Stoel (Rij: {seat.Row} Stoel Nummer: {seat.SeatNumber}) Prijs: {seat.Price} Euro\n";
                totalPrice += seat.Price;
            }

            //displaying all addableItems and their cost
            overviewTable += "\n|Extra's|\n";
            List<string> alreadyFound = new List<string>();
            foreach (string addableItemName in currentOrder.AddableItems)
            {
                // Als 'addableItemName' niet in de lijst alreadyFound staat
                if (!(alreadyFound.Exists(x => x == addableItemName)))
                {
                    // Hoevaak 'addableItemName' aanwezig in de current order wordt opgeslagen in een int
                    int amountOfItem = currentOrder.AddableItems.Where(x => x == addableItemName).Count();
                    // 'addableItemName' de hoeveelheid en de prijs wordt gedisplayt
                    overviewTable += $"   {addableItemName} (Hoeveelheid: {amountOfItem}) Prijs: {App.addableItemsManager.addableItems.Find(x => x.Name == addableItemName).Price * amountOfItem}\n";
                    // 'addableItemName' wordt toegevoegd aan already found
                    alreadyFound.Add(addableItemName);
                }
                // Voor elke 'addableItemName' in current order wordt de prijs toegevoegd aan totalPrice
                totalPrice += App.addableItemsManager.addableItems.Find(x => x.Name == addableItemName).Price;
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