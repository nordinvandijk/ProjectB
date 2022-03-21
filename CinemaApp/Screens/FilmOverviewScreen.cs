using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class FilmOverviewScreen : Screen
    {
        //Fields

        //Constructor
        public FilmOverviewScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"Film Overview";

            string[] options = {"Spiderman", "Batman", "Uncharted"};
            Menu FilmOverviewMenu = new Menu(options, titel, 0);
            int ChosenOption = FilmOverviewMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    //code
                    break;
                case 1:
                    //code
                    break;
                case 2:
                    //code
                    break;      
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}