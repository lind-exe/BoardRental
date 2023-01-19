using BoardRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoardRental.Methods
{                                                                               //(!bookCheck.BookedDay.HasValue) ????
    internal class View
    {
        internal static void BrowseBoards(Customer c)
        {
            using (var db = new BoardRentalContext())
            {
                var boardList = db.Longboards.ToList();

                int i = 0;

                Console.WriteLine("Id\tName\t\t\tPrice\t\t\tMotorized\t\tBrand");
                foreach (var b in boardList)
                {
                    i++;
                    Console.WriteLine(i + "\t" + b.Name + "\t\t\t" + b.Price + "\t\t\t" + b.Motorized + "\t\t\t" + b.Brand + "\t\t");
                }

                Console.Write("\nEnter id of the board you wish to rent: ");
                int answer = Helpers.TryNumber(boardList.Count(), 1);
                answer = boardList[answer - 1].Id;
                OneBoard(answer, c);
            }
        }

        private static void OneBoard(int boardId, Customer c)
        {
            Console.Clear();
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
                Console.WriteLine("\nDescription");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("==============================================================================");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + selectedBoard.Description);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("==============================================================================");
                Console.ResetColor();
            }
            Console.WriteLine("Is this board suitable?\n1. Yes\n2. No");
            int answer = Helpers.TryNumber(2, 1);
            if (answer == 1)
            {
                Console.Clear();
                BookingMenu(boardId, c);
            }
            else if (answer == 2)
            {
                Console.Clear();
                Menus.Show("Main", c);
            }
        }

        private static void BookingMenu(int boardId, Customer c)
        {
            using (var db = new BoardRentalContext())
            {

                int week = 1;
                View.OneWeek(week, boardId, c);


                bool booking = true;
                while (booking == true)
                {
                    Console.WriteLine("\n---------------------------------------------------------------------------");
                    Console.WriteLine("\n[1]. Select day | [2]. Previous week | [3]. Next week | [4]. Return to main");
                    int input = Helpers.TryNumber(4, 1);
                    switch (input)
                    {
                        case 1:
                            
                            Console.WriteLine("Which day would you like to book? 1-5");
                            int day = Helpers.TryNumber(5, 1);
                            Helpers.ConfirmBooking(week, day, boardId, c);
                            booking = false;
                            break;
                        case 2:
                            week--;
                            Console.Clear();
                            View.OneWeek(week, boardId, c);
                            break;
                        case 3:
                            week++;
                            Console.Clear();
                            View.OneWeek(week, boardId, c);
                            break;
                        case 4:
                            Console.Clear();
                            Menus.Show("Main", c);
                            booking = false;
                            break;
                    }
                }


            }
        }

        private static void OneWeek(int week, int boardId, Customer c)
        {
            using (var db = new BoardRentalContext())
            {
                var bookList = db.BookedBoards.Where(x => x.Longboard.Id == boardId);

                var bookCheck = bookList.ToList();

                List<string> daysInWeek;
                int dayCounter = 1;
                string availability = "";
                int i = 1;
                daysInWeek = new List<string>()
                {
                    "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"
                };

                Console.WriteLine("Week " + week + ":");
                Console.WriteLine("---------------------------------");
                foreach (var day in daysInWeek)
                {

                    Console.Write($"\n{dayCounter}. {day}");

                    foreach (var booked in db.BookedBoards.Where(b => b.Longboard.Id == boardId))
                    {
                        if (booked == null)
                        {
                            availability = "Available";
                        }
                        else if (booked != null)
                        {
                            if (booked.BookedDay == dayCounter && booked.BookedWeek == week)
                            {
                                availability = "Booked";
                                break;
                            }
                            else
                            {
                                availability = "Available";
                            }
                        }
                    }

                    if (availability == "Available")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("   \t\t" + availability);
                        Console.ResetColor();
                    }

                    else if (availability == "Booked")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("   \t\t" + availability);
                        Console.ResetColor();
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("   \t\t" + "Available");
                        Console.ResetColor();
                    }
                        dayCounter++;
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

        internal static void Bookings(Customer c)
        {
            using (var db = new BoardRentalContext())
            {
                var bookingList = db.BookedBoards.Where(x => x.CustomerId == c.Id).ToList();

                var bookingList2 = from booking in bookingList
                                   where booking.CustomerId == c.Id
                                   join boards in db.Longboards on booking.LongboardId equals boards.Id
                                   select new { booking.Id, boards.Name, booking.BookedWeek, booking.BookedDay, boards.Price };

                Console.WriteLine("Id\tName\t\t\tWeek\tDay\tPrice");
                foreach (var booking in bookingList2)
                {
                    Console.WriteLine($"{booking.Id}\t{booking.Name}\t\t\t{booking.BookedWeek}\t{booking.BookedDay}\t{booking.Price}");
                }
            }
        }
    }
}
