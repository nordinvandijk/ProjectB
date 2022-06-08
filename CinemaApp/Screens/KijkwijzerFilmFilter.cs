using System;
using System.Linq; 
using static System.Console;
using System.Collections.Generic;

namespace CinemaApp.Screens
{
    class KijkwijzerFilmFilter : Screen
    {
        public string ChosenFilter;
        public KijkwijzerFilmFilter(Application app) : base(app)
        {
        }
        public override void run()
        {  
             
        List<string> optionsList = new List<string>();
        List<string> FilterList = new List<string>();
        List<string> FinalList = new List<string>();
        List<string> Filter = new List<string>();
        optionsList.Add("Angst");
        optionsList.Add("Grof taalgebruik");
        optionsList.Add("Geweld");
        optionsList.Add("Discriminatie");
        optionsList.Add("\nTerug");
        FilterList.Add("Ja");
        FilterList.Add("Nee");

        Method();

            void Method()
            {    
                string titel = @"Selecteer kijkwijzer";

                string[] options = optionsList.ToArray();
                Menu FilterOverviewMenu = new Menu(options, titel, 0);
                int ChosenOption = FilterOverviewMenu.Run();
                ChosenFilter = options.GetValue(ChosenOption).ToString();


                if (ChosenOption == options.GetLength(0)-1) //terug
                {
                    App.filmOverviewScreen.run();
                }
                else //chosen restriction
                {
                    Filter.Add(optionsList[ChosenOption]);

                    optionsList.RemoveAt(ChosenOption);
                    titel =  @"Wilt u nog een kijkwijzer toevoegen?";
                    options = FilterList.ToArray();

                    Menu FilteredList = new Menu(options, titel, 0);
                    ChosenOption = FilteredList.Run();
                    ChosenFilter = options.GetValue(ChosenOption).ToString();
                    
                    if (ChosenOption == options.GetLength(0)-1) //option nee
                    {
                        foreach (Movie mov in App.movieManager.movies) //checks per movie
                        {
             
                            if (!mov.Kijkwijzer.Any(Filter.Contains)) //if kijkwijzer doesnt contain filter
                            {
                                if (!FinalList.Contains(mov.Title)) //if filterList doesnt already contain film to not double
                                {
                                    FinalList.Add(mov.Title); //add movie title if everything checks
                                }
                            }
                            
                        }
                        titel = "Selecteer film en bevestig met ENTER om meer informatie te krijgen\nFilms zonder aangegeven kijkwijzers: ";

                        FinalList.Add("\nTerug naar filmoverzicht");
                        options = FinalList.ToArray();
                        Menu FilteredFinal = new Menu(options, titel, 0);
                        ChosenOption = FilteredFinal.Run(); //print final list of films
                        ChosenFilter = options.GetValue(ChosenOption).ToString();
                        if (ChosenOption == options.GetLength(0)-1)
                        {
                            FilterList.Clear();
                            App.filmOverviewScreen.run();

                        }
                        else 
                        {
                            FilterList.Clear();
                            App.filmOverviewScreen.ChosenMovie = ChosenFilter;
                            App.filmInfoScreen.run();
                        }
                    }
                    else //option ja
                    {
                        Method();
                    }
                }
            }    
        }
    }
}
