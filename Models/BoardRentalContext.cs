using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BoardRental.Models
{
    public class BoardRentalContext : DbContext
    {
        public DbSet<Longboard>? Longboards{ get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<BookedBoard>? BookedBoards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:hanna.database.windows.net,1433;Initial Catalog=JonathanBoardRental;Persist Security Info=False;User ID=hannaadmin;Password=Lösenord1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
    
}
