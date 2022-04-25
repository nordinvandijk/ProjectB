using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;
using static System.Console;

namespace CinemaApp
{
    class AvailableHall
    {
        public string HallName { get; set; }
        public List<MovieItem> MovieItemlist { get; set; }
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

        public FilmAgenda(){
            LoadJson();
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