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
╚═╝░░╚═╝╚══════╝░░░╚═╝░░░  ╚═╝░░░░░╚═╝╚══════╝╚═╝░░░░░╚═╝╚═╝░░╚═╝░╚═════╝░╚═╝╚═════╝░

╒════════════════════════════════════════════════════╕
│ Navigeer door het programma met de pijltjestoetsen │ 
│ of met de WASD toetsen. Druk op ENTER om een       │
│ gekozen optie te bevestigen. We raden je aan om    │
│ de applicatie in fullscreen te gebruiken.          │   
╘════════════════════════════════════════════════════╛
";

            if(App.userManager.currentUser != null){
                string[] optionsLoggedIn = {"Films", "Evenementen", "Bioscopen", "Mijn Reserveringen", "Abonnement", "Uitloggen", "Afsluiten"};
                Menu HomeScreenMenu = new Menu(optionsLoggedIn, titel, 0);
                int ChosenOption = HomeScreenMenu.Run();
                switch(ChosenOption)
                {
                    case 0:
                        App.filmOverviewScreen.run(); // run object filmOverviewScreen uit App object
                        break;
                    case 1:
                        App.eventScreen.run();
                        break;
                    case 2:
                        App.overviewCinemaScreen.run();
                        break;
                    case 3:
                        App.reservationOverviewScreen.run();
                        break;
                    case 4:
                        App.subscriptionScreen.run();
                        break;
                    case 5:
                        App.userManager.currentUser = null;
                        Clear();
                        WriteLine("Uitgelogd");
                        ConsoleUtils.WaitForKeyPress();
                        App.homeScreen.run();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                }
            }
            else{
                string[] options = {"Films", "Evenementen", "Bioscopen", "Log-in", "Afsluiten"};
                Menu HomeScreenMenu = new Menu(options, titel, 0);
                int ChosenOption = HomeScreenMenu.Run();
                switch(ChosenOption)
                {
                    case 0:
                        App.filmOverviewScreen.run(); // run object filmOverviewScreen uit App object
                        break;
                    case 1:
                        App.eventScreen.run();
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
                }
            }
        }
    }   
}