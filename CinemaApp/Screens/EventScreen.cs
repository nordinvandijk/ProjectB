using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class EventScreen : Screen
    {
        //Fields
        public string ChosenEvent;
        //Constructor
        public EventScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"Evenementen";

            //bepaalt de lengte van de eventList plus een om terug te gaan.
            int length = App.eventManager.events.Count + 1;
            string[] options = new string[length];
            int i=0;
            //gaat door alle events om deze te displayen
            foreach(Event ev in App.eventManager.events) {
                options[i] = ev.Name;
                i++;
            }

            options[length-1] = "\nTerug";
            
            Menu EventMenu = new Menu(options, titel, 0);
            int ChosenOption = EventMenu.Run();
            ChosenEvent = options.GetValue(ChosenOption).ToString();

            if (ChosenOption == options.GetLength(0)-1) {
                App.homeScreen.run(); //terug gaan naar homescreen
            }
            else{
                App.eventInfoScreen.run();
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}