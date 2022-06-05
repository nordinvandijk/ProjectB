using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using static System.Console;
using System;
using System.Globalization;

namespace CinemaApp
{
    public class UserManager
    {
        public User currentUser = null;
        private string jsonFile = "userList.json";
        public List<User> users = new List<User>();

        /// <summary>
        /// Met deze functie wordt de betaal datum van abonnement in elke user geupdate, als de betaaldatum is geweest wordt de nieuwe betaaldatum een maand later.
        /// </summary>
        public UserManager(){
            LoadJson();
        }
        public void UpdateSubscriptionLastPayDate()
        {
            LoadJson();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Abonnement != null && DateTime.Parse(users[i].Abonnement[1]) < DateTime.Today)
                {
                    var cultureInfo = new CultureInfo("nl-NL");
                    DateTime dateTime = DateTime.Parse(users[i].Abonnement[1]);
                    dateTime = dateTime.AddMonths(1);
                    users[i].Abonnement[1] = dateTime.ToString("dd-MM-yyyy", cultureInfo);
                }
            }
            UpdateJson();
        }
        public bool CreateUser(string username, string password, string email, string telefoonNummer) 
        {
            User user = new User() {
                Username = username,
                Password = password,
                Email = email,
                PhoneNumber = telefoonNummer
            };
            if(!UsernameAlreadyExist(username)){
                users.Add(user);
                UpdateJson();
                WriteLine("Account is succesvol aangemaakt.");
                return true;
            }
            else {
                WriteLine("Account bestaat al");
                return false;
            }
        }
        
        public void RemoveUser() {

        }

        public void Login(string username, string password){
            LoadJson();
            bool correctUsername = false;
            foreach (User user in users) {
                if (user.Username == username) {
                    correctUsername = true;
                    if (user.Password == password) {
                        currentUser = user;
                        WriteLine("Ingelogd");
                        break;
                    }
                    WriteLine("Verkeerd wachtwoord");
                    break;
                }
            }
            if (!correctUsername){
                Clear();
                WriteLine("Dit is geen geldige gebruikersnaam");
            }

            //users.Clear(); dit heb ik weggehaald omdat het een bug creeert voor omzet omzet
        }

        public void LoadJson() {
            using (StreamReader sr = new StreamReader(jsonFile))
            {
                string json = sr.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }

            // Zorgt ervoor dat als de json nog een keer wordt ingeladen dat de currentUser naar de juiste user in users point.
            if (currentUser != null) {
                for (int i = 0; i < users.Count; i++) {
                    if (users[i].Username == currentUser.Username) {
                        users[i] = currentUser;
                        return;
                    }
                }
                WriteLine(currentUser + " wordt/is niet goed opgeslagen");
            }
        }

        public void UpdateJson() 
        {
            using (StreamWriter sw = new StreamWriter(jsonFile))
            {   
                string json = JsonConvert.SerializeObject(users,Formatting.Indented);
                sw.WriteLine(json);
            } 
        }

        public bool UsernameAlreadyExist(string username){
            LoadJson();
            foreach (User user in users) {
                if (user.Username == username) {
                    return true;
                }
            }
            return false;
        }
    }
}