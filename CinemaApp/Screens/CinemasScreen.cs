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
            Console.WriteLine("Dit is een testbericht");
        }
    }   
}