using System;
using static System.Console;
using CinemaApp;

namespace CinemaApp.Screens
{
    class MovieAgendaScreen : Screen
    {
        //Fields

        //Constructor
        public MovieAgendaScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            // Zolang er geen locatie/datum/hall gekozen is de index waarde -1
            int chosenLocation = -1;
            int chosenDate = -1;
            int chosenHall = -1;
            
            //
            // Code voor het kiezen van een locatie zolang die nog niet gekozen is
            //

            while (chosenLocation == -1){
                Clear();
                int amountOfLocations = App.filmAgenda.locations.Count;
                string[] optionsLocationMenu = new string[amountOfLocations + 1]; // Plus 1 voor extra optie in het menu, namelijk de 'back' optie
                
                // Namen van locaties worden toegevoegd aan 'optionsLocationMenu' array
                int i = 0;
                foreach(Location location in App.filmAgenda.locations){
                    optionsLocationMenu[i] = location.CinemaLocation;
                    i++;
                }
                // De back optie wordt toegevoegd aan 'optionsLocationMenu' array
                optionsLocationMenu[i] = "\nTerug";

                // Aanmaken en runnen van LocationMenu
                string titel = @"Kies een locatie";
                Menu locationMenu = new Menu(optionsLocationMenu, titel, 0);
                chosenLocation = locationMenu.Run();
                
                // Als de gekozen locatie gelijk is aan 'i' wordt er gekozen voor de 'back' optie in het 'locationMenu'
                if(chosenLocation == i){
                    chosenLocation = -1;
                    App.adminPanelScreen.run();
                }

                //
                // Code voor het kiezen van een datum zolang er nog geen datum gekozen is maar wel een locatie
                //

                while (chosenDate == -1 && chosenLocation != -1){
                    
                    // Hier wordt een user input gevraagd in de vorm "00-00-0000"
                    Clear();
                    CursorVisible = true;
                    WriteLine("Voer een datum in volgens het formaat: 00-00-0000\nVoer 'terug' in om terug te gaan");
                    string userInputDate = ReadLine();
                    CursorVisible = false;

                    // Als de user input "terug" is gaat het programma terug naar het 'locationMenu'
                    if(userInputDate == "terug"){
                        chosenDate = -1;
                        chosenLocation = -1;
                        break;
                    }
                    
                    // Deze for loop checkt of de ingevulde datum in het het systeem staat
                    int j = 0;
                    foreach (Day day in App.filmAgenda.locations[chosenLocation].Days){
                        if(day.Date == userInputDate){
                            chosenDate = j;
                        }
                        j++;
                    }
                    
                    // Als de datum niet in het systeem staat krijgt de user een bericht en kan hij een nieuwe datum invullen
                    if(chosenDate == -1){
                        Clear();
                        WriteLine("Deze datum bestaat niet in het systeem");
                        ConsoleUtils.WaitForKeyPress();
                    }

                    //
                    // Code voor het kiezen van een bioscoopzaal zolang er geen bioscoopzaal gekozen is maar wel een locatie en datum
                    //

                    while (chosenHall == -1 && chosenDate != -1 && chosenLocation != -1){
                        
                        int amountOfHalls = App.filmAgenda.locations[chosenLocation].Days[chosenDate].AvailableHalls.Count;
                        string[] optionsHallMenu = new string[amountOfHalls+1];

                        // In de string 'table' wordt een overzicht gegeven van alle 'movie items' per bioscoopzaal voor een gekozen locatie en datum
                        //string table = "";
                        
                        // In deze foreach loop worden de namen van alle bioscoopzalen toegvoegd aan de 'optionsHall' array
                        // De namen van de bioscoopzalen worden ook toegevoegd aan de 'table' string
                        int k = 0;
                        foreach(AvailableHall hall in App.filmAgenda.locations[chosenLocation].Days[chosenDate].AvailableHalls){
                            string table = "";
                            // Hier worden de 'movieItems' in een bioscoopzaal op starttijd gesorteerd en daarna wordt de json geupdate
                            hall.OrderMovieItems();
                            App.filmAgenda.UpdateJson();

                            table += $"{hall.HallName}:\n";

                            // In deze foreach loop worden de 'movieItems' van elke bioscoop zaal toegevoegd aan de 'table' string
                            foreach(MovieItem movieItem in hall.MovieItemlist){
                                table += $"-{movieItem.Title} {movieItem.StartTimeString.Substring(11)} - {movieItem.EndTimeString.Substring(11)}\n";
                            }
                            optionsHallMenu[k] = table;
                            k++;
                        }
                        
                        optionsHallMenu[k] = "Terug";

                        // Aanmaken en runnen 'hallMenu'
                        Clear();
                        Menu hallMenu = new Menu(optionsHallMenu, "Klik op een zaal om een film toe te voegen", 0);
                        chosenHall = hallMenu.Run();

                        // Als de gekozen hall gelijk is aan 'k' wordt er gekozen voor de 'back' optie in het 'hallMenu'
                        if (chosenHall == k){
                            chosenHall = -1;
                            chosenDate = -1;
                            break;
                        }

                        App.filmAgenda.AddMovieItem(chosenLocation, chosenDate, chosenHall);
                        chosenHall = -1;
                    }
                    ConsoleUtils.WaitForKeyPress();
                }
            }
        }
    }   
}