using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class OverviewCinemaScreen : Screen
    {
        //Fields

        //Constructor
        public OverviewCinemaScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"Titel";

            string[] options = {"Optie 1", "Optie 2", "Optie 3"};
            Menu OverviewCinemaMenu = new Menu(options, titel, 0);
            int ChosenOption = OverviewCinemaMenu.Run();

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