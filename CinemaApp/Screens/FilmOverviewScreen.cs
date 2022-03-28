using System;
using static System.Console;
using System.Collections.Generic;

namespace CinemaApp.Screens
{
    class FilmOverviewScreen : Screen
    {
        //Fields
        public string ChosenMovie;
        //Constructor
        public FilmOverviewScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"Film Overview";
            
            List<string> optionsList = new List<string>();
            foreach (Movie mov in App.movieManager.movies) {
                optionsList.Add(mov.Title);
            }
            
            optionsList.Add("\nTerug");

            string[] options = optionsList.ToArray();
            Menu FilmOverviewMenu = new Menu(options, titel, 0);
            int ChosenOption = FilmOverviewMenu.Run();
            ChosenMovie = options.GetValue(ChosenOption).ToString();
            
            // Checkt of de terug button is geklikt, zo wel dan gaat het programma terug naar het Hoofd Scherm, anders gaat het programma door naar het Film Info Scherm.
            if (ChosenOption == options.GetLength(0)-1) {
                App.homeScreen.run();
            }
            else {
                App.filmInfoScreen.run();
            }
        }
    }   
}