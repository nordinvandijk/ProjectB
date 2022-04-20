using System;
using static System.Console;

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
            string titel = @"Admin Panel";

            string[] options = {"Film toevoegen", "Film-agenda", "Uitloggen"};
            Menu AdminPanelMenu = new Menu(options, titel, 0);
            int ChosenOption = AdminPanelMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    App.addMoviesScreen.run();
                    break;
                case 1:
                    App.movieAgendaScreen.run();
                    break;
                case 2:
                    App.userManager.currentUser = null;
                    App.homeScreen.run();
                    break;      
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}