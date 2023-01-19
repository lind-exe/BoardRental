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
        public int CustomerId { get; set; }
        public int LongboardId { get; set; }
        public int? BookedDay { get; set; }
        public int? BookedWeek { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Longboard? Longboard { get; set; }
    }
}
