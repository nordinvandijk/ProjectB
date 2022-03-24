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
██╗░░██╗███████╗████████╗  ███████╗██╗██╗░░░░░███╗░░░███╗██╗░░██╗██╗░░░██╗██╗░██████╗
██║░░██║██╔════╝╚══██╔══╝  ██╔════╝██║██║░░░░░████╗░████║██║░░██║██║░░░██║██║██╔════╝
███████║█████╗░░░░░██║░░░  █████╗░░██║██║░░░░░██╔████╔██║███████║██║░░░██║██║╚█████╗░
██╔══██║██╔══╝░░░░░██║░░░  ██╔══╝░░██║██║░░░░░██║╚██╔╝██║██╔══██║██║░░░██║██║░╚═══██╗
██║░░██║███████╗░░░██║░░░  ██║░░░░░██║███████╗██║░╚═╝░██║██║░░██║╚██████╔╝██║██████╔╝
╚═╝░░╚═╝╚══════╝░░░╚═╝░░░  ╚═╝░░░░░╚═╝╚══════╝╚═╝░░░░░╚═╝╚═╝░░╚═╝░╚═════╝░╚═╝╚═════╝░";

            string[] options = {"Films", "Evenementen", "Bioscopen", "Log-in", "Mijn Reserveringen", "Abonnement"};
            Menu HomeScreenMenu = new Menu(options, titel, 0);
            int ChosenOption = HomeScreenMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    App.filmOverviewScreen.run(); // run object filmOverviewScreen uit App object
                    break;
                case 1:
                    //code
                    break;
                case 2:
                    App.overviewCinemaScreen.run();
                    break;
                case 3:
                    //code
                    break;
                case 4:
                    //code
                    break;
                case 5:
                    //code
                    break;
                
            }
        }
    }   
}