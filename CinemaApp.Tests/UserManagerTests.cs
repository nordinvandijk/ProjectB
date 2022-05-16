using System;
using Xunit;

namespace CinemaApp.Tests
{
    public class UserManagerTests
    {
        [Fact]
        public void LoginTest()
        {
       
            UserManager um = new UserManager();
            um.Login("hans", "willem");

            Assert.IsType<User>(um.currentUser);
        }
    }
}
