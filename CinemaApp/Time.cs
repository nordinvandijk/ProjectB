using System;
using System.Globalization;
using static System.Console;
using System.Collections.Generic;

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

            //Deze for loop loopt 7 keer en veranderd steeds de dayToAdd, hierdoor kunnen de komende 7 dagen toegevoegd worden aan de filmAgenda
            DateTime dayToAdd = thisDay + new TimeSpan(24, 0, 0);
            for (int i = 0; i < 7; i++)
            {
                //Loopt door alle locaties om de dayToAdd toe te voegen als die nog niet bestaat
                for (int j = 0; j < App.filmAgenda.locations.Count; j++)
                {
                    //Als de dayToAdd nog niet bestaat in de Days list van een bepaalde locatie wordt die day aangemaakt
                    if (!App.filmAgenda.locations[j].Days.Exists(x => DateTime.Parse(x.Date, cultureInfo) == dayToAdd))
                    {
                        App.filmAgenda.locations[j].Days.Add(new Day()
                        {
                            Date = dayToAdd.ToString("dd-MM-yyyy",cultureInfo),
                            AvailableHalls = new List<AvailableHall>()
                        {
                        new AvailableHall(){HallName = "Grote Zaal", MovieItemlist = new List<MovieItem>()},
                        new AvailableHall(){HallName = "Gemiddelde Grote Zaal", MovieItemlist = new List<MovieItem>()},
                        new AvailableHall(){HallName = "Kleine Zaal", MovieItemlist = new List<MovieItem>()}
                        }
                        });
                    }
                    // Ordert de days in de filmAgenda
                    App.filmAgenda.locations[j].OrderDays();
                }
                dayToAdd += new TimeSpan(24, 0, 0);
            }
            //Code voor het verwijderen van dagen uit de film agenda die 7 dagen ouder zijn als de huidige datum
            for (int i = 0; i < App.filmAgenda.locations.Count; i++)
            {
                for (int j = 0; j < App.filmAgenda.locations[i].Days.Count; j++)
                {
                    DateTime dayToCheck = DateTime.Parse(App.filmAgenda.locations[i].Days[j].Date, cultureInfo);
                    if (dayToCheck < thisDay - new TimeSpan(7, 0, 0, 0))
                    {
                        App.filmAgenda.locations[i].Days.RemoveAt(j);
                    }
                }
            }
        //Updated json file met alle nieuwe datums
        App.filmAgenda.UpdateJson();
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