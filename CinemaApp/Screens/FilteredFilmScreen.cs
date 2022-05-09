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
            
            List<string> optionsList = new List<string>();
            List<string> FilterList = new List<string>();

            foreach (Movie mov in App.movieManager.movies)
            {
                for (int i = 0; i < mov.Genre.Length; i++)
                {
                    if (!optionsList.Contains(mov.Genre[i]))
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

            if (ChosenOption == options.GetLength(0)-1) {
                App.filmOverviewScreen.run();
            }
            else
            {
                titel = optionsList[ChosenOption] + @" films:";
                foreach (Movie mov in App.movieManager.movies) 
                {
                    if (mov.Genre.Contains(optionsList[ChosenOption]))
                    {
                        FilterList.Add(mov.Title);
                    }
                }
                FilterList.Add("\nTerug");
                options = FilterList.ToArray();
                Menu FilteredDrama = new Menu(options, titel, 0);
                ChosenOption = FilteredDrama.Run();
                ChosenFilter = options.GetValue(ChosenOption).ToString();
                if (ChosenOption == options.GetLength(0)-1)
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