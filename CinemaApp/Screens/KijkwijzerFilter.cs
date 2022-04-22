using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class KijkwijzerFilter : Screen
    {
        //Fields
        Movie movie;

        //Constructor
        public KijkwijzerFilter(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            foreach (Movie mov in App.movieManager.movies) {
                if (mov.Title == App.kijkwijzerFilmFilter.ChosenFilter) {
                    movie = mov;
                    break;
                }
            }
            string titel = movie.Title + "\nBeschrijving: " + movie.Description + "\nRelease: " + movie.ReleaseDate + 
                           "\nGenre: " + string.Join(", ", movie.Genre) + "\nKijkwijzer: " + movie.MinimumAge + 
                           (movie.Kijkwijzer.Length != 0 ? ", " : "") + string.Join(", ", movie.Kijkwijzer);

            string[] options = {"Terug"};
            Menu FilmInfoMenu = new Menu(options, titel, 0);
            int ChosenOption = FilmInfoMenu.Run();
            //t
            switch(ChosenOption)
            {
                case 0:
                    App.filmOverviewScreen.run();
                    break;      
            }
        }
    }   
}