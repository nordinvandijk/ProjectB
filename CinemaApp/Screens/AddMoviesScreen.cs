using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class AddMoviesScreen : Screen
    {

        public string title = "<Leeg>"; 
        public string desc = "<Leeg>";
        public string releaseDate = "<Leeg>";
        public string genreLeegOfvol = "<Leeg>";
        public string minAge = "<Leeg>";
        public string kijkwijzerLeegOfVol = "<Leeg>";
        public string duurFilm = "<Leeg>";
        public string[] kijkwijzer;
        public string[] genre;

        //Constructor
        public AddMoviesScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
            
        }

        //Methods
        public override void run()
        {
            string titel = @"Titel";
            string[] options = {$"Titel : {title}", $"desc : {desc}", $"releaseDate : {releaseDate}", 
            $"genre : {genreLeegOfvol}", $"Minimale leeftijd : {minAge}", $"kijkwijzer : {kijkwijzerLeegOfVol}" , $"Duur film : {duurFilm}", "Bevestig", "Terug"};
            Menu OrderConfirmationMenu = new Menu(options, titel, 0);
            int ChosenOption = OrderConfirmationMenu.Run();
            switch(ChosenOption)
            {
                case 0:
                    Clear();
                    WriteLine("Wat is de titel van de film?");
                    CursorVisible = true;
                    title = ReadLine();
                    CursorVisible = false;
                    run();
                    break;
                
                case 1:
                    Clear();
                    WriteLine("Wat is de desc van de film?");
                    CursorVisible = true;
                    desc = ReadLine();
                    CursorVisible = false;
                    run();
                    break;
                
                case 2:
                    Clear();
                    WriteLine("Wat is de releaseDate van de film?");
                    CursorVisible = true;
                    releaseDate = ReadLine();
                    CursorVisible = false;
                    run();
                    break;  
                
                case 3:
                    int lenGenre = 0;
                    while(lenGenre <= 0){
                        Clear();
                        string stringlenGenre;
                        try{
                            WriteLine("Hoeveel genres heeft de film?");
                            CursorVisible = true;
                            stringlenGenre = ReadLine();
                            CursorVisible = false;
                            lenGenre= Int32.Parse(stringlenGenre);
                            if (lenGenre<=0) WriteLine("Je kan geen negatieve getal of 0 kiezen");
                            if (lenGenre<=0) ConsoleUtils.WaitForKeyPress();
                        }
                        catch{
                           WriteLine("De aantal genres moet een getal zijn");
                           ConsoleUtils.WaitForKeyPress();
                        }
                    }

                    genre = new string[lenGenre];
                    for(int i = 0; i<genre.Length; i++){
                        Clear();
                        WriteLine($"Wat is genre nummer {i+1} van de film?");
                        CursorVisible = true;
                        genre[i] = ReadLine();
                        CursorVisible = false;

                    }
                    genreLeegOfvol = "genre is toegevoegd";
                    run();
                    break;                
                
                case 4:
                    Clear();
                    WriteLine("Wat is de minimale leeftijd van de film?");
                    CursorVisible = true;
                    minAge = ReadLine();
                    CursorVisible = false;
                    run();
                    break;                
                
                case 5:
                    int LenKijkwijzers = 0;
                    while(LenKijkwijzers <= 0){
                        Clear();
                        string stringLenKijkwijzers;
                        try{
                            WriteLine("Hoeveel kijkswijzers heeft de film?");
                            CursorVisible = true;
                            stringLenKijkwijzers = ReadLine();
                            CursorVisible = false;
                            LenKijkwijzers= Int32.Parse(stringLenKijkwijzers);
                            if (LenKijkwijzers<=0) WriteLine("Je kan geen negatieve getal of 0 kiezen");
                            if (LenKijkwijzers<=0) ConsoleUtils.WaitForKeyPress();
                        }
                        catch{
                           WriteLine("De aantal kijkwijzers moet een getal zijn");
                           ConsoleUtils.WaitForKeyPress();
                        }
                    }

                    kijkwijzer = new string[LenKijkwijzers];
                    for(int i = 0; i<kijkwijzer.Length; i++){
                        Clear();
                        WriteLine($"Wat is kijkwijzer nummer {i+1} van de film?");
                        CursorVisible = true;
                        kijkwijzer[i] = ReadLine();
                        CursorVisible = false;

                    }
                    kijkwijzerLeegOfVol = "kijkwijzer is toegevoegd";
                    run();
                    break;
                
                case 6:
                    //Deze while loop blijft true zolang er geen geldige film duur is ingevuld
                    bool durationInputCorrect = false;
                    while (!durationInputCorrect)
                    {
                        //Hier wordt de gebruiker gevraagd om een film duur in te vullen
                        Clear();
                        WriteLine($"Hoe lang duurt deze film? Voer een tijd in volgens het formaat : '00:00:00'");
                        CursorVisible = true;
                        string durationInputString = ReadLine();
                        CursorVisible = false;
                        
                        //Als de user input geparsed kan worden naar een TimeSpan is er een geldige film duur ingevuld
                        TimeSpan durationTimeSpan;
                        durationInputCorrect = TimeSpan.TryParse(durationInputString, out durationTimeSpan);
                        
                        //Error message als er een verkeerde input is gegeven door de user
                        if (!durationInputCorrect)
                        {
                            Clear();
                            WriteLine("Dit is geen juist formaat voor het invoeren van de film duur");
                            ConsoleUtils.WaitForKeyPress();
                        };
                        
                    }
                    run();
                    break;
                
                case 7:
                    Clear();
                    WriteLine("Bevestig");
                    if (title != "<Leeg>" && desc != "<Leeg>" && releaseDate != "<Leeg>" && genreLeegOfvol != "<Leeg>" && minAge != "<Leeg>" && kijkwijzerLeegOfVol != "<Leeg>" && duurFilm != "<Leeg>" && kijkwijzer != null && genre != null && title != "" && desc != "" && releaseDate != "" && genreLeegOfvol != "" && minAge != "" && duurFilm != "")
                    {             
                        try{
                            App.movieManager.AddMovie(title,desc,releaseDate,genre,Int32.Parse(minAge),kijkwijzer,duurFilm);
                            ConsoleUtils.WaitForKeyPress();
                             // reset alles terug nadat de film is toegevoegd
                            title = "<Leeg>";
                            desc = "<Leeg>";
                            releaseDate = "<Leeg>";
                            genreLeegOfvol = "<Leeg>";
                            minAge = "<Leeg>";
                            kijkwijzerLeegOfVol = "<Leeg>";
                            duurFilm = "<Leeg>";
                            kijkwijzer = null; 
                            genre = null;
                            App.adminPanelScreen.run();
                        }
                        catch{
                            WriteLine("De inputs waren verkeerd probeer het nog een keer.");
                            ConsoleUtils.WaitForKeyPress();
                            run();
                        }               
                    }
                    else{
                        WriteLine("Sommige vakken zijn niet ingevuld");
                        ConsoleUtils.WaitForKeyPress();
                        run();
                    }
                    break; 
                // case 7:
                //     Clear();
                //     string deleteTitle = null;
                //     while(deleteTitle == "" || deleteTitle == null){
                //        Clear();
                //        WriteLine("De titel naam is nog niet toegevoegd.");
                //        deleteTitle = ReadLine();
                //     }
                //     App.movieManager.RemoveMovie(deleteTitle); 
                //     App.adminPanelScreen.run(); 
                //     break; 
                case 8:
                    App.adminPanelScreen.run();
                    break;    
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}