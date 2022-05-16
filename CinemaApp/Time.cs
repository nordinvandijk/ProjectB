using System;
using System.Globalization;
using static System.Console;

namespace CinemaApp
{
    class Time
    {
        public int hours;
        public int minutes;
        public Application App;

        public Time(Application app){
            App = app;
        }

        public void UpdateAgenda()
        {
            // Huidige dag in 'this day'
            var cultureInfo = new CultureInfo("nl-NL");
            DateTime thisDay = DateTime.Today;

            for (int i = 0; i < App.filmAgenda.locations.Count; i++)
            {
                Location location = App.filmAgenda.locations[i];
                
            }

            string test = thisDay.ToString(cultureInfo);
            test = test.Substring(0, 8);
            Console.WriteLine(test);
        }
        public bool CheckTimeOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2){
            if (start1 >= start2 && start1 <= end2){
                return true;
            }
            else if (end1 >= start2 && end1 <= end2){
                return true;
            }
            else if (start2 >= start1 && start2 <= end1){
                return true;
            }
            else if (end2 >= start1 && end2 <= end1){
                return true;
            }
            else {
                return false;
            }
        }
    }
}