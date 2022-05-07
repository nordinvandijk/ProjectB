using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;
using static System.Console;
using System.Globalization;
using System.Linq;

namespace CinemaApp
{
    class AvailableHall
    {
        public string HallName { get; set; }
        public List<MovieItem> MovieItemlist { get; set; }

        public void OrderMovieItems(){ // sorteert movie items in een hall op begintijd
            var cultureInfo = new CultureInfo("nl-NL");
            MovieItemlist = MovieItemlist.OrderBy(x => DateTime.Parse(x.StartTimeString, cultureInfo)).ToList();
        }
    }

    class Day
    {
        public string Date { get; set; }
        public List<AvailableHall> AvailableHalls { get; set; }
    }

    class Location
    {
        public string CinemaLocation { get; set; }
        public List<Day> Days { get; set; }
    }

    class FilmAgenda
    {
        public List<Location> locations = new List<Location>();
        public Application App;

        public FilmAgenda(Application app){
            App = app;
            LoadJson();
        }
        /// <summary>
        /// Deze functie maakt een 'movieItem' aan en voegt die toe aan een locatie -> datum -> bioscoopzaal volgens de meegegeven parameters
        /// </summary>
        /// <param name="locationIndex"></param>
        /// <param name="dayIndex"></param>
        /// <param name="hallIndex"></param>
        public void AddMovieItem(int locationIndex, int dayIndex, int hallIndex){
        	// 'cultureInfo' wordt gebruikt bij omzetten datum string naar DateTime
            var cultureInfo = new CultureInfo("nl-NL");
            DateTimeStyles styles = DateTimeStyles.None;

            //Declare start en end time
            DateTime startTime;
            DateTime endTime;

            // Kiezen film
            int amountOfMovies = App.movieManager.movies.Count;
            string[] movieOptions = new string[amountOfMovies];
            int i = 0;
            foreach(Movie movie in App.movieManager.movies)
            {
                movieOptions[i] = movie.Title;
                i++;
            }
            string titleChooseMovie = @"Kies een film";
            Menu chooseMovieMenu = new Menu(movieOptions, titleChooseMovie, 0);
            int chosenMovie = chooseMovieMenu.Run();

            // Kiezen format
            string titleChooseFormat = @"Kies een formar";
            string[] formatOptions = { "2D", "3D", "IMAX", "IMAX-3D", "4D" };
            Menu chooseFormatMenu = new Menu(formatOptions, titleChooseFormat, 0);
            string format = formatOptions[chooseFormatMenu.Run()];

            // Pakt de film duration in string vorm en zet het om naar TimeSpan
            string durationMovieString = App.movieManager.movies[chosenMovie].Duration;
            TimeSpan durationMovie = TimeSpan.Parse(durationMovieString);
            
            // Code voor het vinden van de start en end time
            bool startAndEndTimeFound = false;
            while (!startAndEndTimeFound)
            {
                // User input starttijd
                Console.Clear();
                CursorVisible = true;
                Console.WriteLine("Voer een startTijd in volgende het formaat: '00:00'");
                string startTimeString = Console.ReadLine();
                CursorVisible = false;

                // Pakt de datum in string vorm
                string dayString = App.filmAgenda.locations[locationIndex].Days[dayIndex].Date;

                // Probeert de datum en de starttijd samen te voegen en te parsen naar een DateTime
                bool startTimeParse = DateTime.TryParse($"{dayString} {startTimeString}", cultureInfo, styles, out startTime);

                // Als er een correcte begintijd is ingevuld word er automatisch een endTime aangemaakt met behulp van de TimeSpan 'durationMovie'
                if (startTimeParse)
                {
                    endTime = startTime + durationMovie;

                    //Checkt of de gekozen startTime en endTime niet overlappen met andere movieItems in dezelfde location->date->hall
                    bool overlapping = false;
                    foreach (MovieItem movieItem in locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].MovieItemlist)
                    {
                        var compareStartTime = DateTime.Parse(movieItem.StartTimeString, cultureInfo);
                        var compateEndTime = DateTime.Parse(movieItem.EndTimeString, cultureInfo);

                        if (App.time.CheckTimeOverlap(startTime, endTime, compareStartTime, compateEndTime))
                        {
                            overlapping = true;
                            WriteLine("Tijdens de ingvulde tijd speelt er al een film in deze zaal");
                            ConsoleUtils.WaitForKeyPress();
                            break;
                        }
                    }
                    if (!overlapping)
                    {
                        startAndEndTimeFound = true;
                    }

                    // Voegt movieItem toe zodra deze niet overlapt met andere movieItems
                    if (startAndEndTimeFound)
                    {
                        foreach (Movie movie in App.movieManager.movies)
                        {
                            if (movie.Title == App.movieManager.movies[chosenMovie].Title)
                            {
                                // Aanmaken
                                MovieItem movieItem = new MovieItem()
                                {
                                    Title = movie.Title,
                                    Description = movie.Description,
                                    ReleaseDate = movie.ReleaseDate,
                                    Genre = movie.Genre,
                                    MinimumAge = movie.MinimumAge,
                                    Kijkwijzer = movie.Kijkwijzer,
                                    Duration = movie.Duration,
                                    StartTimeString = startTime.ToString(cultureInfo),
                                    EndTimeString = endTime.ToString(cultureInfo),
                                    Format = format
                                };
                                // Toevoegen
                                locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].MovieItemlist.Add(movieItem);
                            }
                        }
                        UpdateJson();
                    }
                }
                else
                {
                    Console.WriteLine("This is not a correct time!");
                    ConsoleUtils.WaitForKeyPress();
                }
            }



            

            
        }

        public void LoadJson() 
        {
            using (StreamReader sr = new StreamReader("FilmAgenda.json"))
            {
                string json = sr.ReadToEnd();
                locations = JsonConvert.DeserializeObject<List<Location>>(json);
            }
        }

        public void UpdateJson() 
        {
           using (StreamWriter sw = new StreamWriter("FilmAgenda.json"))
            {   
                string json = JsonConvert.SerializeObject(locations,Formatting.Indented);
                sw.WriteLine(json);
            } 
        }
    }
}