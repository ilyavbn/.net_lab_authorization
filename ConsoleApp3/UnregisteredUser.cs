using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class UnregisteredUser : User
    {
        private const String message = "U r unregistered , you have not ";

        public String GetLogin()
        {
            return message + "login";
        }
        public String GetPassword()
        {
            return message + "password";
        }
        public  String GetName()
        {
            return "Unnamed";
        }
        public  String GetSurname()
        {
            return "Unnamed";
        }
    }

    
    
}
