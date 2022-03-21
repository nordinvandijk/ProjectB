using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class CinemasScreen : Screen
    {
        //Fields

        //Constructor
        public CinemasScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string[] bioscoopnaam = new string[]{"De BIOS Rotterdam", "De BIOS Den Haag", "De BIOS Amsterdam", "De BIOS Utrecht", "De BIOS Eindhoven"};
            string[] adres = new string[]{"Ahoyweg 12, 3084 BA Rotterdam", "President Kennedylaan 10, 2517 JK Den Haag", "Javakade 30, 1019 SZ Amsterdam", "Van Deventerlaan 20, 3528 AE Utrecht", "Willemstraat 40, 5616 GE Eindhoven"};
            for(int i = 0; i < bioscoopnaam.Length && i < adres.Length; i++){
                Console.WriteLine($"- {bioscoopnaam[i]} \n  {adres[i]}");
            }
        }
    }   
}