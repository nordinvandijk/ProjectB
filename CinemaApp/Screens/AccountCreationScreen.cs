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
            string titel = @"Account aanmaken?";

            string[] options = {"Maak account aan", "terug"};
            Menu AccountCreationMenu = new Menu(options, titel, 0);
            int ChosenOption = AccountCreationMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    Clear();
                    Write("Type je username: ");
                    string username = ReadLine();
                    Write("Type je wachtwoord: ");
                    string password = ReadLine();
                    Write("Type je mail: ");
                    string mail = ReadLine();
                    if(App.userManager.CreateUser(username,password,mail)){
                        WriteLine("Account is succesvol aangemaakt.");
                        ConsoleUtils.WaitForKeyPress();
                        App.userManager.Login(username,password); 
                        ConsoleUtils.WaitForKeyPress();
                    }
                    else{
                        WriteLine("Account bestaat al");
                        ConsoleUtils.WaitForKeyPress();
                        App.logInScreen.run();
                        break;
                    }
                    App.homeScreen.run();
                    break;
                case 1:
                    App.logInScreen.run();
                    break; 
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}