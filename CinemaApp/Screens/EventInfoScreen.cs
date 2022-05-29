using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class EventInfoScreen : Screen
    {
        //Fields
        Event Event;

        //Constructor
        public EventInfoScreen(Application app) : base(app)
        {
        }

        //Methods

        public override void run()
        {
            //gaat door events om de event te vinden die is gekozen bij eventscreen
            foreach (Event ev in App.eventManager.events) {
                if (ev.Name == App.eventScreen.ChosenEvent) {
                    Event = ev;
                    break;
                }
            }

            //displayt alle details in een string
            string titel = Event.Name + "\nBeschrijving: " + Event.Description +
                        "\nMinimale leeftijd: " + Event.MinimumAge +
                         "\nTijdsduur: " + Event.Duration + "\nTicket prijs: " + Event.TicketPrice;

            string[] options = {"Evenementen overzicht"};
            Menu EventInfoMenu = new Menu(options, titel, 0);
            int ChosenOption = EventInfoMenu.Run();

            switch(ChosenOption)
            {
                case 0: //terug naar eventscreen
                    App.eventScreen.run();
                    break;
            }  
        }
    }
}