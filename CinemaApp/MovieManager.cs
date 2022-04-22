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
        public void AddMovie(string title, string desc, string releaseDate, string[] genre, int minAge, string[] kijkwijzer) 
        {
            Movie mov = new Movie() {
                Title = title,
                Description = desc,
                ReleaseDate = releaseDate,
                Genre = genre,
                MinimumAge = minAge,
                Kijkwijzer = kijkwijzer
            };
            movies.Add(mov);
            UpdateJson();
            WriteLine("Film toegevoegd");
        }

        public void InputAddmovie(){ // Maybe beter om deze method weg te halen en in de screen code te zetten, kan wrs ook nog wat worden veranderd aan opmaak van de input zinnen.
            Clear();
            Write("Enter title: ");
            string title = ReadLine();
            Write("Enter desc: ");
            string desc = ReadLine();
            Write("Enter releaseDate: ");
            string releaseDate = ReadLine();
            Write("How many genres does the movie have?");
            string stringlenGenre = ReadLine();
            int lenGenre= Int32.Parse(stringlenGenre);
            string[] genre = new string[lenGenre];
            string inputGenre = "";
            for(int i = 0; i<lenGenre; i++){
                Write($"What is genre number {i+1}");
                inputGenre = ReadLine();
                genre[i] = title;
            }
            Write("What is the minimum age of the movie?");
            string stringMinAge = ReadLine();
            int MinAge= Int32.Parse(stringMinAge); 
            Write("Hoeveel kijkwijzers wil je toevoegen?");
            string stringlenKijkwijzer = ReadLine();
            int lenkijkwijzer= Int32.Parse(stringlenKijkwijzer);
            string[] kijkwijzer = new string[lenGenre];           
            string inputKijkwijzer = "";
            for(int i = 0; i<lenkijkwijzer; i++){
                Write($"What is kijkwijzer number {i+1}");
                inputKijkwijzer = ReadLine();
                genre[i] = title;
            }
            AddMovie(title,desc,releaseDate,genre,MinAge,kijkwijzer);
        }
        public void InputRemoveMovie(){ // Maybe beter om deze method weg te halen en in de screen code te zetten
            Clear();
            CursorVisible = true;
            Write("Enter title: ");
            string title = ReadLine(); 
            CursorVisible = false;
            RemoveMovie(title);  
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
    }
}