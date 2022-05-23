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
            foreach (Event ev in App.eventManager.events) {
                if (ev.Name == App.eventScreen.ChosenEvent) {
                    Event = ev;
                    break;
                }
            }

            string naam = Event.Name + "\nBeschrijving: " + Event.Description +  
        }
    }
}