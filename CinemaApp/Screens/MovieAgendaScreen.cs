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
            int chosenLocation = -1;
            int chosenDate = -1;
            int chosenHall = -1;

            while (chosenLocation == -1){
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
                chosenLocation = ChooseCinema.Run();
            }

            while (chosenDate == -1){
                Clear();
                CursorVisible = true;
                WriteLine("Voer een datum in volgens het formaat: 00-00-0000");
                string input = ReadLine();
                CursorVisible = false;
                int i = 0;
                foreach(Day day in App.filmAgenda.locations[chosenLocation].Days){
                    if(day.Date == input){
                        chosenDate = i;
                    }
                    i++;
                }
                if(chosenDate == -1){
                    Clear();
                    WriteLine("Deze datum bestaat niet in het systeem");
                    ConsoleUtils.WaitForKeyPress();
                }
            }

            while (chosenHall == -1){
                int i = 0;
                foreach(AvailableHall hall in App.filmAgenda.locations[chosenLocation].Days[chosenDate].AvailableHalls){
                    i++;
                }
                string[] options = new string[i];
                i = 0;
                foreach(AvailableHall hall in App.filmAgenda.locations[chosenLocation].Days[chosenDate].AvailableHalls){
                    options[i] = hall.HallName;
                    i++;
                }
                string titel = "Kies een zaal";
                Menu ChooseHall = new Menu(options, titel, 0);
                chosenHall = ChooseHall.Run();
            }

            WriteLine($"{chosenLocation},{chosenDate},{chosenHall}");

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}