using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using static System.Console;

namespace CinemaApp
{
    class MovieManager
    {
        public List<Movie> movies = new List<Movie>();

        public MovieManager() {
            LoadJson(); // zorgt ervoor dat de list "movies" direct na het runnen gevuld wordt met de movies van de json
        }

        public void AddMovie(string title, string desc, string releaseDate, string[] genre, int minAge, string[] kijkwijzer) 
        {
            // Maakt een movie object aan met de meegegeven arguments van "AddMovie"
            Movie mov = new Movie() {
                Title = title,
                Description = desc,
                ReleaseDate = releaseDate,
                Genre = genre,
                MinimumAge = minAge,
                Kijkwijzer = kijkwijzer
            };
            // <<<

            // Voegt het gemaakt object toe aan de "Movie" list
            movies.Add(mov);
            
            // Deze blok code zorgt ervoor dat de nieuw toegevoegde film ook direct in het json bestand wordt gestopt
            using (StreamWriter sw = new StreamWriter("movieList.json"))
            {   
                string json = JsonConvert.SerializeObject(movies,Formatting.Indented);
                sw.WriteLine(json);
            }
            // <<<
        }


        public void LoadJson() 
        {
            using (StreamReader sr = new StreamReader("movieList.json"))
            {
                string json = sr.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }
    }
}