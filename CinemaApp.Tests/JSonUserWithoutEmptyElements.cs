using Xunit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CinemaApp.Tests
{
    public class UserJsonTest
    {

        [FactAttribute]

        public void UserJson_shouldNotbeEmpty()
        {
            List<User> userList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("userList.json"));
            bool JsonUserWithEmptyElements = false;
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Username == "" || userList[i].Password == "" || userList[i].Email == "" || userList[i].PhoneNumber == "")
                {
                    JsonUserWithEmptyElements = true;
                    break;
                }
            }
            Assert.True(!JsonUserWithEmptyElements);
        }
    }
}
