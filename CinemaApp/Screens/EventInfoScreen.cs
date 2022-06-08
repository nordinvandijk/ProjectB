using System;
using System.Collections.Generic;
using static System.Console;

namespace CinemaApp.Screens
{
    class EventInfoScreen : Screen
    {
        //Fields
        Event Event;
        private int selectedMovieItem;
        private List<MovieItem> options = new List<MovieItem>();

        //Constructor
        public EventInfoScreen(Application app) : base(app)
        {
        }

        //Methods

        public void Display()
        {
            //gaat door events om de event te vinden die is gekozen bij eventscreen
            foreach (Event ev in App.eventManager.events)
            {
                if (ev.Name == App.eventScreen.ChosenEvent)
                {
                    Event = ev;
                    break;
                }
            }

            // Displayt informatie over de film
            Console.WriteLine($"╒═{new string('═', Event.Name.Length)}═╕\n│ {Event.Name} │\n╘═{new string('═', Event.Name.Length)}═╛");

            string descriptionInBox = "";
            int longestLine = 0;
            int i = 0;
            foreach (Char c in Event.Description)
            {
                i++;
                if (c == ' ' && i >= 80)
                {
                    if (i > longestLine) { longestLine = i; }
                    i = 0;
                }
            }
            if (i > longestLine) { longestLine = i; }

            string add = "│ ";
            i = 0;
            descriptionInBox += $"╒═{new string('═', longestLine)}═╕\n";
            foreach (Char c in Event.Description)
            {
                add += c;
                i++;
                if (c == ' ' && i >= 80)
                {
                    descriptionInBox += add + new string(' ', longestLine - i) + " │\n";
                    add = "│ ";
                    i = 0;
                }
            }
            descriptionInBox += $"╘═{new string('═', longestLine)}═╛";

            Console.WriteLine("\nBeschrijving:\n" + descriptionInBox +
                        "\nMinimale leeftijd: " + Event.MinimumAge +
                         "\nTijdsduur: " + Event.Duration + "\nTicket prijs: " + Event.TicketPrice);

            // Deze integer wordt met 1 verhoogd elke keer voordat er een movieItem wordt gedisplayt, hierdoor kan het geselecteerde movieItem anders gedisplayt worden
            int numberMovieItem = 0;

            // Iterate over alle locaties en displayt ze
            foreach (Location location in App.filmAgenda.locations)
            {
                // Displayt alle locaties
                Console.WriteLine($"╒═{new string('═', location.CinemaLocation.Length)}═╕\n│ {location.CinemaLocation} │\n╘═{new string('═', location.CinemaLocation.Length)}═╛");

                // Iterate over alle dagen
                foreach (Day day in location.Days)
                {
                    // Als de dag later of gelijk is aan vandaag
                    if (DateTime.Parse(day.Date) >= DateTime.Today)
                    {
                        // Displayt de dagen vanaf vandaag
                        Console.WriteLine(day.Date);

                        // Iterate over alle bioscoopzalen
                        foreach (AvailableHall hall in day.AvailableHalls)
                        {
                            // Iterate over alle movieItems
                            foreach (MovieItem movieItem in hall.MovieItemlist)
                            {
                                // Als een movieItem dezelfde titel heeft als de gekozen film wordt hij gedisplayt
                                if (movieItem.Title == Event.Name)
                                {
                                    Console.Write($"    ");
                                    if (selectedMovieItem == numberMovieItem)
                                    {
                                        ForegroundColor = ConsoleColor.Black;
                                        BackgroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        ForegroundColor = ConsoleColor.White;
                                        BackgroundColor = ConsoleColor.Black;
                                    }

                                    Console.Write($"<{movieItem.Title} | Formaat: {movieItem.Format} | Tijd: {movieItem.StartTimeString.Substring(11)} - {movieItem.EndTimeString.Substring(11)} >");
                                    ResetColor();
                                    Console.Write("\n");
                                    numberMovieItem++;
                                }
                            }
                        }
                    }
                }
            }

            // Display 'terug' button
            if (selectedMovieItem == numberMovieItem)
            {
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
            }
            else
            {
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine("\nTerug");
            ResetColor();
        }
        public override void run()
        {
            int amountOfMovieItems = 0;
            selectedMovieItem = 0;

            // Finds the amount of moviesItems from the chosen movie that play the upcoming 7 days
            foreach (Location location in App.filmAgenda.locations)
            {
                foreach (Day day in location.Days)
                {
                    if (DateTime.Parse(day.Date) >= DateTime.Today)
                    {
                        foreach (AvailableHall hall in day.AvailableHalls)
                        {

                            foreach (MovieItem movieItem in hall.MovieItemlist)
                            {
                                if (movieItem.Title == App.eventScreen.ChosenEvent)
                                {
                                    amountOfMovieItems++;
                                    options.Add(movieItem);
                                }
                            }
                        }
                    }
                }
            }

            ConsoleKey keyPressed;
            do
            {
                Clear();
                Display();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.W)
                {
                    selectedMovieItem--;
                    if (selectedMovieItem < 0)
                    {
                        selectedMovieItem = amountOfMovieItems;
                    }
                }
                if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
                {
                    selectedMovieItem++;
                    if (selectedMovieItem >= amountOfMovieItems + 1)
                    {
                        selectedMovieItem = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            // If the 'Terug' button is clicked
            if (selectedMovieItem == amountOfMovieItems)
            {
                App.eventScreen.run();
            }
            else
            {
                App.filmInfoScreen.chosenMovieItem = options[selectedMovieItem];
                App.seatsOverviewScreen.run();
            }
        }
    }
}