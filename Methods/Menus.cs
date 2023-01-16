using BoardRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardRental.Methods;

namespace BoardRental.Methods
{
    internal class Menus
    {
        enum LogIn
        {
            Sign_In = 1,
            Create_New_User,
        }
        enum Main
        {
            Browse_Boards = 1,
            View_Bookings,
            Log_Out,
            Admin_Menu
        }
        enum Bookings
        {
            Edit_Booking = 1,
            Remove_Booking,
            Return = 0
        }
        enum Admin
        {
            Add_Board = 1,
            Edit_Boards,
            Queries,
            Return = 0
        }

        public static Customer Show(string value, Customer c)                   //fixa wrong input till meny val - funkar nästan
        {
            bool logIn = true;
            bool goMain = true;
            bool booking = true;
            bool adminMenu = true;

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
            if (value == "Main")
            {
                while (goMain)
                {
                    foreach (int i in Enum.GetValues(typeof(Main)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(Main), i).Replace("_", " ")}");
                    }

                    int nr;
                    Main menu = (Main)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(Main)).Length - 1)
                    {
                        menu = (Main)nr;
                        Console.Clear();
                    }
                    else
                    {
                        Helpers.WrongInput();
                    }
                    switch (menu)
                    {
                        case Main.Browse_Boards:
                            View.BrowseBoards(c);
                            goMain = false;
                            break;
                        case Main.View_Bookings:
                            Show("Bookings", c);
                            goMain = false;
                            break;
                        case Main.Log_Out:
                            c = null;
                            goMain = false;
                            Show("LogIn", c);
                            break;
                        case Main.Admin_Menu:
                            if (c.UserName == "admin")
                            {
                                Show("Admin", c);
                            }
                            else
                            {
                                Helpers.WrongInput();
                            }
                            break;
                    }
                }
            }
            if (value == "Bookings")
            {
                while (booking)
                {
                    foreach (int i in Enum.GetValues(typeof(Bookings)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(Bookings), i).Replace("_", " ")}");
                    }

                    int nr;
                    Bookings menu = (Bookings)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(Bookings)).Length - 1)
                    {
                        menu = (Bookings)nr;
                        Console.Clear();
                    }
                    else
                    {
                        Helpers.WrongInput();
                    }
                    switch (menu)
                    {
                        case Bookings.Edit_Booking:

                            booking = false;
                            break;
                        case Bookings.Remove_Booking:

                            booking = false;
                            break;
                        case Bookings.Return:

                            booking = false;
                            Show("Main", c);
                            break;
                    }
                }
            }
            if (value == "Admin")
            {
                while (adminMenu)
                {
                    foreach (int i in Enum.GetValues(typeof(Admin)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(Admin), i).Replace("_", " ")}");
                    }

                    int nr;
                    Admin admin = (Admin)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(Admin)).Length - 1)
                    {
                        admin = (Admin)nr;
                        Console.Clear();
                    }
                    else
                    {
                        Helpers.WrongInput();
                    }
                    switch (admin)
                    {
                        case Admin.Add_Board:
                            Methods.Admin.AddBoard();
                            adminMenu = false;
                            break;
                        case Admin.Edit_Boards:
                            Methods.Admin.EditBoards();
                            adminMenu = false;
                            break;
                        case Admin.Queries:
                            Methods.Admin.Queries();
                            adminMenu = false;
                            break;
                        case Admin.Return:
                            Show("Main", c);
                            adminMenu = false;
                            break;
                    }
                }
            }
            return c;
        }
    }
}
