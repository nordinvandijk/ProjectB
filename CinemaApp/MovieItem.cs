using System;
using static System.Console;

namespace CinemaApp
{
    class MovieItem
    {
        public string StartTimeString {get; set;}
        public string EndTimeString {get; set;}
        public string EndTimeWithCleaning { get; set;}
        public string Format {get; set;}
        public string Duration {get; set;}
        public string Title {get; set;}
        public string LocationName { get; set;}
        public Seat[][] Seats {get; set;}
    }
}