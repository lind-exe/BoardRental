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
