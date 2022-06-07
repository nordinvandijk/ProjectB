using System;
using System.Collections.Generic;
using static System.Console;
using static CinemaApp.ConsoleUtils;

namespace CinemaApp.Screens
{
    class FilmInfoScreen : Screen
    {
        //Fields
        private Movie movie;
        private int selectedMovieItem;
        private List<MovieItem> options = new List<MovieItem>();
        public MovieItem chosenMovieItem;

        //Constructor
        public FilmInfoScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public void Display()
        {
            // Vind de gekozen movie en slaat die op in 'movie'
            foreach (Movie mov in App.movieManager.movies)
            {
                if (mov.Title == App.filmOverviewScreen.ChosenMovie)
                {
                    movie = mov;
                    break;
                }
            }
            // Displayt informatie over de film
            Console.WriteLine("--------------------\n" + movie.Title + "\n--------------------\n" + "\nBeschrijving: " + movie.Description + "\nReleaseDatum: " + movie.ReleaseDate +
                           "\nGenre: " + string.Join(", ", movie.Genre) + "\nKijkwijzer: " + movie.MinimumAge +
                           (movie.Kijkwijzer.Length != 0 ? ", " : "") + string.Join(", ", movie.Kijkwijzer) + "\n");

            // Deze integer wordt met 1 verhoogd elke keer voordat er een movieItem wordt gedisplayt, hierdoor kan het geselecteerde movieItem anders gedisplayt worden
            int numberMovieItem = 0;

            // Iterate over alle locaties en displayt ze
            foreach (Location location in App.filmAgenda.locations)
            {
                // Displayt alle locaties
                Console.WriteLine($"--------------------\n{location.CinemaLocation}\n--------------------");

                // Iterate over alle dagen
                foreach (Day day in location.Days)
                {
                    // Als de dag later of gelijk is aan vandaag
                    if (DateTime.Parse(day.Date) >= DateTime.Today)
                    {
                        // Displayt de dagen vanaf vandaag
                        Console.WriteLine(day.Date);

                        // Iterate over alle bioscoopzalen
                        foreach (AvailableHall hall in day.AvailableHalls)
                        {
                            // Iterate over alle movieItems
                            foreach (MovieItem movieItem in hall.MovieItemlist)
                            {
                                // Als een movieItem dezelfde titel heeft als de gekozen film wordt hij gedisplayt
                                if (movieItem.Title == App.filmOverviewScreen.ChosenMovie)
                                {
                                    Console.Write($"    ");
                                    if (selectedMovieItem == numberMovieItem)
                                    {
                                        ForegroundColor = ConsoleColor.Black;
                                        BackgroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        ForegroundColor = ConsoleColor.White;
                                        BackgroundColor = ConsoleColor.Black;
                                    }
                                    
                                    Console.Write($"<{movieItem.Title} | Formaat: {movieItem.Format} | Tijd: {movieItem.StartTimeString.Substring(11)} - {movieItem.EndTimeString.Substring(11)} >");
                                    ResetColor();
                                    Console.Write("\n");
                                    numberMovieItem++;
                                }
                            }
                        }
                    }
                }
            }
            
            // Display 'terug' button
            if (selectedMovieItem == numberMovieItem)
            {
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
            }
            else
            {
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine("\nTerug");
            ResetColor();
        }
        public override void run()
        {
            options.Clear();
            int amountOfMovieItems = 0;
            selectedMovieItem = 0;

            // Finds the amount of moviesItems from the chosen movie that play the upcoming 7 days
            foreach (Location location in App.filmAgenda.locations)
            {
                foreach (Day day in location.Days)
                {
                    if (DateTime.Parse(day.Date) >= DateTime.Today)
                    {
                        foreach (AvailableHall hall in day.AvailableHalls)
                        {

                            foreach (MovieItem movieItem in hall.MovieItemlist)
                            {
                                if (movieItem.Title == App.filmOverviewScreen.ChosenMovie)
                                {
                                    amountOfMovieItems++;
                                    options.Add(movieItem);
                                }
                            }
                        }
                    }
                }
            }

            ConsoleKey keyPressed;
            do
            {
                Clear();
                Display();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.W)
                {
                    selectedMovieItem--;
                    if (selectedMovieItem < 0)
                    {
                        selectedMovieItem = amountOfMovieItems;
                    }
                }
                if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
                {
                    selectedMovieItem++;
                    if (selectedMovieItem >= amountOfMovieItems + 1)
                    {
                        selectedMovieItem = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            // If the 'Terug' button is clicked
            if (selectedMovieItem == amountOfMovieItems)
            {
                App.filmOverviewScreen.run();
            }
            else
            {
                chosenMovieItem = options[selectedMovieItem];
                App.seatsOverviewScreen.run();
            }
        }
    }   
}