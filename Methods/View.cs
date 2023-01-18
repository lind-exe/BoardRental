using BoardRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

                int i = 0;

                Console.WriteLine("Id\t\tName\t\t\tPrice\t\t\tMotorized\t\tBrand");
                foreach (var b in boardList)
                {
                    i++;
                    Console.WriteLine(i + "\t" + b.Name + "\t\t\t" + b.Price + "\t\t\t" + b.Motorized + "\t\t\t" + b.Brand + "\t\t");
                }

                Console.Write("Enter id of the board you wish to rent or 0 to return: ");
                int answer = Helpers.TryNumber(boardList.Count(), 0);
                int selectedProduct = answer;
                answer = boardList[answer - 1].Id;
                OneBoard(answer, c);
            }
        }

        private static void OneBoard(int boardId, Customer c)
        {
            using (var db = new BoardRentalContext())
            {

                var selectedBoard = db.Longboards.Where(x => x.Id == boardId).FirstOrDefault();



                int padValue1 = 15;
                int padValue2 = 20;


                Console.WriteLine("Name".PadRight(30) + "Price".PadRight(padValue1) +
                                  "Brand".PadRight(padValue2) + "Motorized".PadRight(padValue2) +
                                  "Type".PadRight(padValue1));
                Console.WriteLine("----------------------------------------------------------------------------------------------");

                Console.WriteLine(selectedBoard.Name.PadRight(30) + selectedBoard.Price.ToString().PadRight(padValue1) +
                                  selectedBoard.Brand.ToString().PadRight(padValue2) + selectedBoard.Motorized.ToString().PadRight(padValue2) +
                                  selectedBoard.Type.ToString().PadRight(padValue1));
                Console.WriteLine("\n" + selectedBoard.Description);
            }
            BookingMenu(boardId, c);
        }

        private static void BookingMenu(int boardId, Customer c)
        {
            using (var db = new BoardRentalContext())
            {
                var selectedBoard = db.Longboards.Where(x => x.Id == boardId).FirstOrDefault();

                var bookCheck = db.BookedBoards.Where(x => x.Longboard.Id == boardId).FirstOrDefault();

                List<string> daysInWeek;
                int dayCounter = 0;
                string availability = "";

                daysInWeek = new List<string>()
                {
                    "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"
                };

                //(!bookCheck.BookedDay.HasValue) ????

                foreach (var day in daysInWeek)
                {
                    dayCounter++;
                    if (bookCheck == null)
                    {
                        availability = "Available";

                    }
                    else if (bookCheck != null)
                    {
                        if (bookCheck.BookedDay == dayCounter)
                        {
                            availability = "Booked";
                        }
                        else if (bookCheck.BookedDay != dayCounter)
                        {
                            availability = "Available";
                        }
                        
                    }
                    Console.Write(dayCounter + ". " + day + " ");
                    if (availability == "Available")
                    {  
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(availability + "\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(availability + "\n");
                        Console.ResetColor();
                    }
                    

                }
                Console.ReadLine();
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
