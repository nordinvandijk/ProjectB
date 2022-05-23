using System;
using Xunit;

namespace CinemaApp.Tests
{
    public class UserManagerTests
    {
        [Theory]
        [InlineData("hans", "willem")]
        [InlineData("test", "test123")]
        public void LoginTest_ShouldWork(string username, string password)
        {
       
            UserManager um = new UserManager();
            um.Login(username, password);

            Assert.IsType<User>(um.currentUser);
        }   
    }
}
