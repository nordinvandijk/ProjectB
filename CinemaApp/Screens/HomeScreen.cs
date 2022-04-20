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

            if(App.userManager.currentUser != null){
                string[] optionsLoggedIn = {"Films", "Evenementen", "Bioscopen", "Log-in met ander account", "Mijn Reserveringen", "Abonnement", "Uitloggen", "Afsluiten"};
                Menu HomeScreenMenu = new Menu(optionsLoggedIn, titel, 0);
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
                        App.userManager.currentUser = null;
                        App.logInScreen.run();
                        break;
                    case 4:
                        //code
                        break;
                    case 5:
                        //code
                        break;
                    case 6:
                        App.userManager.currentUser = null;
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                }
            }
            else{
                string[] options = {"Films", "Evenementen", "Bioscopen", "Log-in", "Afsluiten", "Test"};
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
                        App.logInScreen.run();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    case 5:
                        App.adminPanelScreen.run();
                        break;
                }
            }
        }
    }   
}