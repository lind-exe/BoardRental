using BoardRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardRental.Methods
{
    internal class View
    {
        internal static void BrowseBoards(Customer c)
        {
            using (var db = new BoardRentalContext())
            {
                var boardList = db.Longboards.ToList();
                Console.WriteLine("Id\t\tName\t\tPrice\t\tMotorized\t\tBrand");
                foreach (var b in boardList)
                {
                    Console.WriteLine(b.Id + "\t\t" + b.Name + "\t\t" + b.Price + "\t\t" + b.Motorized + "\t\t\t" + b.Brand + "\t\t");
                }
            }
        }

        internal static void DisplayCustomer(Customer c)
        {
            Console.Clear();
            Console.SetCursorPosition(40, 0);
            string? displayUserName = ("User: " + ((c.FirstName != "" ? c.FirstName : (c.UserName == "" ? "Unknown" : c.UserName)) + "\n"));
            try
            {
                Console.WriteLine(displayUserName);
            }
            catch (Exception) { Console.WriteLine("Fel"); }
        }
    }
}
