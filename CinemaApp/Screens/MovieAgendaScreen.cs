using System;
using static System.Console;
using CinemaApp;

namespace CinemaApp.Screens
{
    class MovieAgendaScreen : Screen
    {
        //Fields

        //Constructor
        public MovieAgendaScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string chosenLocation = null;
            string chosenDate = null;

            while (chosenLocation == null){
                Clear();
                int i = 0;
                foreach(Location location in App.filmAgenda.locations){
                    i++;
                }
                string[] options = new string[i];
                i = 0;
                foreach(Location location in App.filmAgenda.locations){
                    options[i] = location.CinemaLocation;
                    i++;
                }
                string titel = @"Kies een locatie";
                Menu ChooseCinema = new Menu(options, titel, 0);
                chosenLocation = App.filmAgenda.locations[ChooseCinema.Run()].CinemaLocation;
            }

            while (chosenDate == null){
                Clear();
                CursorVisible = true;
                WriteLine("Voer een datum in volgens het formaat: 00/00/0000");
                chosenDate = ReadLine();
                CursorVisible = false;
            }

            

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}