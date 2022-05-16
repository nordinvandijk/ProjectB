using System;
using static System.Console;

namespace CinemaApp
{
    class Movie
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public string[] Genre {get; set; }
        public int MinimumAge { get; set; }
        public string[] Kijkwijzer { get; set; }
        public string Duration { get; set; }
    }
}