using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using static System.Console;
using System;

namespace CinemaApp
{
    class MovieManager
    {
        public List<Movie> movies = new List<Movie>();
        
        public MovieManager() {
            LoadJson(); // zorgt ervoor dat de list "movies" direct na het runnen gevuld wordt met de movies van de json
        }

        /// <summary>
        /// This method will first make a new Movie object with all the required parameters you need to pass in. Then it adds the Movie object to the List<Movie> called "movies"
        /// and at last it will update the json file called "movieList" by using UpdateJson()
        /// </summary>
        /// <param name="title">Tile of the movie you want to add.</param>
        /// <param name="desc">Description of the movie you want to add.</param>
        /// <param name="releaseDate">Release date of the movie you want to add.</param>
        /// <param name="genre">Genre of the movie you want to add.</param>
        /// <param name="minAge">Minimum age of the movie you want to add.</param>
        /// <param name="kijkwijzer">The dutch "kijkwijzer" of the movie you want to add.</param>
        public void AddMovie(string title, string desc, string releaseDate, string[] genre, int minAge, string[] kijkwijzer, string duration) 
        {
            Movie mov = new Movie() {
                Title = title,
                Description = desc,
                ReleaseDate = releaseDate,
                Genre = genre,
                MinimumAge = minAge,
                Kijkwijzer = kijkwijzer,
                Duration = duration
            };
            movies.Add(mov);
            UpdateJson();
            WriteLine("Film toegevoegd");
        }
 
        /// <summary>
        /// Removes a movie based on title from the List<Movie> called "movies".
        /// </summary>
        /// <param name="title">The title of the movie you want to delete.</param>
        public void RemoveMovie(string title) {
            foreach (Movie mov in movies.ToList()) { // Mischien handig om hier een reverse for loop te gebruiken ipv de list tijdelijk te kopieeren met ToList().
                if (mov.Title == title) {
                    movies.Remove(mov);
                    UpdateJson();
                    WriteLine("Film bestaat in het systeem en is verwijderd");
                    return;   
                }
            }
            WriteLine("Film staat niet in het systeem");
        } 

        /// <summary>
        /// Loads all json objects from "movieList.json" in. Converts the json objects to Movie.cs objects and those Movie objects get stored in the List<Movie> called "movies".
        /// </summary>
        public void LoadJson() 
        {
            using (StreamReader sr = new StreamReader("movieList.json"))
            {
                string json = sr.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        /// <summary>
        /// Updates the file "movieList.json" by writing all the Movie classes inside List<Movie> called "movies" to "movieList.json".
        /// </summary>
        public void UpdateJson() 
        {
           using (StreamWriter sw = new StreamWriter("movieList.json"))
            {   
                string json = JsonConvert.SerializeObject(movies,Formatting.Indented);
                sw.WriteLine(json);
            } 
        }

        /// <summary>
        /// Deze functie is om dingen te testen
        /// </summary>
        public void UpdateAllMovies()
        {
            foreach(Movie movie in this.movies)
            {
                WriteLine(movie.Duration);
                movie.Duration = "pipi";
            }
            UpdateJson();
            ConsoleUtils.WaitForKeyPress();
        }
    }
}