using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using static System.Console;
using System;
using System.Globalization;

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
    - Je kan naar het abonnementevenement.";

            string[] options = {"Overzicht van beschikbare abonnementen", "Terug"};
            Menu SubscriptionMenu = new Menu(options, titel, 0);
            int ChosenOption = SubscriptionMenu.Run();
            string titel2 = " Zilveren Filmhuis abonnement: \n   Gratis naar de film!\n   Prijs: 30 euro per maand.\n\n Gouden Filmhuis abonnement:\n   Gratis naar de film!\n   50% korting op eten en drankjes\n   Prijs: 50 euro per maand.";
            

            string[] options2 = {"Gouden Filmhuis abonnement kopen","Zilveren Filmhuis abonnement kopen", "Terug"};
            Menu OrderOverviewMenu = new Menu(options2, titel2, 0);

            switch(ChosenOption)
            {
                case 0:
                    App.userManager.LoadJson();

                    int ChosenOption2 = OrderOverviewMenu.Run();

                    if (ChosenOption2 == options2.GetLength(0)-1)
                    {   
                        App.subscriptionScreen.run();
                    }

                    else
                    {
                        var cultureInfo = new CultureInfo("nl-NL");
                        string chosenabonnement = "";
                        if (ChosenOption2 == 0)
                        {
                            // Als een abonnement wordt opgeslagen in de user wordt het type abonnement opgeslagen en de datum waarop deze betaald is.
                            // Deze datum wordt elke maand automatisch aangepast omdat een abonnement automatisch doorloopt
                            chosenabonnement = "Gouden Filmhuis abonnement";
                            App.userManager.currentUser.Abonnement = new string[]{ "Gouden Filmhuis abonnement", $"{DateTime.Today.ToString("dd-MM-yyyy", cultureInfo)}" };
                        }
                        else if (ChosenOption2 == 1)
                        {
                            chosenabonnement = "Zilveren Filmhuis abonnement";
                            App.userManager.currentUser.Abonnement = new string[] { "Zilveren Filmhuis abonnement", $"{DateTime.Today.ToString("dd-MM-yyyy", cultureInfo)}" };
                        }

                        App.userManager.UpdateJson();
                        Clear();
                        WriteLine(chosenabonnement + " is gekocht.");
                        ConsoleUtils.WaitForKeyPress();
                        App.homeScreen.run();
                    }
                    break;

                case 1:
                    App.homeScreen.run();
                    break;     
            }
        }
    }   
}