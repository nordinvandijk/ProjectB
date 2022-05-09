using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;


namespace CinemaApp.Screens
{
    class Subscriptions : Screen
    {
        //Fields

        //Constructor
        public Subscriptions(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            App.userManager.LoadJson();
            string jsonFile = "userList.Json";
            List<User> users = new List<User>();
            using (StreamReader sr = new StreamReader(jsonFile))
            {
                string json = sr.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }


            string titel = @"Hier krijg je alle abonnementen te zien";

            string[] options = {"Abonnement 1", "Abonnement 2", "Abonnement 3", "Terug"};
            Menu OrderOverviewMenu = new Menu(options, titel, 0);
            int ChosenOption = OrderOverviewMenu.Run();

            if (ChosenOption == options.GetLength(0)-1)
            {   
                App.subscriptionScreen.run();
            }
            else
            {
                string chosenabonnement = options.GetValue(ChosenOption).ToString();
                foreach (User user in users) 
                {
                    if (user.Username == App.userManager.currentUser.Username) 
                    {
                        user.Abonnement = chosenabonnement;
                    }
            }
                using (StreamWriter sw = new StreamWriter(jsonFile))
                {   
                    string json = JsonConvert.SerializeObject(users,Formatting.Indented);
                    sw.WriteLine(json);
                } 
            }

            ConsoleUtils.WaitForKeyPress();
        }
    }   
}