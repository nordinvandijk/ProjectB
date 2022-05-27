using System;
using Xunit;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using static System.Console;

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
        [Theory]
        [InlineData("false", "willem")]
        [InlineData("alsofalse", "test123")]
        public void LoginTest_Shouldfail(string username, string password)
        {

            UserManager um = new UserManager();
            um.Login(username, password);

            Assert.Null(um.currentUser);
        }
    }

}
