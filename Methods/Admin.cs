using BoardRental.Migrations;
using BoardRental.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BoardRental.Methods
{
    internal class Admin
    {

        internal static void AddBoard()                                     // C r u d
        {
            bool motorized = false;
            string boardType = "";
            Console.WriteLine("Input name of board: ");
            string name = Helpers.CheckStringInput();
            Console.WriteLine("Input brand: ");
            string brand = Helpers.CheckStringInput();
            Console.WriteLine("Is the board electrical?\n1. Yes\n2. No");
            int electric = Helpers.TryNumber(2, 1);
            if (electric == 1)
            {
                motorized = true;
            }
            Console.WriteLine("Input price: ");
            int price = Helpers.TryNumber(19551105, 200);  // the day Emmet Brown invented time machines!
            Console.WriteLine("What type of longboard is it?\n1. Cruiser\n2. Carving\n3. Downhill\n4. Freeride\n5. Futuristic");
            int type = Helpers.TryNumber(5, 1);
            switch (type)
            {
                case 1:
                    boardType = "Cruiser";
                    break;
                case 2:
                    boardType = "Carving";
                    break;
                case 3:
                    boardType = "Downhill";
                    break;
                case 4:
                    boardType = "Freeride";
                    break;
                case 5:
                    boardType = "Futuristic";
                    break;
            }
            Console.WriteLine("Input a description of the board: ");
            string description = Helpers.CheckStringInput();

            using (var db = new BoardRentalContext())
            {
                var newBoard = new Longboard()
                {
                    Name = name,
                    Brand = brand,
                    Motorized = motorized,
                    Price = price,
                    Type = boardType,
                    Description = description
                };
                db.Add(newBoard);
                db.SaveChanges();
            }


        }

        internal static void EditBoards()
        {


            using (var db = new BoardRentalContext())
            {
                var boardList = db.Longboards.ToList();

                Console.WriteLine("ID\t\tName\t\tPrice\t\tMotorized\t\tBrand");
                foreach (var board in db.Longboards.ToList())
                {
                    Console.WriteLine(board.Id + "\t\t" + board.Name + "\t\t" + board.Price + "\t\t" + board.Motorized + "\t\t\t" + board.Brand + "\t\t");
                }
                Console.WriteLine("Select id of the board you wish to edit: ");
                int idInput = Helpers.TryNumber(boardList.ToList().Count, 1);
                int selectedBoard = boardList[idInput - 1].Id;
                if (boardList[selectedBoard - 1] != null)
                {

                    OneBoard(selectedBoard);
                }
                else
                {
                    Console.WriteLine("There are no boards with corresponding id");
                }

                Console.WriteLine("What would you like to edit?\n1. Name\n2. Price\n3. Manual/ motorized\n4. Brand\n5. Delete board\n0. Return");
                int answer = Helpers.TryNumber(5, 0);
                switch (answer)
                {
                    case 1:
                        UpdateName(selectedBoard);
                        break;
                    case 2:
                        UpdatePrice(selectedBoard);
                        break;
                    case 3:
                        UpdateMotorized(selectedBoard);
                        break;
                    case 4:
                        UpdateBrand(selectedBoard);
                        break;
                    case 5:
                        DeleteBoard(selectedBoard);
                        break;
                }
            }
        }

        private static void DeleteBoard(int selectedBoard)                  // c r u D
        {
            using (var db = new BoardRentalContext())
            {
                var board = db.Longboards.Where(x => x.Id == selectedBoard).FirstOrDefault();
                Console.Write("\nAre you sure you wish to delete " + board.Name + "\n1. Yes\n2. No");
                int update = Helpers.TryNumber(2, 1);
                if (update == 1)
                {
                    db.Remove(board);
                }
                else if (update == 2)
                {
                    return;
                }
                db.SaveChanges();
            }
        }

        private static void UpdateBrand(int selectedBoard)
        {
            Console.Write("\nNew brand: ");
            string newBrand = Helpers.CheckStringInput();

            using (var db = new BoardRentalContext())
            {
                var update = db.Longboards.Where(x => x.Id == selectedBoard).FirstOrDefault();

                update.Brand = newBrand;
                db.SaveChanges();
            }
        }               // c r U d

        private static void UpdateMotorized(int selectedBoard)
        {
            Console.Write("\nIs the board motorized?\n1. Yes\n2. No ");
            using (var db = new BoardRentalContext())
            {
                int? update = Helpers.TryNumber(2, 1);
                var boolUpdate = db.Longboards.Where(x => x.Id == selectedBoard).FirstOrDefault();
                if (update == 1)
                {
                    boolUpdate.Motorized = true;
                }
                else if (update == 2)
                {
                    boolUpdate.Motorized = false;
                }
                db.SaveChanges();
            }
        }

        private static void UpdatePrice(int selectedBoard)
        {
            Console.Write("\nNew price: ");
            int? newPrice = Helpers.TryNumber(19551105, 500);  // 1955-11-05 The day Emmet Brown invented time travel!

            using (var db = new BoardRentalContext())
            {
                var update = db.Longboards.Where(x => x.Id == selectedBoard).FirstOrDefault();

                update.Price = newPrice;
                db.SaveChanges();
            }
        }

        private static void UpdateName(int selectedBoard)
        {
            Console.Write("\nNew name: ");
            string? newName = Helpers.CheckStringInput();

            using (var db = new BoardRentalContext())
            {
                var update = db.Longboards.Where(x => x.Id == selectedBoard).FirstOrDefault();

                update.Name = newName;
                db.SaveChanges();
            }
        }

        private static void OneBoard(int idInput)                           // c R u d
        {
            using (var db = new BoardRentalContext())
            {
                var selectedBoard = db.Longboards.FirstOrDefault(x => x.Id == idInput);
            }
        }
        internal static void Queries()
        {
            int input = Helpers.TryNumber(3, 1);

            switch (input)
            {
                case 1:
                    MostPopularBoard();
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private static void MostPopularBoard()
        {
            using (var db = new BoardRentalContext())
            {
                var mostPopularBoard = db.Longboards;
                                       


            }
        }
    }
}
