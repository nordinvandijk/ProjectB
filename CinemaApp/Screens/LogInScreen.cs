using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class LogInScreen : Screen
    {
        //Fields

        //Constructor
        public LogInScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            bool MenuBool = true;
            string email = "<leeg>";
            string wachtwoord = "<leeg>";

            while (MenuBool){

                string titel = @"Titel";
                string[] options = {$"Email : {email}", $"Wachtwoord : {wachtwoord}", "Geen account?\n Aanmelden", "Bevestiggen", "Terug"};
                Menu LogInMenu = new Menu(options, titel, 0);
                int ChosenOption = LogInMenu.Run();

                switch(ChosenOption)
                {
                    case 0:
                        Clear();
                        WriteLine("Voer je Email in: : ");
                        email = ReadLine();

                        break;
                    case 1:
                        Clear();
                        WriteLine("Voer je wachwoord in: : ");
                        wachtwoord = ReadLine();
                        break;
                    case 2:
                        MenuBool = false;
                        App.accountCreationScreen.run();
                        break;
                    case 3:
                        break;
                    case 4:
                        MenuBool = false;
                        App.homeScreen.run();
                        break;
                }

                ConsoleUtils.WaitForKeyPress();
            }
        }
    }   
}