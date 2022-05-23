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

            List<string> optionsList = new List<string>();
            foreach(Event ev in App.eventManager.events) {
                optionsList.Add(ev.Name);
            }

            optionsList.Add("\nTerug")
            
            string[] options = optionsList.ToArray();
            Menu EventMenu = new Menu(options, titel, 0);
            int ChosenOption = EventMenu.Run();
            ChosenEvent = options.GetValue(ChosenOption).ToString();

            if (ChosenOption == options.Length-1) {
                App.homeScreen.run();
            }
            else{
                App.eventInfoScreen.run();
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}