using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using static System.Console;

namespace CinemaApp
{
    class UserManager
    {
        public User currentUser = null;
        private string jsonFile = "userList.json";
        private List<User> users = new List<User>();

        public void CreateUser(string username, string password, string email) 
        {
            User user = new User() {
                Username = username,
                Password = password,
                Email = email,
            };
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
                        Clear();
                        WriteLine("Ingelogd");
                        break;
                    }
                    Clear();
                    WriteLine("Verkeerd wachtwoord");
                    break;
                }
            }
            if (!correctUsername){
                Clear();
                WriteLine("Dit is geen geldige gebruikersnaam");
            }

            users = new List<User>(); // maakt de users weer leeg voor veiligheid
        }


        public void LoadJson() {
            using (StreamReader sr = new StreamReader(jsonFile))
            {
                string json = sr.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
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

        public void Test() {
            LoadJson();
            foreach (User user in users) {
                WriteLine(user.Username);
            }
            Login("hans","willem");
            Login("hans","fout");
        }

    }
}