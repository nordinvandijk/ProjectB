using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using static System.Console; 

namespace CinemaApp.Screens
{
    class SubscriptionScreen : Screen
    {
        //Fields

        //Constructor
        public SubscriptionScreen(Application app) : base(app) //Neemt appliaction van de parent class
        {
        }

        //Methods
        public override void run()
        {
            string titel = @"Het Filmhuis-abonnement
In dit scherm kan je het Filmhuis-abonnement kopen.
Het Filmhuis-abonnement heeft de volgende voordelen:
    - Korting op films, eten en drinken en accessoires
    - Je kan naar het abonnementenevenement.";

            string[] options = {"Kopen", "Terug"};
            Menu SubscriptionMenu = new Menu(options, titel, 0);
            int ChosenOption = SubscriptionMenu.Run();
            string titel2 = @"Hier krijg je alle abonnementen te zien";

            string[] options2 = {"Abonnement 1", "Abonnement 2", "Abonnement 3", "Terug"};
            Menu OrderOverviewMenu = new Menu(options2, titel2, 0);

            switch(ChosenOption)
            {
                case 0:
                    App.userManager.LoadJson();
                    string jsonFile = "userList.Json";
                    List<User> users = new List<User>();
                    
                    using (StreamReader sr = new StreamReader(jsonFile))
                    {
                        string json = sr.ReadToEnd();
                        users = JsonConvert.DeserializeObject<List<User>>(json);
                    }

                    int ChosenOption2 = OrderOverviewMenu.Run();

                    if (ChosenOption2 == options2.GetLength(0)-1)
                    {   
                        App.subscriptionScreen.run();
                    }
                    else
                    {
                        string chosenabonnement = options2.GetValue(ChosenOption2).ToString();
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
                        Clear();
                        WriteLine(chosenabonnement + " is gekocht.");
                        ConsoleUtils.WaitForKeyPress();
                        App.homeScreen.run();
                    }
                    break;
                case 1:
                    App.homeScreen.run();
                    break;     
            }
        }
    }   
}