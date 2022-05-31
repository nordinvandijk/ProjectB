using System;
using System.Collections.Generic;
using static System.Console;
using static CinemaApp.ConsoleUtils;

namespace CinemaApp.Screens
{
    class SeatsOverviewScreen : Screen
    {
        //Fields
        public SeatSelector seatSelector;
        public Order currentOrder;

        //Constructor
        public SeatsOverviewScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            User currentUser = App.userManager.currentUser;
            MovieItem chosenMovieItem = App.filmInfoScreen.chosenMovieItem;
            
            // Reset alle geselecteerde seats. Dit moet gebeuren als iemand op terug klikt in het addToOrderScreen
            foreach (Seat[] seatArray in chosenMovieItem.Seats)
            {
                foreach(Seat seat in seatArray)
                {
                    if(seat.Availability == "selected")
                    {
                        seat.Availability = "available";
                    }
                }
            }
            // Runt seat selector
            seatSelector = new SeatSelector(chosenMovieItem.Seats);
            bool isConfirmed = seatSelector.Run();

            // Als er meer als 0 stoelen zijn gekozen en er op bevestigen is geklikt
            if (seatSelector.selectedSeats.Count > 0 && isConfirmed)
            {
                // Als er een user is ingelogd is wordt er een order aangemaakt met username en de geselecteerde stoelen in string vorm
                if (App.userManager.currentUser != null)
                {
                    currentOrder = App.orderManager.CreateOrder(currentUser.Username, seatSelector.selectedSeats, chosenMovieItem);
                }
                
                // Als er geen user ingelogd is wordt er een order aangemaakt met alleen de geselecteerde stoelen
                else
                {
                    currentOrder = App.orderManager.CreateOrder(seatSelector.selectedSeats, chosenMovieItem);
                }
                
                // Order is aangemaakt dus de volgende pagina wordt gerunt
                App.addToOrderScreen.run();
            }
            // Als er geen stoelen zijn gekozen maar wel op bevestigen wordt geklikt
            else if (seatSelector.selectedSeats.Count <= 0 && isConfirmed)
            {
                Clear();
                Console.WriteLine("Kies tenminste 1 stoel!");
                WaitForKeyPress();
                run();
            }
            // Als er niet op bevestigen maar op terug wordt gekilkt
            else if (!isConfirmed)
            {
                App.filmInfoScreen.run();
            }
        }
    }   
}