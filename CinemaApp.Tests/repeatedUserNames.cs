using Xunit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using CinemaApp;
using System.Linq;


public class RepeatingUsers
{

    [FactAttribute]

    public void UserJson_shouldNotbeEmpty()
    {
        List<User> userList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("userList.json")); // veranderd userlist.json in cinemaApp.test naar list
        List<string> userListfilter = new List<string>(); 
        bool norepeatingItems = true;
        for (int i = 0; i < userList.Count; i++)
        {
            userListfilter.Add(userList[i].Username);
        } //deze list heeft alle usernames (inclusief repeating items)

        for (int i = 0; i < userList.Count; i++)
        {
            userListfilter.Remove(userList[i].Username); //verwijderd user van userlist
            if (userListfilter.Contains(userList[i].Username)) //kijkt of de verwijderde user bestaat
            {
                norepeatingItems = false; //test is false als user nog steeds bestaad
            }
        }
        Assert.True(norepeatingItems);
    }
}
