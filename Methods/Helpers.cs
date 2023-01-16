using BoardRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardRental.Methods
{
    internal class Helpers
    {
        public static Customer TryLogIn(Customer c)
        {
            using (var db = new BoardRentalContext())
            {
                var customerList = db.Customers;
                Console.Write("Username: ");
                string user = Console.ReadLine();
                Console.Write("Password: ");
                string passWord = Console.ReadLine();
                var correctUsername = customerList.SingleOrDefault(x => x.UserName == user);
                var correctUser = customerList.SingleOrDefault(x => x.Password == passWord && x.UserName == user);


                if (correctUsername != null && correctUser != null)
                {
                    c = correctUser;
                }
                else if (correctUsername == null && correctUser == null)
                {
                    Console.WriteLine("User does not exist, try again or register new user!", c);
                }
                else if (correctUsername != null && correctUser == null)
                {
                    Console.WriteLine("Wrong password", c);
                }
                else
                {
                    Console.WriteLine("Try again", c);
                }
                return c;
            }
        }
        public static Customer CreateUser(Customer c)
        {

            Console.WriteLine("Input username: ");
            string userName = CheckStringInput();
            Console.WriteLine("Input password: ");
            string passWord = CheckStringInput();
            Console.WriteLine("Input first name: ");
            string firstName = CheckStringInput();
            Console.WriteLine("Input last name: ");
            string lastName = CheckStringInput();
            Console.WriteLine("Input city: ");
            string city = CheckStringInput();
            Console.WriteLine("Input phone number: ");
            string phone = CheckStringInput();
            Console.WriteLine("Input email address: ");
            string email = CheckStringInput();

            using (var database = new BoardRentalContext())            //detta lägger till varje sak
            {
                var customerList = database.Customers;
                var userNameExists = customerList.SingleOrDefault(x => x.UserName == userName) != null;
                if (userNameExists)
                {
                    Console.WriteLine("User already exists");
                }
                else
                {
                    var newCustomer = new Customer
                    {
                        UserName = userName,
                        Password = passWord,
                        FirstName = firstName,
                        LastName = lastName,
                        City = city,
                        Phone = phone,
                        Email = email
                    };
                    database.Add(newCustomer);
                    database.SaveChanges();
                    c = newCustomer;
                }
                return c;
            }

        }
        public static string CheckStringInput()
        {
            bool tryAgain = true;
            string? outPut = "";
            while (tryAgain)
            {
                outPut = Console.ReadLine();
                if (outPut == null)
                {
                    Console.WriteLine("Your input must contain atleast one character");
                }
                else if (outPut.Length > 0)
                {
                    tryAgain = false;
                }
                else
                {
                    Console.WriteLine("Your input must contain atleast one character");
                }

            }
            return outPut;
        }
        public static int TryNumber(int maxValue, int minValue)               //input security
        {
            int number = 0;
            bool correctInput = false;
            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number) || number > maxValue || number < minValue)
                {
                    Console.Write("Wrong input, try again: ");
                    //ClearLine();
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }
        internal static void WrongInput()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("WRONGINPUT");
            Thread.Sleep(500);
            Console.ResetColor();
            Console.Clear();
        }

        internal static void BoardType(int type)
        {
            throw new NotImplementedException();
        }
    }
}
