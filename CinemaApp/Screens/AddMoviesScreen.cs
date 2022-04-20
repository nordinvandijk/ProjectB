using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class AddMoviesScreen : Screen
    {
        //Fields

        //Constructor
        public AddMoviesScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"Edit movies";

            string[] options = {"Film toevoegen", "Film verwijderen", "Terug"};
            Menu AddMoviesMenu = new Menu(options, titel, 0);
            int ChosenOption = AddMoviesMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    App.movieManager.InputAddmovie();
                    break;
                case 1:
                    App.movieManager.InputRemoveMovie();
                    break;
                case 2:
                    App.homeScreen.run();
                    break;      
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}