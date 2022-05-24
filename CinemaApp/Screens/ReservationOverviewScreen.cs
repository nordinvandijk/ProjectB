using System;
using System.Collections.Generic;
using System.Linq;
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
            overviewTable += $"|Algemene Informatie|\n   Ordernummer: {order.OrderID}\n   Op naam van: {order.Username}\n";

            // Displaying movie info
            overviewTable += $"\n|Informatie Film|\n";
            overviewTable += $"   Titel: {order.FilmTitle}\n   Uitvoering: {order.Format}\n";
            overviewTable += $"   Locatie: {order.LocationName}\n";
            overviewTable += $"   Datum: {order.StartTimeString.Substring(0, 8)}\n   Tijd: {order.StartTimeString.Substring(11)} - {order.EndTimeString.Substring(11)}\n";

            // Displaying all seats and their cost
            overviewTable += "\n|Stoelen|\n";
            foreach (Seat seat in order.Seats)
            {
                overviewTable += $"   Stoel (Rij: {seat.Row} Stoel Nummer: {seat.SeatNumber}) Prijs: {seat.Price} Euro\n";
                totalPrice += seat.Price;
            }

            //displaying all addableItems and their cost
            overviewTable += "\n|Extra's|\n";
            List<string> alreadyFound = new List<string>();
            foreach (string addableItemName in order.AddableItems)
            {
                // Als 'addableItemName' niet in de lijst alreadyFound staat
                if (!(alreadyFound.Exists(x => x == addableItemName)))
                {
                    // Hoevaak 'addableItemName' aanwezig in de current order wordt opgeslagen in een int
                    int amountOfItem = order.AddableItems.Where(x => x == addableItemName).Count();
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
            // OrderNames worden gebruikt voor het maken van een menu
            List<string> orderNames = new List<string>();
            
            // Orders van ingelogde klant worden opgeslagen in deze list
            List<Order> orders = new List<Order>();

            foreach(Order order in App.orderManager.orders)
            {
                if(order.Username == App.userManager.currentUser.Username)
                {
                    orderNames.Add($"Film: {order.FilmTitle} Datum: {order.StartTimeString.Substring(0,8)}");
                    orders.Add(order);
                }
            }
            orderNames.Add("Terug");

            string title = "Dit zijn al uw orders:";
            string[] options = orderNames.ToArray();
            Menu reservationOverviewMenu = new Menu(options, title, 0);
            int chosenOption = reservationOverviewMenu.Run();

            // Terug knop op scherm overzicht reserveringen
            if (chosenOption == options.Length - 1)
            {
                App.homeScreen.run();
            }

            // Gekozen order bekijken met terug knop
            else
            {
                string displayOrder = CreateOverview(orders[chosenOption]);
                string[] optionsDisplayOrder = { "Terug" };
                Menu displayOrderMenu = new Menu(optionsDisplayOrder, displayOrder, 0);
                int chosenOptionDisplayOrder = displayOrderMenu.Run();

                switch (chosenOptionDisplayOrder)
                {
                    case 0:
                        run();
                        break;
                }
            }
        }
    }   
}