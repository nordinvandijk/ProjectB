using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;

namespace CinemaApp.Screens
{
    [Serializable]
    class LoginData
    {
      public string Username {get;set;}
      public string Password {get;set;}
      public string EmailAdress {get;set;}
    }  
}