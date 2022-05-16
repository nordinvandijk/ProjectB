using System;
using static System.Console;

namespace CinemaApp.Screens
{
    class AccountCreationScreen : Screen
    {
        //Fields

        //Constructor
        public AccountCreationScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {

        }

        //Methods
        public override void run()
        {   
            bool MenuBool = true;

            string gebruikersnaam = "<leeg>";
            string wachtwoord = "<leeg>";
            string wachtwoordHerhaal = "<leeg>";
            string email = "<leeg>";
            string telefoon = "<leeg>";

            while (MenuBool) {

                string titel = @"Account aanmaken?";
                string[] options = {$"Gebruikersnaam : {gebruikersnaam}", $"Wachtwoord : {wachtwoord}", $"Herhaal je wachtwoord : {wachtwoordHerhaal}", 
                $"Email : {email}", $"Telefoon nummer : {telefoon}", "Bevestigen", "Terug"};
                Menu AccountCreationMenu = new Menu(options, titel, 0);
                int ChosenOption = AccountCreationMenu.Run();

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
                        WriteLine("Voer je wachtwoord in: ");
                        CursorVisible = true;
                        wachtwoord = ReadLine();
                        CursorVisible = false;
                        break;
                    case 2:
                        Clear();
                        WriteLine("Herhaal je wachtwoord: ");
                        CursorVisible = true;
                        wachtwoordHerhaal = ReadLine();
                        CursorVisible = false;
                        break;
                    case 3:
                        Clear();
                        WriteLine("Voer je email in: ");
                        CursorVisible = true;
                        email = ReadLine();
                        CursorVisible = false;
                        break;
                    case 4:
                        Clear();
                        WriteLine("Voer je telefoon nummer in: ");
                        CursorVisible = true;
                        telefoon = ReadLine();
                        CursorVisible = false;
                        break;
                    case 5:
                        Clear();
                        if (wachtwoord != wachtwoordHerhaal) {
                            WriteLine("Het herhaalde wachtwoord komt niet overeen met je wachtwoord");
                        }
                        else if (App.userManager.CreateUser(gebruikersnaam,wachtwoord,email)){
                            App.userManager.Login(gebruikersnaam,wachtwoord);
                            ConsoleUtils.WaitForKeyPress();
                            App.homeScreen.run();
                            MenuBool = false;
                        }
                        ConsoleUtils.WaitForKeyPress();
                        break;
                    case 6:
                        App.logInScreen.run();
                        MenuBool = false;
                        break; 
                }

            }
        }
    }   
}