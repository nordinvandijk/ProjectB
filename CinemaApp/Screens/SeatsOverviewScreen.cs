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
            // Reset alle geselecteerde seats. Dit moet gebeuren als iemand op terug klikt in het addToOrderScreen
            foreach(Seat[] seatArray in App.filmInfoScreen.chosenMovieItem.Seats)
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
            seatSelector = new SeatSelector(App.filmInfoScreen.chosenMovieItem.Seats);
            seatSelector.Run();

            // Als er meer als 0 stoelen zijn gekozen en er op bevestigd is geklikt
            if (seatSelector.selectedSeats.Count > 0)
            {
                // Als er een user is ingelogd is wordt er een order aangemaakt met username en de geselecteerde stoelen in string vorm
                if (App.userManager.currentUser != null)
                {
                    currentOrder = App.orderManager.CreateOrder(App.userManager.currentUser.Username, seatSelector.selectedSeats);
                }
                
                // Als er geen user ingelogd is wordt er een order aangemaakt met alleen de geselecteerde stoelen
                else
                {
                    currentOrder = App.orderManager.CreateOrder(seatSelector.selectedSeats);
                }
                
                // Order is aangemaakt dus de volgende pagina wordt gerunt
                App.addToOrderScreen.run();
            }
            else
            {
                Clear();
                Console.WriteLine("Kies tenminste 1 stoel!");
                WaitForKeyPress();
                run();
            }
        }
    }   
}