using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ConsoleApp3
{
    class RegisteredUser : User
    {
        private String login;
        private String password;
        
        private String name;
        private String surname;

        public RegisteredUser(String login, String password, String name, String surname)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.surname = surname;
        }

        public String GetLogin()
        {
            return login;
        }
        public String GetPassword()
        {
            return password;
        }
        public String GetName()
        {
            return name;
        }
        public String GetSurname()
        {
            return surname;
        }

    }
}
