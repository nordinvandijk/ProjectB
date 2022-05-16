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
            Clear();
            string titel = "";
            string[] bioscoopnaam = new string[]{"Het Filmhuis Rotterdam", "Het Filmhuis Den Haag", "Het Filmhuis Amsterdam", "Het Filmhuis Utrecht", "Het Filmhuis Eindhoven"};
            string[] adres = new string[]{"Ahoyweg 12, 3084 BA Rotterdam", "President Kennedylaan 10, 2517 JK Den Haag", "Javakade 30, 1019 SZ Amsterdam", "Van Deventerlaan 20, 3528 AE Utrecht", "Willemstraat 40, 5616 GE Eindhoven"};
            for(int i = 0; i < bioscoopnaam.Length && i < adres.Length; i++)
            {
                titel += $"- {bioscoopnaam[i]} \n  {adres[i]} \n";
            }
            
            string[] options = {"Terug"};
            Menu OverviewCinemaMenu = new Menu(options, titel, 0);
            int ChosenOption = OverviewCinemaMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    App.homeScreen.run();
                    break;
            }
        }
    }   
}