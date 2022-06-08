using System;
using static System.Console;
using System.Net.Mail;
using System.Text.RegularExpressions;
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

            while (MenuBool)
            {
                string titel = @"Account aanmaken";
                string[] options = {$"Gebruikersnaam : {gebruikersnaam}", $"Wachtwoord : {wachtwoord}", $"Herhaal je wachtwoord : {wachtwoordHerhaal}",
                $"Email : {email}", $"Telefoon nummer : {telefoon}", "Bevestigen", "\nTerug"};
                Menu AccountCreationMenu = new Menu(options, titel, 0);
                int ChosenOption = AccountCreationMenu.Run();

                switch (ChosenOption)
                {
                    case 0:
                        Clear();
                        WriteLine("Voer je gebruikersnaam in en bevestig met ENTER: ");
                        CursorVisible = true;
                        gebruikersnaam = ReadLine();
                        CursorVisible = false;
                        break;
                    case 1:
                        Clear();
                        WriteLine("Voer je wachtwoord in en bevestig met ENTER: ");
                        CursorVisible = true;
                        wachtwoord = ReadLine();
                        CursorVisible = false;
                        break;
                    case 2:
                        Clear();
                        WriteLine("Herhaal je wachtwoord en bevestig met ENTER: ");
                        CursorVisible = true;
                        wachtwoordHerhaal = ReadLine();
                        CursorVisible = false;
                        break;
                    case 3:
                        Clear();
                        WriteLine("Voer je email in en bevestig met ENTER: ");
                        CursorVisible = true;
                        email = ReadLine();
                        while (true)
                        {
                            try
                            {
                                MailAddress test = new MailAddress(email);
                                break;
                            }
                            catch
                            {
                                Clear();
                                WriteLine("Voer een goede email in: ");
                                email = ReadLine();
                            }
                        }
                        CursorVisible = false;
                        break;
                    case 4:
                        Clear();
                        WriteLine("Voer je Nederlandse telefoon nummer in en bevestig met ENTER: ");

                        CursorVisible = true;
                        telefoon = ReadLine();
                        while(Regex.IsMatch(telefoon, @"^([+]{1}[\d]{1})?([\d]{10})$") == false)
                        {
                            Clear();
                            WriteLine("Voorbeeld: 0612345678 of +316123345678");
                            WriteLine("Voer een goede Nederlandse telefoon nummer in: ");
                            telefoon = ReadLine();
                        }
                        CursorVisible = false;
                        break;
                    case 5:
                        Clear();
                        if (wachtwoord != wachtwoordHerhaal)
                        {
                            WriteLine("Het herhaalde wachtwoord komt niet overeen met je wachtwoord");
                        }
                        else if (gebruikersnaam == "<leeg>" || wachtwoord == "<leeg>" || wachtwoordHerhaal == "<leeg>" || email == "<leeg>" || telefoon == "<leeg>" || gebruikersnaam == "" || wachtwoord == "" || wachtwoordHerhaal == "" || email == "" || telefoon == "")
                        {
                            WriteLine("Niet alle gegevens zijn doorgegeven");
                        }
                        else if (App.userManager.CreateUser(gebruikersnaam, wachtwoord, email, telefoon))
                        {
                            App.userManager.Login(gebruikersnaam, wachtwoord);
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
        public void RunFromOrderConfirmation()
        {
            bool MenuBool = true;

            string gebruikersnaam = "<leeg>";
            string wachtwoord = "<leeg>";
            string wachtwoordHerhaal = "<leeg>";
            string email = "<leeg>";
            string telefoon = "<leeg>";

            while (MenuBool)
            {

                string titel = @"Account aanmaken?";
                string[] options = {$"Gebruikersnaam : {gebruikersnaam}", $"Wachtwoord : {wachtwoord}", $"Herhaal je wachtwoord : {wachtwoordHerhaal}",
                $"Email : {email}", $"Telefoon nummer : {telefoon}", "Bevestigen", "Terug"};
                Menu AccountCreationMenu = new Menu(options, titel, 0);
                int ChosenOption = AccountCreationMenu.Run();

                switch (ChosenOption)
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
                        while (true)
                        {
                            try
                            {
                                MailAddress test = new MailAddress(email);
                                break;
                            }
                            catch
                            {
                                Clear();
                                WriteLine("Voer een goede email in: ");
                                email = ReadLine();
                            }
                        }
                        CursorVisible = false;
                        break;
                    case 4:
                        Clear();
                        WriteLine("Voer je Nederlandse telefoon nummer in: ");
                        CursorVisible = true;
                        telefoon = ReadLine();
                        int inttelefoon = -1;
                        while (!Int32.TryParse(telefoon, out inttelefoon) || inttelefoon <= 0 || telefoon[0] != '0' || telefoon[1] != '6' || telefoon.Length != 10)
                        {
                            Clear();
                            WriteLine("Voorbeeld: 0612345678");
                            WriteLine("Voer een goede Nederlandse telefoon nummer in: ");
                            telefoon = ReadLine();
                        }
                        CursorVisible = false;
                        break;
                    case 5:
                        Clear();
                        if (wachtwoord != wachtwoordHerhaal)
                        {
                            WriteLine("Het herhaalde wachtwoord komt niet overeen met je wachtwoord");
                        }
                        else if (gebruikersnaam == "<leeg>" || wachtwoord == "<leeg>" || wachtwoordHerhaal == "<leeg>" || email == "<leeg>" || telefoon == "<leeg>" || gebruikersnaam == "" || wachtwoord == "" || wachtwoordHerhaal == "" || email == "" || telefoon == "")
                        {
                            WriteLine("Niet alle gegevens zijn doorgegeven");
                        }
                        else if (App.userManager.CreateUser(gebruikersnaam, wachtwoord, email, telefoon))
                        {
                            App.userManager.Login(gebruikersnaam, wachtwoord);
                            ConsoleUtils.WaitForKeyPress();
                            App.orderConfirmationScreen.run();
                            MenuBool = false;
                        }
                        ConsoleUtils.WaitForKeyPress();
                        break;
                    case 6:
                        App.orderConfirmationScreen.run();
                        MenuBool = false;
                        break;
                }
            }
        }
    }
}