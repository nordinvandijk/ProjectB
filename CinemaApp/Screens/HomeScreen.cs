using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class HomeScreen : Screen
    {
        //Fields

        //Constructor
        public HomeScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"
██████  ███████     ██████  ██  ██████  ███████ 
██   ██ ██          ██   ██ ██ ██    ██ ██      
██   ██ █████       ██████  ██ ██    ██ ███████ 
██   ██ ██          ██   ██ ██ ██    ██      ██ 
██████  ███████     ██████  ██  ██████  ███████";

            string[] options = {"Films", "Evenementen", "Bioscopen", "Log-in", "Mijn Reserveringen", "Abonnement"};
            Menu HomeScreenMenu = new Menu(options, titel, 0);
            HomeScreenMenu.Run();

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}