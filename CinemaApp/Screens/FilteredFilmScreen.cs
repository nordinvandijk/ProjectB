using System;
using System.Linq; 
using static System.Console;
using System.Collections.Generic;

namespace CinemaApp.Screens
{
    class FilteredFilmScreen : Screen
    {
        public string ChosenFilter;
        public FilteredFilmScreen(Application app) : base(app)
        {
        }
        public override void run()
        {
            string titel = @"Selecteer genre";
            
            List<string> optionsList = new List<string>(); //lege lijst om genres te toevoegen
            List<string> FilterList = new List<string>();  //lege lijst om gefiltreerde films te toevoegen

            foreach (Movie mov in App.movieManager.movies) //checkt per movie in Json bestand
            {
                for (int i = 0; i < mov.Genre.Length; i++) //checkt per hoeveelheid genres in elk film
                {
                    if (!optionsList.Contains(mov.Genre[i])) //als optionList al gevonden genre bevat, wordt het niet toegevoegd om opties niet te dupliceren
                    {
                        optionsList.Add(mov.Genre[i]);
                    }
                }
            }
            optionsList.Add("\nTerug");
            string[] options = optionsList.ToArray();
            Menu FilterOverviewMenu = new Menu(options, titel, 0);
            int ChosenOption = FilterOverviewMenu.Run();
            ChosenFilter = options.GetValue(ChosenOption).ToString();

            if (ChosenOption == options.GetLength(0)-1) //terug button
            {
                App.filmOverviewScreen.run();
            }
            else
            {
                titel = "Selecteer film en bevestig met ENTER om meer informatie te krijgen\n" + optionsList[ChosenOption] + " films:";
                foreach (Movie mov in App.movieManager.movies) //checkt per film in json bestand

                {
                    if (mov.Genre.Contains(optionsList[ChosenOption])) //als film in json bestand wel gekozen optie bevat, wordt het aan filterlist toegevoegd om later als optie te worden gebruikt
                    {
                        FilterList.Add(mov.Title);
                    }
                }
                FilterList.Add("\nTerug naar filmoverzicht");
                options = FilterList.ToArray();
                Menu FilteredDrama = new Menu(options, titel, 0);
                ChosenOption = FilteredDrama.Run();
                ChosenFilter = options.GetValue(ChosenOption).ToString();
                if (ChosenOption == options.GetLength(0)-1) //terug button
                {
                    FilterList.Clear();
                    App.filteredFilmScreen.run();

                }
                else 
                {
                    FilterList.Clear();
                    App.FilmFilter.run();
                }
            }
        }
    }
}