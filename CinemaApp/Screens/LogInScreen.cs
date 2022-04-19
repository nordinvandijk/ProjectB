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
            string gebruikersnaam = "<leeg>";
            string wachtwoord = "<leeg>";

            while (MenuBool){

                string titel = @"Log-in";
                string[] options = {$"Gebruikersnaam : {gebruikersnaam}", $"Wachtwoord : {wachtwoord}", "Geen account?\n Aanmelden", "Bevestiggen", "Terug"};
                Menu LogInMenu = new Menu(options, titel, 0);
                int ChosenOption = LogInMenu.Run();

                switch(ChosenOption)
                {
                    case 0:
                        Clear();
                        WriteLine("Voer je gebruikersnaam in: ");
                        CursorVisible = true;
                        gebruikersnaam = ReadLine();
                        CursorVisible = false;

                        break;
                    case 1:
                        Clear();
                        WriteLine("Voer je wachwoord in: ");
                        CursorVisible = true;
                        wachtwoord = ReadLine();
                        CursorVisible = false;
                        break;
                    case 2:
                        MenuBool = false;
                        App.accountCreationScreen.run();
                        break;
                    case 3:
                        App.userManager.Login(gebruikersnaam, wachtwoord);
                        if (App.userManager.currentUser != null){
                            App.homeScreen.run();
                        }
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


