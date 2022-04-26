using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class SubscriptionScreen : Screen
    {
        //Fields

        //Constructor
        public SubscriptionScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"Het Filmhuis-abonnement
In dit scherm kan je het Filmhuis-abonnement kopen.
Het Filmhuis-abonnement heeft de volgende voordelen:
    - Korting op films, eten en drinken en accessoires
    - Je kan naar het abonnementenevenement.";

            string[] options = {"Kopen", "Terug"};
            Menu SubscriptionMenu = new Menu(options, titel, 0);
            int ChosenOption = SubscriptionMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    //code
                    break;
                case 1:
                    App.homeScreen.run();
                    break;      
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}