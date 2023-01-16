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

            //using (var db = new BoardRentalContext())
            //{
            //    var newRental = new Models.BookedBoard()
            //    {
            //        BookDate = DateTime.Now,
            //        Customer = db.Customers.SingleOrDefault(x => x.Id == 1),
            //        Longboard = db.Longboards.SingleOrDefault(x => x.Id == 1)
            //    };
            //    db.Add(newRental);
            //    db.SaveChanges();
            //}


            bool runProgram = true;
            Customer? c = null;
            while (runProgram)
            {
                Console.Clear();
                Console.ResetColor();
                //Helpers.Welcome();
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