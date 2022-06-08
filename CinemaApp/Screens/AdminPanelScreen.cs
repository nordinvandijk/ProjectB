using System;
using static System.Console;
using System.Globalization;
using System.Collections.Generic;

namespace CinemaApp.Screens
{
    class AdminPanelScreen : Screen
    {
        //Fields

        //Constructor
        public AdminPanelScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            DateTimeStyles styles = DateTimeStyles.None;
            var cultureInfo = new CultureInfo("nl-NL");

            string titel = @"Admin Panel";

            string[] options = {"Film toevoegen", "Film verwijderen" ,"Film-agenda", "Evenement toevoegen","Evenement verwijderen","Omzet Overzicht", "\nUitloggen"};
            Menu AdminPanelMenu = new Menu(options, titel, 0);
            int ChosenOption = AdminPanelMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    App.addMoviesScreen.run();
                    break;
                case 1:
                    Clear();
                    Write("Voer een titel in van de film die verwijderd moet worden: ");
                    string title = ReadLine(); 
                    App.movieManager.RemoveMovie(title); 
                    ConsoleUtils.WaitForKeyPress();
                    run();
                    break; 
                case 2:
                    App.movieAgendaScreen.run();
                    break;
                case 3: //evenementen toevoegen pagina
                    App.addEventScreen.run();
                    break;
                case 4:
                    Clear();
                    Write("Voer een titel in van het evenement dat verwijderd moet worden: ");
                    string evenement = ReadLine(); 
                    App.eventManager.RemoveEvent(evenement); 
                    ConsoleUtils.WaitForKeyPress();
                    run();             
                    break;
                case 5:
                    Clear();
                    WriteLine("Van welke week wil je de omzet zien? Voer een datum van die week in. Schrijf het zoals dit voorbeeld: 09-05-2022"); 
                    CursorVisible = true;
                    DateTime Omzetdate;
                    string OmzetdateString = ReadLine();
                    while (!(DateTime.TryParse(OmzetdateString,cultureInfo,styles, out Omzetdate))){
                        Clear();
                        WriteLine("Probeer de datum opnieuw in te vullen, Schrijf het zoals dit voorbeeld op: 09-05-2022");
                        OmzetdateString = ReadLine();
                    } //public string Remove (int startIndex, int count);
                    CursorVisible = false;
                    Omzetdate = DateTime.Parse(OmzetdateString,cultureInfo,styles).Date;
                    List<Omzet> filteredTotaleOmzet = new List<Omzet>(); //lege list 
                    for(int i = 0; i<App.omzetManager.totaalOmzet.Count; i++){
                        if((DateTime.Parse(App.omzetManager.totaalOmzet[i].CurrentDate).Date >= Week(Omzetdate).Item1 && DateTime.Parse(App.omzetManager.totaalOmzet[i].CurrentDate).Date <= Week(Omzetdate).Item2)){
                            filteredTotaleOmzet.Add(App.omzetManager.totaalOmzet[i]); //voegt omzet toe als het in de week zit van de datum 
                        }
                    }
                    double TotaleOmzet = 0;
                    double addableItems = 0;
                    double seatOmzet = 0;
                    double omzetAbonnement = 0;
                    foreach(var omzets in filteredTotaleOmzet){ //loop door Omzet.json
                        for(int i = 0; i<omzets.Seats.Count; i++){ //loop door stoelen in een object in omzet.json
                            TotaleOmzet += omzets.Seats[i].Price; //voegt stoelen toe aan totale omzet
                            seatOmzet += omzets.Seats[i].Price; //voegt stoelen toe aan de seatomzet
                        }
                        for(int i = 0; i<omzets.AddableItems.Count; i++){ //loop door AddableItems van een object in omzet.json
                            for(int j = 0; j<App.addableItemsManager.addableItems.Count; j++){ //zoekt naar de prijs van de AddableItems 
                                if(omzets.AddableItems[i].Name == App.addableItemsManager.addableItems[j].Name){ 
                                    TotaleOmzet += App.addableItemsManager.addableItems[j].Price; //voegt prijs toe
                                    addableItems += App.addableItemsManager.addableItems[j].Price; //voegt prijs toe                                  
                                }
                            }
                        }
                    }
                    foreach(var User in App.userManager.users){
                        if (User.Abonnement != null)
                        {
                            if ((DateTime.Parse(User.Abonnement[1]).Date >= Week(Omzetdate).Item1 && DateTime.Parse(User.Abonnement[1]).Date <= Week(Omzetdate).Item2))
                            {
                                    if (User.Abonnement[0] == "Gouden Filmhuis abonnement")
                                    {
                                        omzetAbonnement += 50;
                                    }
                                    if (User.Abonnement[0] == "Zilveren Filmhuis abonnement")
                                    {
                                        omzetAbonnement += 30;
                                    }
                            }
                        }
                    }
                    TotaleOmzet += omzetAbonnement;
                    Clear();
                    WriteLine($"De omzet van week {Week(Omzetdate).Item1} - {Week(Omzetdate).Item2}"); //print geselecteerde week
                    WriteLine($"De totale omzet is: {TotaleOmzet} euro"); //print totale omzet
                    WriteLine($"De omzet van de toegevoegde accessoires/eten is: {addableItems} euro"); // print spullen/eten winst
                    WriteLine($"De omzet van de stoelen is: {seatOmzet} euro"); // print winst van stoelen 
                    WriteLine($"De omzet van de abonnementen is: {omzetAbonnement} euro"); // print winst van stoelen                     
                    ConsoleUtils.WaitForKeyPress();
                    run();
                    break;
                case 6:
                    App.userManager.currentUser = null;
                    App.homeScreen.run();
                    break;      
            }

            ConsoleUtils.WaitForKeyPress();
        }
        public Tuple<DateTime,DateTime> Week(DateTime date){ //deze functie returned een tuple met een de eerste dag van een week(maandag) en de laatste (zondag), de parameter is een datum
            if(date.DayOfWeek.ToString() == "Monday"){
                return Tuple.Create(date.Date, date.AddDays(6).Date);
            }
            if(date.DayOfWeek.ToString() == "Tuesday"){
                return Tuple.Create(date.AddDays(-1).Date, date.AddDays(5).Date);
            }
            if(date.DayOfWeek.ToString() == "Wednesday"){
                return Tuple.Create(date.AddDays(-2).Date, date.AddDays(4).Date);
            }
            if(date.DayOfWeek.ToString() == "Thursday"){
                return Tuple.Create(date.AddDays(-3).Date, date.AddDays(3).Date);
            }
            if(date.DayOfWeek.ToString() == "Friday"){
                return Tuple.Create(date.AddDays(-4).Date, date.AddDays(2).Date);
            }
            if(date.DayOfWeek.ToString() == "Saturday"){
                return Tuple.Create(date.AddDays(-5).Date, date.AddDays(1).Date);
            }
            else{
                return Tuple.Create(date.AddDays(-6).Date, date.Date);                
            }
        }
    }   
}