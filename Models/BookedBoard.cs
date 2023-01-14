using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardRental.Models
{
    public class BookedBoard
    {
        public int Id { get; set; }
        public DateTime BookDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Longboard? Longboard { get; set; }
    }
}
