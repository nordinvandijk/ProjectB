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

            //code om te kijken of order een film of event is
            List<string> allMovieTitles = new List<string>();
            foreach(Movie movie in App.movieManager.movies)
            {
                allMovieTitles.Add(movie.Title);
            }

            if (allMovieTitles.Exists(x => x == order.FilmTitle))
            {
                overviewTable += $"\n|Informatie Film|\n";
            }
            else
            {
                overviewTable += $"\n|Informatie Evenement|\n";
            }
            overviewTable += $"   Titel: {order.FilmTitle}\n   Uitvoering: {order.Format}\n";
            overviewTable += $"   Locatie: {order.LocationName}\n";
            overviewTable += $"   Datum: {order.StartTimeString.Substring(0, 10)}\n   Tijd: {order.StartTimeString.Substring(11)} - {order.EndTimeString.Substring(11)}\n";

            // Displaying all seats and their cost
            overviewTable += "\n|Stoelen|\n";
            foreach (Seat seat in order.Seats)
            {
                overviewTable += $"   Stoel (Rij: {seat.Row} Stoelnummer: {seat.SeatNumber}) Prijs: {String.Format("{0:0.00}",seat.Price)} euro\n";
                totalPrice += seat.Price;
            }

            //displaying all addableItems and their cost
            overviewTable += "\n|Extra's|\n";
            List<AddableItem> alreadyFound = new List<AddableItem>();
            foreach (AddableItem addableItem in order.AddableItems)
            {
                // Als 'addableItemName' niet in de lijst alreadyFound staat
                if (!(alreadyFound.Exists(x => x.Name == addableItem.Name)))
                {
                    // Hoevaak 'addableItemName' aanwezig in de current order wordt opgeslagen in een int
                    int amountOfItem = order.AddableItems.Where(x => x.Name == addableItem.Name).Count();
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
            // OrderNames worden gebruikt voor het maken van een menu
            List<string> orderNames = new List<string>();
            
            // Orders van ingelogde klant worden opgeslagen in deze list
            List<Order> orders = new List<Order>();

            string filmOrEvent = "";
            foreach(Order order in App.orderManager.orders)
            {
                if(order.Username == App.userManager.currentUser.Username)
                {
                    // Code om te kijken of order gaat om een film of een evenement
                    List<string> allMovieTitles = new List<string>();
                    foreach (Movie movie in App.movieManager.movies)
                    {
                        allMovieTitles.Add(movie.Title);
                    }

                    if (allMovieTitles.Exists(x => x == order.FilmTitle))
                    {
                        filmOrEvent = "Film";
                    }
                    else
                    {
                        filmOrEvent = "Evenement";
                    }
                    orderNames.Add($"{filmOrEvent}: {order.FilmTitle} | Datum: {order.StartTimeString.Substring(0,10)} | Tijd: {order.StartTimeString.Substring(11)} - {order.EndTimeString.Substring(11)} | Locatie: {order.LocationName}");
                    orders.Add(order);
                }
            }
            orderNames.Add("\nTerug");

            string title = "Dit zijn al uw orders: Druk op enter om meer details te zien over de order";
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