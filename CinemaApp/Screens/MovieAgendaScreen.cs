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
            int chosenLocation = -1;
            int chosenDate = -1;
            int chosenHall = -1;

            while (chosenLocation == -1){ //code voor het kiezen van een bioscoop, zolang er geen bioscoop gekozen is
                Clear();
                int i = 0;
                foreach(Location location in App.filmAgenda.locations){
                    i++;
                }
                string[] options = new string[i+1]; //plus 1 voor extra optie menu, back knop
                i = 0;
                foreach(Location location in App.filmAgenda.locations){
                    options[i] = location.CinemaLocation;
                    i++;
                }
                options[i] = "Back";
                string titel = @"Kies een locatie";
                Menu ChooseCinema = new Menu(options, titel, 0);
                chosenLocation = ChooseCinema.Run();
                
                if(chosenLocation == i){ //back knop
                    chosenLocation = -1;
                    App.adminPanelScreen.run();
                }

                while (chosenDate == -1 && chosenLocation != -1){ //code voor het kiezen van datum zolang er geen datum gekozen is en er wel een locatie gekozen is
                    Clear();
                    CursorVisible = true;
                    WriteLine("Voer een datum in volgens het formaat: 00-00-0000\nVoer 'back' in om terug te gaan");
                    string input = ReadLine();
                    CursorVisible = false;
                    int ii = 0;
                    if(input == "back"){
                        chosenDate = -1;
                        chosenLocation = -1;
                        break;
                    }
                    foreach(Day day in App.filmAgenda.locations[chosenLocation].Days){
                        if(day.Date == input){
                            chosenDate = ii;
                        }
                        ii++;
                    }
                    if(chosenDate == -1){
                        Clear();
                        WriteLine("Deze datum bestaat niet in het systeem");
                        ConsoleUtils.WaitForKeyPress();
                    }

                    while (chosenHall == -1 && chosenDate != -1 && chosenLocation != -1){ //kiezen van een hal, zolang er geen hal gekozen is en er wel een datum en loactie gekozen is
                        int iii = 0;
                        foreach(AvailableHall hall in App.filmAgenda.locations[chosenLocation].Days[chosenDate].AvailableHalls){ //deze foreach loop kijkt hoeveel beschikbare hallen er zijn zodat er een array met de beschikbare hallen aangemaakt kan worden.
                            iii++;
                        }
                        string[] optionsHall = new string[iii+1];
                        string table = "";
                        iii = 0;
                        foreach(AvailableHall hall in App.filmAgenda.locations[chosenLocation].Days[chosenDate].AvailableHalls){ // vult de array met beschikbare hallen en voegt de naam van de hallen toe aan de table string
                            hall.OrderMovieItems(); //ordert de movieItems in een hall op begintijd
                            App.filmAgenda.UpdateJson(); //update json
                            optionsHall[iii] = $"Voeg film toe in: {hall.HallName}";
                            table += $"{hall.HallName}:\n";
                            foreach(MovieItem movieItem in hall.MovieItemlist){ // voegt de films die worden gespeeld in een hal toe aan de table string
                                table += $"-{movieItem.Title} {movieItem.StartTimeString.Substring(11)} - {movieItem.EndTimeString.Substring(11)}\n";
                            }
                            iii++;
                        }
                        optionsHall[iii] = "Back";
                        Clear();
                        Menu ChooseHall = new Menu(optionsHall, table, 0);
                        chosenHall = ChooseHall.Run();

                        if(chosenHall == iii){
                            chosenHall = -1;
                            chosenDate = -1;
                            break;
                        }
                    }

                    WriteLine($"{chosenLocation},{chosenDate},{chosenHall}");

                    ConsoleUtils.WaitForKeyPress();
                }
            }
        }
    }   
}