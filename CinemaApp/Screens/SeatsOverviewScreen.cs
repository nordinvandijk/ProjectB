using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class SeatsOverviewScreen : Screen
    {
        //Fields
        SeatSelector seatSelector;

        //Constructor
        public SeatsOverviewScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            // Runt seat selector
            seatSelector = new SeatSelector(App.filmInfoScreen.chosenMovieItem.Seats);
            seatSelector.Run();

            // Als de gekozen stoelen zijn bevestigd
            App.addToOrderScreen.run();
        }
    }   
}