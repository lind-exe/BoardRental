using BoardRental.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using BoardRental.Methods;
namespace BoardRental
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool runProgram = true;
            Customer? c = null;
            while (runProgram)
            {
                Console.Clear();
                Console.ResetColor();
                if (c == null)
                {
                    c = Menus.Show("LogIn", c);
                }
                else
                {
                    Methods.View.DisplayCustomer(c);
                    if (c.UserName == "admin")
                    {
                        Menus.Show("Main", c);
                    }
                    else
                    {
                        Methods.View.DisplayCustomer(c);
                        Menus.Show("Main", c);
                    }
                }
            }
        }
    }
}