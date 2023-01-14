using BoardRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardRental.Methods
{
    internal class Menus
    {
        enum LogIn
        {
            Sign_In = 1,
            Create_New_User,
        }
        public static Customer Show(string value, Customer c)                   //fixa wrong input till meny val - funkar nästan
        {
            bool logIn = true;



            if (value == "LogIn")
            {
                while (logIn)
                {
                    foreach (int i in Enum.GetValues(typeof(LogIn)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(LogIn), i).Replace("_", " ")}");
                    }

                    int nr;
                    LogIn login = (LogIn)99; //Default
                    if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(LogIn)).Length)
                    {
                        Console.WriteLine("Wrong input try again!");
                    }
                    else
                    {
                        login = (LogIn)nr;
                        Console.Clear();
                    }
                    switch (login)
                    {
                        case LogIn.Sign_In:
                            c = Helpers.TryLogIn(c);
                            logIn = false;
                            break;
                        case LogIn.Create_New_User:
                            c = Helpers.CreateUser(c);
                            logIn = false;
                            break;

                    }
                }
                Console.WriteLine("Succesfully logged in!");
            }
            return c;
        }
    }
}
