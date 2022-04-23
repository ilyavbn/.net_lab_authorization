using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp3
{
    class Application
    {
        private User user;
        private Boolean isStarted;
        private List<User> users;

        public Application()
        {

            user = new UnregisteredUser();
            isStarted = false;
            users = new List<User>();
            readDb();

        }
        public void Start()
        {
            if (isStarted) { return; }

            isStarted = true;

            Console.WriteLine("Type help to see commands list");

            while (isStarted)
            {
                Console.Write("$ ");
                String input = Console.ReadLine();

                switch (input)
                {
                    case "help":
                        help();
                        break;

                    case "login":
                        login();
                        break;

                    case "logout":
                        logOut();
                        break;

                    case "showinfo":
                        showInfo();
                        break;

                    case "reg":
                        registration();
                        break;

                    case "exit":
                        exit();
                        break;



                    default:
                        Console.WriteLine("command undefined");
                        break;
                }
            }
        }

        private void readDb()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("db.xml");

            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    XmlNode? attr = xnode.Attributes.GetNamedItem("login");
                    String login = attr?.Value;
                    String name = "";
                    String surname = "";
                    String password = "";
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "name")
                        {
                            name = childnode.InnerText;
                        }
                        if (childnode.Name == "surname")
                        {
                            surname = childnode.InnerText;
                        }
                        if (childnode.Name == "password")
                        {
                            password = childnode.InnerText;
                        }
                    }
                    users.Add(new RegisteredUser(login, password, name, surname));
                }
            }
        }
        private void registration()
        {
            
            Console.Write("Enter Name: ");
            String name = Console.ReadLine();
            Console.Write("Enter Surname: ");
            String surname = Console.ReadLine();
            Console.Write("Enter Login: ");
            String login = Console.ReadLine();
            Console.Write("Enter Password: ");
            String password = Console.ReadLine();

            foreach(User u in users)
            {
                if (u.GetLogin().Equals(login))
                {
                    Console.WriteLine("User " +  login + " is exist");
                    return;
                }
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("db.xml");
            XmlElement? xRoot = xDoc.DocumentElement;

            XmlElement personElem = xDoc.CreateElement("registereduser");

            XmlAttribute loginAttr = xDoc.CreateAttribute("login");

            XmlElement nameElem = xDoc.CreateElement("name");
            XmlElement surnameElem = xDoc.CreateElement("surname");
            XmlElement passElem = xDoc.CreateElement("password");


            XmlText loginText = xDoc.CreateTextNode(login);
            XmlText nameText = xDoc.CreateTextNode(name);
            XmlText surnameText = xDoc.CreateTextNode(surname);
            XmlText passText = xDoc.CreateTextNode(password);

            loginAttr.AppendChild(loginText);
            nameElem.AppendChild(nameText);
            surnameElem.AppendChild(surnameText);
            passElem.AppendChild(passText);


            personElem.Attributes.Append(loginAttr);
            personElem.AppendChild(nameElem);
            personElem.AppendChild(surnameElem);
            personElem.AppendChild(passElem);

            xRoot?.AppendChild(personElem);
            xDoc.Save("db.xml");

            users.Add(new RegisteredUser(login,password,name,surname));

        }

        

        public void login()
        {
            Console.Write("Enter Login: ");
            String login = Console.ReadLine();
            

            Boolean isExist = false;

            foreach(User u in users)
            {
                if (u.GetLogin().Equals(login))
                {
                    Console.Write("Enter Password: ");
                    String password = Console.ReadLine();

                    if (u.GetPassword().Equals(password))
                    {
                        user = u;
                        isExist = true;
                        Console.WriteLine("Login succeed");
                        break;
                    }
                }
            }
            if (!isExist) { Console.WriteLine("Login Failed"); }
        }

        private void logOut()
        {
            user = new UnregisteredUser();
        }

        private void exit()
        {
            isStarted = false;
        }

        private void help()
        {
            Console.WriteLine("List of commands: ");
            Console.WriteLine("\treg\n"+
                "\tlogin\n" +
                "\tlogout\n" +
                "\tshowinfo\n" +
                "\texit");
        }

        private void showInfo()
        {
            String name = user.GetName();
            String surname = user.GetSurname();
            String login = user.GetLogin();
            String password = user.GetPassword();

            Console.WriteLine(login + " info are : ");
            Console.WriteLine("\tname: " + name);
            Console.WriteLine("\tsurname: " + surname);
            Console.WriteLine("\tpassword: " + password);
            

        }


    }
}
