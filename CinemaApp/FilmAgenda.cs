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

        public void OrderMovieItems(){ // sorteert movie items in een hall op begintijd
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

        public void OrderDays()
        { // sorteert movie items in een hall op begintijd
            var cultureInfo = new CultureInfo("nl-NL");
            Days = Days.OrderBy(x => DateTime.Parse(x.Date, cultureInfo)).ToList();
        }
    }

    class FilmAgenda
    {
        public List<Location> locations = new List<Location>();
        public Application App;

        public FilmAgenda(Application app){
            App = app;
            LoadJson();
        }
        public void ClearMovieItems()
        {
            for (int i = 0; i < locations.Count; i++)
            {
                for (int j = 0; j < locations[i].Days.Count; j++)
                {
                    for (int k = 0; k < locations[i].Days[j].AvailableHalls.Count; k++)
                    {
                        locations[i].Days[j].AvailableHalls[k].MovieItemlist.Clear();
                    }
                }
            }
        }

        public void ClearDays()
        {
            for (int i = 0; i < locations.Count; i++)
            {
                locations[i].Days.Clear();
            }
        }

        /// <summary>
        /// Deze functie maakt een 'movieItem' aan en voegt die toe aan een locatie -> datum -> bioscoopzaal volgens de meegegeven parameters
        /// </summary>
        /// <param name="locationIndex"></param>
        /// <param name="dayIndex"></param>
        /// <param name="hallIndex"></param>
        public void AddMovieItem(int locationIndex, int dayIndex, int hallIndex){
        	// 'cultureInfo' wordt gebruikt bij omzetten datum string naar DateTime
            var cultureInfo = new CultureInfo("nl-NL");
            DateTimeStyles styles = DateTimeStyles.None;

            //Declare start en end time
            DateTime startTime;
            DateTime endTime;

            // Kiezen film
            int amountOfMovies = App.movieManager.movies.Count;
            string[] movieOptions = new string[amountOfMovies];
            int i = 0;
            foreach(Movie movie in App.movieManager.movies)
            {
                movieOptions[i] = movie.Title;
                i++;
            }
            string titleChooseMovie = @"Kies een film";
            Menu chooseMovieMenu = new Menu(movieOptions, titleChooseMovie, 0);
            int chosenMovie = chooseMovieMenu.Run();

            // Kiezen format
            string titleChooseFormat = @"Kies een uitvoering";
            string[] formatOptions = { "2D", "3D", "IMAX", "IMAX-3D", "4D" };
            Menu chooseFormatMenu = new Menu(formatOptions, titleChooseFormat, 0);
            string format = formatOptions[chooseFormatMenu.Run()];

            // Pakt de film duration in string vorm en zet het om naar TimeSpan
            string durationMovieString = App.movieManager.movies[chosenMovie].Duration;
            TimeSpan durationMovie = TimeSpan.Parse(durationMovieString);
            
            // Code voor het vinden van de start en end time
            bool startAndEndTimeFound = false;
            while (!startAndEndTimeFound)
            {
                // User input starttijd
                Console.Clear();
                CursorVisible = true;
                Console.WriteLine("Voer een startTijd in volgens de volgende notatie: '00:00'");
                string startTimeString = Console.ReadLine();
                CursorVisible = false;

                // Pakt de datum in string vorm
                string dayString = App.filmAgenda.locations[locationIndex].Days[dayIndex].Date;

                // Probeert de datum en de starttijd samen te voegen en te parsen naar een DateTime
                bool startTimeParse = DateTime.TryParse($"{dayString} {startTimeString}", cultureInfo, styles, out startTime);

                // Als er een correcte begintijd is ingevuld word er automatisch een endTime aangemaakt met behulp van de TimeSpan 'durationMovie'
                if (startTimeParse)
                {
                    endTime = startTime + durationMovie;
                    DateTime endTimeWithCleaning = endTime + new TimeSpan(0,30,0);

                    //Checkt of de gekozen startTime en endTime niet overlappen met andere movieItems in dezelfde location->date->hall
                    bool overlapping = false;
                    foreach (MovieItem movieItem in locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].MovieItemlist)
                    {
                        var compareStartTime = DateTime.Parse(movieItem.StartTimeString, cultureInfo);
                        var compareEndTimeWithCleaning = DateTime.Parse(movieItem.EndTimeWithCleaning, cultureInfo);

                        if (App.time.CheckTimeOverlap(startTime, endTimeWithCleaning, compareStartTime, compareEndTimeWithCleaning))
                        {
                            overlapping = true;
                            WriteLine("Tijdens de ingvulde tijd speelt er al een film in deze zaal");
                            ConsoleUtils.WaitForKeyPress();
                            break;
                        }
                    }

                    //Checkt of de gekozen start- en endTime tijdens de openingstijd van de bios vallen, 09:00 tot 02:00, elke film dient voor 12 uur s'nachts te starten
                    bool betweenOpeningAndClosingTime = false;
                    if (startTime >= DateTime.Parse($"{dayString} 09:00") && startTime <= DateTime.Parse($"{dayString} 02:00") + new TimeSpan(24,00,00) && endTime >= DateTime.Parse($"{dayString} 09:00") && endTime <= DateTime.Parse($"{dayString} 02:00") + new TimeSpan(24,00,00))
                    {
                        betweenOpeningAndClosingTime = true;
                    }

                    if (!betweenOpeningAndClosingTime)
                    {
                        WriteLine("De bioscoop opent om 9:00 en sluit om 02:00, en er kunnen geen films starten na 12 uur s'nachts\n Vul een startTijd in die voldoet aan deze eisen!");
                        ConsoleUtils.WaitForKeyPress();
                    }

                    if (!overlapping && betweenOpeningAndClosingTime)
                    {
                        startAndEndTimeFound = true;
                    }

                    // Voegt movieItem toe zodra deze niet overlapt met andere movieItems
                    if (startAndEndTimeFound)
                    {
                        //Maken van juiste grootte Seat jagged array
                        int sizeHall = 5;
                        float price = 15.0f;

                        // Bepalen hoeveel stoelen aan de hand van zaal grote
                        if (locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].HallName == "Grote Zaal")
                        {
                            sizeHall = 20;
                        }
                        else if (locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].HallName == "Gemiddelde Grote Zaal")
                        {
                            sizeHall = 14;
                        }
                        else if (locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].HallName == "Kleine Zaal")
                        {
                            sizeHall = 10;
                        }

                        //Prijs van de stoelen bepalen
                        if (format == "2D") { price = 12.0f; }
                        if (format == "3D") { price = 15.0f; }
                        if (format == "IMAX") { price = 15.0f; }
                        if (format == "IMAX-3D") { price = 18.0f; }
                        if (format == "4D") { price = 22.0f; }

                        // Seat jagged array aanmaken
                        Seat[][] seatJarr = new Seat[sizeHall][];
                        for (int j = 0; j < seatJarr.Length; j++)
                        {
                            seatJarr[j] = new Seat[sizeHall];
                            for (int k = 0; k < seatJarr[j].Length; k++)
                            {
                                seatJarr[j][k] = new Seat(price, j, k);
                            }
                        }

                        // Aanmaken
                        MovieItem movieItem = new MovieItem()
                        {
                            Title = App.movieManager.movies[chosenMovie].Title,
                            Duration = App.movieManager.movies[chosenMovie].Duration,
                            StartTimeString = startTime.ToString("dd-MM-yyyy HH:mm", cultureInfo),
                            EndTimeString = endTime.ToString("dd-MM-yyyy HH:mm", cultureInfo),
                            EndTimeWithCleaning = endTimeWithCleaning.ToString("dd-MM-yyyy HH:mm", cultureInfo),
                            Format = format,
                            LocationName = App.filmAgenda.locations[locationIndex].CinemaLocation,
                            IsEvent = false,
                            Seats = seatJarr
                        };
                        // Toevoegen
                        locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].MovieItemlist.Add(movieItem);
                         
                        UpdateJson();
                    }
                }
                else
                {
                    Console.WriteLine("Dit is geen juist tijd!");
                    ConsoleUtils.WaitForKeyPress();
                }
            }
        }

        public void AddMovieItem(int locationIndex, int dayIndex, int hallIndex, bool isEvent)
        {
            // 'cultureInfo' wordt gebruikt bij omzetten datum string naar DateTime
            var cultureInfo = new CultureInfo("nl-NL");
            DateTimeStyles styles = DateTimeStyles.None;

            //Declare start en end time
            DateTime startTime;
            DateTime endTime;

            // Kiezen Evenement
            int amountOfEvents = App.eventManager.events.Count;
            string[] movieOptions = new string[amountOfEvents];
            int i = 0;
            foreach (Event eventt in App.eventManager.events)
            {
                movieOptions[i] = eventt.Name;
                i++;
            }
            string titleChooseEvent = @"Kies een evenement";
            Menu chooseEventMenu = new Menu(movieOptions, titleChooseEvent, 0);
            int chosenEvent = chooseEventMenu.Run();

            // Kiezen format
            string titleChooseFormat = @"Kies een uitvoering";
            string[] formatOptions = { "2D", "3D", "IMAX", "IMAX-3D", "4D" };
            Menu chooseFormatMenu = new Menu(formatOptions, titleChooseFormat, 0);
            string format = formatOptions[chooseFormatMenu.Run()];

            // Pakt de film duration in string vorm en zet het om naar TimeSpan
            string durationMovieString = App.eventManager.events[chosenEvent].Duration;
            TimeSpan durationMovie = TimeSpan.Parse(durationMovieString);

            // Code voor het vinden van de start en end time
            bool startAndEndTimeFound = false;
            while (!startAndEndTimeFound)
            {
                // User input starttijd
                Console.Clear();
                CursorVisible = true;
                Console.WriteLine("Voer een startTijd in volgende het formaat: '00:00'");
                string startTimeString = Console.ReadLine();
                CursorVisible = false;

                // Pakt de datum in string vorm
                string dayString = App.filmAgenda.locations[locationIndex].Days[dayIndex].Date;

                // Probeert de datum en de starttijd samen te voegen en te parsen naar een DateTime
                bool startTimeParse = DateTime.TryParse($"{dayString} {startTimeString}", cultureInfo, styles, out startTime);

                // Als er een correcte begintijd is ingevuld word er automatisch een endTime aangemaakt met behulp van de TimeSpan 'durationMovie'
                if (startTimeParse)
                {
                    endTime = startTime + durationMovie;
                    DateTime endTimeWithCleaning = endTime + new TimeSpan(0, 30, 0);

                    //Checkt of de gekozen startTime en endTime niet overlappen met andere movieItems in dezelfde location->date->hall
                    bool overlapping = false;
                    foreach (MovieItem movieItem in locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].MovieItemlist)
                    {
                        var compareStartTime = DateTime.Parse(movieItem.StartTimeString, cultureInfo);
                        var compareEndTimeWithCleaning = DateTime.Parse(movieItem.EndTimeWithCleaning, cultureInfo);

                        if (App.time.CheckTimeOverlap(startTime, endTimeWithCleaning, compareStartTime, compareEndTimeWithCleaning))
                        {
                            overlapping = true;
                            WriteLine("Tijdens de ingvulde tijd speelt er al een film in deze zaal");
                            ConsoleUtils.WaitForKeyPress();
                            break;
                        }
                    }

                    //Checkt of de gekozen start- en endTime tijdens de openingstijd van de bios vallen, 09:00 tot 02:00, elke film dient voor 12 uur s'nachts te starten
                    bool betweenOpeningAndClosingTime = false;
                    if (startTime >= DateTime.Parse($"{dayString} 09:00") && startTime <= DateTime.Parse($"{dayString} 02:00") + new TimeSpan(24, 00, 00) && endTime >= DateTime.Parse($"{dayString} 09:00") && endTime <= DateTime.Parse($"{dayString} 02:00") + new TimeSpan(24, 00, 00))
                    {
                        betweenOpeningAndClosingTime = true;
                    }

                    if (!betweenOpeningAndClosingTime)
                    {
                        WriteLine("De bioscoop opent om 9:00 en sluit om 02:00, en er kunnen geen films starten na 12 uur s'nachts\n Vul een startTijd in die voldoet aan deze eisen!");
                        ConsoleUtils.WaitForKeyPress();
                    }

                    if (!overlapping && betweenOpeningAndClosingTime)
                    {
                        startAndEndTimeFound = true;
                    }

                    // Voegt movieItem toe zodra deze niet overlapt met andere movieItems
                    if (startAndEndTimeFound)
                    {
                        //Maken van juiste grootte Seat jagged array
                        int sizeHall = 5;

                        //De prijs van een event is vastgesteld bij het aanmaken van een event
                        var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                        ci.NumberFormat.NumberDecimalSeparator = ",";
                        float price = (float)Double.Parse(App.eventManager.events[chosenEvent].TicketPrice, ci);

                        // Bepalen hoeveel stoelen aan de hand van zaal grote
                        if (locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].HallName == "Grote Zaal")
                        {
                            sizeHall = 20;
                        }
                        else if (locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].HallName == "Gemiddelde Grote Zaal")
                        {
                            sizeHall = 14;
                        }
                        else if (locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].HallName == "Kleine Zaal")
                        {
                            sizeHall = 10;
                        }

                        // Seat jagged array aanmaken
                        Seat[][] seatJarr = new Seat[sizeHall][];
                        for (int j = 0; j < seatJarr.Length; j++)
                        {
                            seatJarr[j] = new Seat[sizeHall];
                            for (int k = 0; k < seatJarr[j].Length; k++)
                            {
                                seatJarr[j][k] = new Seat(price, j, k);
                            }
                        }

                        // Aanmaken
                        MovieItem movieItem = new MovieItem()
                        {
                            Title = App.eventManager.events[chosenEvent].Name,
                            Duration = App.eventManager.events[chosenEvent].Duration,
                            StartTimeString = startTime.ToString("dd-MM-yyyy HH:mm", cultureInfo),
                            EndTimeString = endTime.ToString("dd-MM-yyyy HH:mm", cultureInfo),
                            EndTimeWithCleaning = endTimeWithCleaning.ToString("dd-MM-yyyy HH:mm", cultureInfo),
                            Format = format,
                            LocationName = App.filmAgenda.locations[locationIndex].CinemaLocation,
                            IsEvent = true,
                            Seats = seatJarr
                        };
                        // Toevoegen
                        locations[locationIndex].Days[dayIndex].AvailableHalls[hallIndex].MovieItemlist.Add(movieItem);

                        UpdateJson();
                    }
                }
                else
                {
                    Console.WriteLine("Dit is geen juist tijd!");
                    ConsoleUtils.WaitForKeyPress();
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