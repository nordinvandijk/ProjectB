using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class LogInScreen : Screen
    {
        //Fields

        //Constructor
        public LogInScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"Titel";

            string[] options = {"Optie 1", "Optie 2", "Optie 3"};
            Menu LogInMenu = new Menu(options, titel, 0);
            int ChosenOption = LogInMenu.Run();

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