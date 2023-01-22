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
                    ReturnToLogIn("User does not exist, try again or register new user!", c);
                }
                else if (correctUsername != null && correctUser == null)
                {
                    ReturnToLogIn("Wrong password, try again!", c);
                }
                else
                {
                    ReturnToLogIn("Try again", c);
                }
                return c;
            }
        }
        internal static void ReturnToLogIn(string message, Customer c)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.ResetColor();
            Console.Clear();
            Menus.Show("LogIn", c);
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
            int phone = Helpers.TryNumber(999999999, 99999999);
            Console.WriteLine("Input email address: ");
            string email = CheckStringInput();

            using (var database = new BoardRentalContext())          
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
                        Phone = phone.ToString(),
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
        public static int TryNumber(int maxValue, int minValue)
        {
            int number = 0;
            bool correctInput = false;
            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number) || number > maxValue || number < minValue)
                {
                    Console.Write("Wrong input, try again: ");

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
        internal static void ConfirmBooking(int week, int day, int boardId, Customer c)
        {

            using (var db = new BoardRentalContext())
            {
                string selectedDay = "";
                switch (day)
                {
                    case 1:
                        selectedDay = "Monday";
                        break;
                    case 2:
                        selectedDay = "Tuesday";
                        break;
                    case 3:
                        selectedDay = "Wednesday";
                        break;
                    case 4:
                        selectedDay = "Thursday";
                        break;
                    case 5:
                        selectedDay = "Friday";
                        break;
                }


                var selectedBoard = db.Longboards.Where(x => x.Id == boardId).FirstOrDefault();
                var customer = db.Customers.Where(x => x.Id == c.Id);
                var booking = db.BookedBoards.Where(x => x.Longboard.Id == boardId && x.BookedWeek == week && x.BookedDay == day).FirstOrDefault();
                Console.Clear();
                if (booking == null)
                {
                    Console.Write("You have selected ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(selectedBoard.Name);
                    Console.ResetColor();
                    Console.Write(" for booking on ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(selectedDay);
                    Console.ResetColor();
                    Console.Write(" week ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(week);
                    Console.ResetColor();
                    Console.WriteLine("\nYour booking price is: " + selectedBoard.Price);
                    Console.WriteLine("\nAre you sure you wish to proceed?\n1. Yes\n2. No");
                    int input = Helpers.TryNumber(2,1);

                    if (input == 1)
                    {
                        var newBooking = new BookedBoard()
                        {
                            BookedDay = day,
                            BookedWeek = week,
                            LongboardId = selectedBoard.Id,
                            CustomerId = c.Id
                        };
                        db.Add(newBooking);
                        db.SaveChanges();
                        Console.WriteLine("\n\nWe will send a text to " + c.Phone + " as well as an email to " + c.Email + "" +
                            "\nWith additional instructions when to pick up your board as well as how to use it & how to return it!");
                        
                        Helpers.PressAnyKey();
                        Console.Clear();
                        Victory(c);
                    }
                    else if (input == 2)
                    {
                        Console.Write("Returning to main menu");
                        Thread.Sleep(500);
                        Console.Write(".");
                        Thread.Sleep(500);
                        Console.Write(".");
                        Thread.Sleep(500);
                        Console.Write(".");
                        Thread.Sleep(500);
                        Console.Write(".");                     
                        Thread.Sleep(500);
                        Console.Clear();
                        Menus.Show("Main", c);
                        
                    }
                }
                else if (booking != null)
                {
                    Console.WriteLine("Already booked, try another day");
                }
            }
        }

        public static void PressAnyKey()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey(true);
            Console.ResetColor();
        }

        private static void Victory(Customer c)
        {
            string text = "------------------------------------------------------------\n|\t\t    BOOKING CONFIRMED!                     |\n|\t\tThank you and ride safe :)                 |\n------------------------------------------------------------";

            for (int i = 0; i < 5; i++)
            {


                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(text);
                Thread.Sleep(100);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(text);
                Thread.Sleep(100);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(text);
                Thread.Sleep(100);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(text);
                Thread.Sleep(100);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(text);
                Thread.Sleep(100);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(text);
                Thread.Sleep(100);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(text);
                Thread.Sleep(100);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(text);
                Thread.Sleep(100); 

            }
            Console.Clear();
            Menus.Show("Main", c);
        }
    }
}
