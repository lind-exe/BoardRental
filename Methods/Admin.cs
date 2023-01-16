using BoardRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BoardRental.Methods
{
    internal class Admin
    {
        static readonly string _connString = "data source=.\\SQLEXPRESS; initial catalog = BoardRental; persist security info = True; Integrated Security = True;";

        internal static void AddBoard()
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
            int price = Helpers.TryNumber(19550905, 200);
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
                Console.WriteLine("ID\t\tName\t\tPrice\t\tMotorized\t\tBrand");
                foreach (var board in db.Longboards.ToList())
                {
                    Console.WriteLine(board.Id + "\t\t" + board.Name + "\t\t" + board.Price + "\t\t" + board.Motorized + "\t\t\t" + board.Brand + "\t\t");
                }
                Console.WriteLine("Select id of the board you wish to edit: ");
                // input id hej
            }
        }

        internal static void Queries()
        {
            throw new NotImplementedException();
        }
    }
}
