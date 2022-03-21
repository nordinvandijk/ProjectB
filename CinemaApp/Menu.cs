using System;
using static System.Console;
using CinemaApp.Screens; //Using namespace folder Screens

namespace CinemaApp
{
    class Menu
    {
        //Fields
        string[] Options;
        string Titel;
        int SelectedOption;

        //Constructor
        public Menu(string[] options, string titel, int selectedOption)
        {
            Titel = titel;
            Options = options;
            SelectedOption = selectedOption;
        }

        //Methods
        private void Display()
        {
            WriteLine(Titel);
            WriteLine();

            for(int i = 0; i < Options.Length; i++)
            {
                if (i == SelectedOption) //de geselecteerde optie krijgt witte achtergrond
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine(Options[i]); //Print alle menu opties
            }
            ResetColor(); //reset color naar default
        }

        public int Run()
        {
            ConsoleKey keyPressed; //consoleKey is een Enum, kan verschillend aantal waarde aannemen die een key vertegenwoordigen
            do
            {
                Clear(); //maakt scherm leeg
                Display(); //displayt opties, staat in de loop zodat het constant gerefreshed wordt en selectedOption gedisplayt kan worden
                
                ConsoleKeyInfo keyInfo = ReadKey(true); //ReadKey geeft ConsoleKeyInfo, onder andere welke key ingedrukt is
                keyPressed = keyInfo.Key; //keyInfo.Key pakt uit de keyInfo welke key er ingedrukt is
                
                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.W) //checkt of up arrow geklikt wordt
                {
                    SelectedOption--;
                    if (SelectedOption < 0) // zorgt dat selectedOption niet out of index gaat
                    {
                        SelectedOption = Options.Length - 1;
                    }
                }
                if (keyPressed == ConsoleKey.DownArrow || keyPressed == ConsoleKey.S)
                {
                    SelectedOption++;
                    if (SelectedOption >= Options.Length)
                    {
                        SelectedOption = 0;
                    }
                }
            } while(keyPressed != ConsoleKey.Enter);
            
            return SelectedOption; //returnt de geselecteerde optie als enter wordt gekilkt
        }
    }
}