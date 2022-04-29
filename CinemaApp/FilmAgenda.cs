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

        public void OrderMovieItems(){
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

        public void AddMovieItem(int locationIndex, int dayIndex, int hallIndex, string title, string startTimeString, string endTimeString, string format){
        	var cultureInfo = new CultureInfo("nl-NL");
            var startTime = DateTime.Parse(startTimeString, cultureInfo); //Making startTime and endTime a DateTime var instead of a string
            var endTime = DateTime.Parse(endTimeString, cultureInfo);

            bool overlapping = false;
            foreach (MovieItem movieItem in locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].MovieItemlist){ //iterating over every movieItem for a given location date and hall
                
                var compareStartTime = DateTime.Parse(movieItem.StartTimeString, cultureInfo);
                var compateEndTime = DateTime.Parse(movieItem.EndTimeString, cultureInfo);

                if(App.time.CheckTimeOverlap(startTime, endTime, compareStartTime, compateEndTime)){
                    overlapping = true;
                    WriteLine("Tijdens de ingvulde tijd speelt er al een film in deze zaal");
                    ConsoleUtils.WaitForKeyPress();
                    break;
                }
            }
            if (!overlapping){ // adds movie to location-date-hall if there isnt playing a movie during the given time
                foreach(Movie movie in App.movieManager.movies){
                    if (movie.Title == title){
                        MovieItem movieItem = new MovieItem(){
                            Title = movie.Title,
                            Description = movie.Description,
                            ReleaseDate = movie.ReleaseDate,
                            Genre = movie.Genre,
                            MinimumAge = movie.MinimumAge,
                            Kijkwijzer = movie.Kijkwijzer,
                            StartTimeString = startTimeString,
                            EndTimeString = endTimeString,
                            Format = format
                        };
                    }
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