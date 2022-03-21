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
            string titel = @"Titel";

            string[] options = {"Optie 1", "Optie 2", "Optie 3"};
            Menu AddMoviesMenu = new Menu(options, titel, 0);
            int ChosenOption = AddMoviesMenu.Run();

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