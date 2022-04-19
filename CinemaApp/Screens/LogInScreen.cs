using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using Newtonsoft.Json;

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
            string titel = @"Login/aanmelden";
            string[] options = {"Inloggen", "Account aanmaken", "Terug"};
            Menu LogInMenu = new Menu(options, titel, 0);
            int ChosenOption = LogInMenu.Run();

            switch(ChosenOption)
            {
                case 0:
                    //LogInFunction(); 
                    break;
                case 1:
                    //CreateAccount();
                    break;
                case 2:
                    App.homeScreen.run();
                    break;      

            }

            ConsoleUtils.WaitForKeyPress();
        }
        // public void LogInFunction(){
        //    Write("type je gebruikersnaam ");
        //    string userName = Console.ReadLine();
        //    Write("type je wachtwoord ");
        //    string Password = Console.ReadLine();
        // }
        // public void CreateAccount(){

        //    Write("type je gebruikersnaam ");
        //    string userName = ReadLine();
        //    Write("type je wachtwoord ");
        //    string password = ReadLine();
        //    Write("type je email ");
        //    string Email = ReadLine();
        //    string url = @"login.json";
        //    List<LoginData> loginlist = JsonConvert.DeserializeObject<List<LoginData>>(File.ReadAllText(url));
        //    loginlist.Add(new LoginData()
        //    {
        //       Username = userName,
        //       Password = password,
        //       EmailAdress= Email,
        //    });
        //    var convertedJson = JsonConvert.SerializeObject(loginlist,Formatting.Indented);
        //    File.WriteAllText(url,convertedJson);
           
           //var convertedjson = JsonConvert.SerializeObject() 
        //    LoginData loginData = new LoginData()
        //        {
        //         Username = userName,
        //         Password = password,
        //         EmailAdress = Email,
        //         LoginList = new List<string>(){
        //             userName,
        //             password,
        //             Email,
        //         }
        //        };

           
        //   var convertedList = JsonConvert.SerializeObject(loginData);
        //    string strData = JsonConvert.SerializeObject(loginData);
        //    File.WriteAllText(@"Login.json",strData);
        //    strData = String.Empty;
        //    strData = File.ReadAllText(@"Login.json");
        //    var dic = JsonConvert.DeserializeObject<IDictionary>(strData,strData);
        //    var data = JsonConvert.DeserializeObject<LoginData>(strData);
        //    WriteLine(data.ToString());
        //    strData = String.Empty;
        //    strData = File.ReadAllText(@"Login.json");
        //    var resultData= JsonConvert.DeserializeObject<LoginData>(strData);    
        //    List<LoginData> listLogins = JsonConvert.DeserializeObject<List<LoginData>>(@"Login.Data");
        //}
    }
}


