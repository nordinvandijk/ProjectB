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
        List<User> userList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@".\userList.json"));
        List<string> userListfilter = new List<string>();
        bool norepeatingItems = true;
        for (int i = 0; i < userList.Count; i++)
        {
            userListfilter.Add(userList[i].Username);
        }

        for (int i = 0; i < userList.Count; i++)
        {
            userListfilter.Remove(userList[i].Username);
            if (userListfilter.Contains(userList[i].Username))
            {
                norepeatingItems = false;
            }
        }
        Assert.True(norepeatingItems);
    }
}
