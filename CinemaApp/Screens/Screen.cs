using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class Screen
    {
        //Fields
        protected Application App; //Protected means any subclass can acces it

        //Constructor
        public Screen(Application app)
        {
            App = app;
        }

        //Methods

        virtual public void run()
        {
            //runs screen logic
            //override in child classes
        }
    }
}