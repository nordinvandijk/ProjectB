using System;
using static System.Console;

namespace CinemaApp
{
    class MovieItem : Movie
    {
        public string StartTimeString {get; set;}
        public string EndTimeString {get; set;}
        public string Format {get; set;}
    }
}